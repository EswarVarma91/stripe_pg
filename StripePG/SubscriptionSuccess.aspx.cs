using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            litSessionId.Text = "<p>Checkout Session ID: " + session.Id + "</p>";
            // You might also want to display the customer email or subscription details
            // if they are available and relevant for the user.
        }
    }
}