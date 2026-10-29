using Stripe;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

public partial class SubscriptionSuccess : System.Web.UI.Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        // Initialize Stripe API key
        StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

        // Optionally, retrieve the session_id from the query string
        // and fetch session details from Stripe for display purposes.
        // NOTE: The webhook is the primary source of truth for provisioning.
        if (!IsPostBack && Request.QueryString["session_id"] != null)
        {
            string sessionId = Request.QueryString["session_id"];
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = await service.GetAsync(sessionId);

            // You can display information from the session here, e.g.:
            litSessionId.Text = "<p>Checkout Session ID: " + Server.HtmlEncode(session.Id) + "</p>";

            // Show which plan (monthly / yearly) was just purchased
            if (!string.IsNullOrEmpty(session.SubscriptionId))
            {
                try
                {
                    var subscriptionService = new Stripe.SubscriptionService();
                    Stripe.Subscription subscription = await subscriptionService.GetAsync(session.SubscriptionId);

                    var item = subscription.Items.Data.FirstOrDefault();
                    if (item != null && item.Price != null)
                    {
                        string interval = (item.Price.Recurring != null) ? item.Price.Recurring.Interval : "month";
                        string planName = (interval == "year") ? "Yearly" : "Monthly";
                        string currency = string.IsNullOrEmpty(item.Price.Currency) ? "" : item.Price.Currency.ToUpperInvariant();
                        string amount = item.Price.UnitAmount.HasValue ? (item.Price.UnitAmount.Value / 100m).ToString("0.00") : "";

                        litPlan.Text = "<p>Plan: <strong>" + Server.HtmlEncode(planName) + "</strong> &mdash; " +
                                       Server.HtmlEncode(amount) + " " + Server.HtmlEncode(currency) +
                                       " (" + Server.HtmlEncode(interval) + "ly)</p>";

                        litRenewal.Text = "<p>Next renewal: " +
                                          Server.HtmlEncode(subscription.CurrentPeriodEnd.ToLongDateString()) + "</p>";
                    }
                }
                catch (StripeException)
                {
                    // The webhook remains the source of truth; if the subscription cannot be
                    // fetched here we just skip the extra detail instead of failing the page.
                }
            }
        }
    }
}
