using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

public partial class Subscribe : System.Web.UI.Page
{
    // Display-only plan info rendered by Subscribe.aspx (values come from Web.config)
    protected string MonthlyPriceText = "";
    protected string YearlyPriceText = "";
    protected string YearlyBadgeText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize Stripe API key from Web.config
        StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

        string currency = (ConfigurationManager.AppSettings["StripeCurrency"] ?? "USD").ToUpperInvariant();
        decimal monthly = ParseAmount(ConfigurationManager.AppSettings["StripeMonthlyAmount"], 10m);
        decimal yearly = ParseAmount(ConfigurationManager.AppSettings["StripeYearlyAmount"], 100m);

        MonthlyPriceText = monthly.ToString("0.00") + " " + currency;
        YearlyPriceText = yearly.ToString("0.00") + " " + currency;

        // Show the saving hint only when the configured yearly price is cheaper than 12 monthly payments
        if (monthly > 0 && yearly > 0 && yearly < (monthly * 12))
        {
            int savePercent = (int)Math.Round((1m - (yearly / (monthly * 12))) * 100m);
            YearlyBadgeText = "Save " + savePercent + "%";
        }
        else
        {
            YearlyBadgeText = "";
        }

        // Hide the yearly card until a yearly price ID has been configured,
        // so the page never offers a plan that cannot check out.
        pnlYearly.Visible = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["StripeYearlyPriceId"]);
    }


    protected async void btnSubscribe_Click(object sender, EventArgs e)
    {
        // Which plan did the visitor pick?
        bool isYearly = radYearly.Checked && pnlYearly.Visible;

        string priceSetting = isYearly ? "StripeYearlyPriceId" : "StripeMonthlyPriceId";
        string planName = isYearly ? "yearly" : "monthly";
        string interval = isYearly ? "year" : "month";

        // Price ID comes from Web.config:
        // Dashboard -> Billing -> Products -> your product -> recurring price
        string priceId = ConfigurationManager.AppSettings[priceSetting];

        if (string.IsNullOrWhiteSpace(priceId) || !priceId.StartsWith("price_"))
        {
            lblError.Text = "The " + planName + " plan is not configured yet. Set " + priceSetting +
                            " in Web.config to a recurring price ID from the Stripe Dashboard (Billing > Products).";
            lblError.Visible = true;
            return;
        }
        lblError.Visible = false;

        StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

        // Define the URLs Stripe will redirect to after checkout
        string successUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/SubscriptionSuccess.aspx?session_id={CHECKOUT_SESSION_ID}";
        string cancelUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/SubscriptionCancel.aspx";

        string UserEmail = "azhar@rezy360.com";

        var options = new Stripe.Checkout.SessionCreateOptions
        {
            Mode = "subscription", // This is crucial for subscriptions
            LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceId,
                        Quantity = 1,
                    },
                },
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            // Optional: Pre-fill customer email if available (e.g., from logged-in user)
            CustomerEmail = UserEmail,//User.Identity.IsAuthenticated ? User.Identity.Name : null,
            // Record which plan was chosen so the webhook / success page can use it
            Metadata = new Dictionary<string, string>
            {
                { "plan", planName },
                { "interval", interval }
            }
        };

        var service = new Stripe.Checkout.SessionService();
        Stripe.Checkout.Session session = await service.CreateAsync(options);

        // Redirect the user to the Stripe Checkout page
        Response.Redirect(session.Url);

    }

    private static decimal ParseAmount(string value, decimal fallback)
    {
        decimal parsed;
        return decimal.TryParse(value, out parsed) ? parsed : fallback;
    }
}
