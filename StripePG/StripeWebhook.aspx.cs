using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stripe;
using Stripe.BillingPortal; // For Customer Portal events if needed

using System.Configuration;
using System.IO;


public partial class StripeWebhook : System.Web.UI.Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        // This page should only handle POST requests from Stripe
        if (Request.HttpMethod == "POST")
        {
            // Initialize Stripe API key
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

            // Read the raw request body
            string json = await new StreamReader(Request.InputStream).ReadToEndAsync();

            // Get the Stripe-Signature header
            string stripeSignatureHeader = Request.Headers["Stripe-Signature"];

            // Get your webhook signing secret from Web.config
            string webhookSecret = ConfigurationManager.AppSettings["StripeWebhookSecret"];

            Event stripeEvent;

            try
            {
                // Construct the event to verify its authenticity
                stripeEvent = EventUtility.ConstructEvent(json, stripeSignatureHeader, webhookSecret);
            }
            catch (StripeException ex)
            {
                // Invalid signature
                Response.StatusCode = 400;
                Response.Write("Invalid webhook signature.");
                // Log the error: Console.WriteLine($"Webhook Error: {ex.Message}");
                Response.End();
                return;
            }
            catch (Exception ex)
            {
                // Other parsing errors
                Response.StatusCode = 400;
                Response.Write("Error parsing webhook event.");
                // Log the error: Console.WriteLine($"Webhook Error: {ex.Message}");
                Response.End();
                return;
            }

            // Handle the event based on its type
            switch (stripeEvent.Type)
            {
                case Stripe.Events.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    // This event indicates a successful checkout session.
                    // This is where you should provision your user's access in your database.
                    // You get the CustomerId and SubscriptionId from the session.
                    Console.WriteLine("Checkout Session Completed! Session ID: "+session.Id);
                    Console.WriteLine("Customer ID: "+session.CustomerId);
                    Console.WriteLine("Subscription ID: "+session.SubscriptionId);
                    // Example: Update your local database:
                    // YourDbManager.UpdateUserSubscription(session.CustomerId, session.SubscriptionId, "active", session.Metadata?["userId"]);
                    break;

                case Stripe.Events.InvoicePaid:
                    var invoice = stripeEvent.Data.Object as Invoice;
                    // This event fires when an invoice is paid (initial or recurring).
                    // You can confirm subscription status and update your database if needed.
                    Console.WriteLine("Invoice Paid! Invoice ID: "+invoice.Id );
                    Console.WriteLine("Customer ID: "+invoice.CustomerId);
                    // Check if invoice.SubscriptionId is available for linked subscriptions
                    // YourDbManager.UpdateSubscriptionPaymentStatus(invoice.SubscriptionId, "paid");
                    break;

                case Stripe.Events.CustomerSubscriptionUpdated:
                    var subscriptionUpdated = stripeEvent.Data.Object as Subscription;
                    // This event fires when a subscription changes (e.g., plan change, trial ends).
                    Console.WriteLine("Subscription Updated! Subscription ID: "+subscriptionUpdated.Id);
                    Console.WriteLine("New Status: "+subscriptionUpdated.Status);
                    // YourDbManager.UpdateSubscriptionStatus(subscriptionUpdated.Id, subscriptionUpdated.Status);
                    break;

                case Stripe.Events.CustomerSubscriptionDeleted:
                    var subscriptionDeleted = stripeEvent.Data.Object as Subscription;
                    // This event fires when a subscription is cancelled.
                    Console.WriteLine("Subscription Deleted! Subscription ID: "+subscriptionDeleted.Id);
                    // YourDbManager.DeactivateUserSubscription(subscriptionDeleted.Id);
                    break;

                // Add more event types as needed for your application logic
                // For a full list of events: https://stripe.com/docs/api/events/types

                default:
                    // Optionally log unhandled event types
                    Console.WriteLine("Unhandled event type: "+stripeEvent.Type);
                    break;
            }

            // Return a 200 OK to Stripe to acknowledge receipt of the event
            Response.StatusCode = 200;
            Response.End(); // Important to end the response for webhooks
        }
        else
        {
            // Respond with Method Not Allowed for non-POST requests
            Response.StatusCode = 405;
            Response.Write("Method Not Allowed");
            Response.End();
        }
    }
}

/*
 <%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>
 */