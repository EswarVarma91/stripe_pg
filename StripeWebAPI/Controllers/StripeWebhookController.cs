using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace StripeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StripeWebhookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            //SaveFileCriteria objSFC = new SaveFileCriteria
            //{
            //    AdminId = "213",
            //    FileExtension = "txt",
            //    FileName = "StripeTrace",
            //    strFileText = json
            //};
            //_utilityRepository.SaveFile(objSFC);
            //SaveFile(objSFC);

            try
            {
                // Retrieve the Stripe-Signature header
                var signatureHeader = Request.Headers["Stripe-Signature"];

                // Verify event by comparing signature
                string webhookSecret = _configuration["StripeWebhookSecret"] ?? string.Empty;
                Stripe.Event stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, webhookSecret, throwOnApiVersionMismatch: false);
                //stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, _webhookSecret);



                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        //Console.WriteLine($"PaymentIntent was successful! ID: {paymentIntent.Id}");
                        //objSFC.strFileText = $"PaymentIntentSucceeded - PaymentIntent was successful! ID: {paymentIntent.Id}";
                        //_utilityRepository.SaveFilePerDay(objSFC);
                        break;

                    case "checkout.session.completed":
                        var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                        //Console.WriteLine($"Checkout session completed! ID: {session.Id}");
                        //objSFC.strFileText = $"CheckoutSessionCompleted - Checkout session completed! ID: {session.Id}";
                        //_utilityRepository.SaveFilePerDay(objSFC);
                        break;

                   /* case Stripe.Events.InvoicePaid:
                        var invoice = stripeEvent.Data.Object as Invoice;
                        // This event fires when an invoice is paid (initial or recurring).
                        // You can confirm subscription status and update your database if needed.
                        string strTxt = "InvoicePaid - Invoice ID: " + invoice.Id.ToString() + "--" + "Customer ID: " + invoice.CustomerId.ToString();

                        //objSFC.strFileText = strTxt;
                        //_utilityRepository.SaveFilePerDay(objSFC);

                        // Check if invoice.SubscriptionId is available for linked subscriptions
                        // YourDbManager.UpdateSubscriptionPaymentStatus(invoice.SubscriptionId, "paid");
                        break;

                    case Stripe.Events.CustomerSubscriptionUpdated:
                        var subscriptionUpdated = stripeEvent.Data.Object as Subscription;
                        // This event fires when a subscription changes (e.g., plan change, trial ends).
                        //Console.WriteLine("Subscription Updated! Subscription ID: " + subscriptionUpdated.Id);
                        //Console.WriteLine("New Status: " + subscriptionUpdated.Status);                        

                        //objSFC.strFileText = "CustomerSubscriptionUpdated - Subscription ID: " + subscriptionUpdated.Id.ToString() + "--" + "New Status: " + subscriptionUpdated.Status.ToString(); ;
                        //_utilityRepository.SaveFilePerDay(objSFC);
                        // YourDbManager.UpdateSubscriptionStatus(subscriptionUpdated.Id, subscriptionUpdated.Status);
                        break;

                    case Stripe.Events.CustomerSubscriptionDeleted:
                        var subscriptionDeleted = stripeEvent.Data.Object as Subscription;
                        // This event fires when a subscription is cancelled.
                        //Console.WriteLine("Subscription Deleted! Subscription ID: " + subscriptionDeleted.Id);

                        //objSFC.strFileText = "CustomerSubscriptionDeleted - Subscription ID: " + subscriptionDeleted.Id.ToString();
                        //_utilityRepository.SaveFilePerDay(objSFC);
                        // YourDbManager.DeactivateUserSubscription(subscriptionDeleted.Id);
                        break;
                    // Add more case blocks for other event types you're interested in*/

                    default:
                        //Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                        //objSFC.strFileText = $"Unhandled event type: {stripeEvent.Type}";
                        //_utilityRepository.SaveFilePerDay(objSFC);
                        break;
                }

            }
            catch (StripeException e)
            {
                //Console.WriteLine($"Stripe error: {e.Message}");
                //objSFC.strFileText = $"Stripe error: {e.Message}";
                //_utilityRepository.SaveFilePerDay(objSFC);
                return BadRequest();
            }


            return Ok(); 
        }
    }
}
