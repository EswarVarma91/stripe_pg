using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Subscribe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize Stripe API key from Web.config
        StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
    }


    protected async void btnSubscribe_Click(object sender, EventArgs e)
    {
        // IMPORTANT: Replace with your actual Price ID from Stripe Dashboard
        // Go to dashboard.stripe.com -> Billing -> Products -> Your Product -> Recurring Price
        string priceId = "price_1RQLLwEVX74DuW0s6YoDMaWh"; // Example Price ID

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
            // Optional: Add metadata to the session if you need to pass custom data
            // Metadata = new Dictionary<string, string> { { "userId", "your_user_id_here" } }
        };

        var service = new Stripe.Checkout.SessionService();
        Stripe.Checkout.Session session = await service.CreateAsync(options);

        // Redirect the user to the Stripe Checkout page
        Response.Redirect(session.Url);
        
    }
}