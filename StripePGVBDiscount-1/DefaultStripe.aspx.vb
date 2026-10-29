Imports Stripe

Partial Class DefaultStripe
    Inherits System.Web.UI.Page

    Private Sub DefaultStripe_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim stripeAPIkey As String = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")
        'stripeAPIkey = "[live Stripe key removed]"
        Dim Amt = (Convert.ToDecimal(25))
        TotalAmount.Text = Amt
        lblCurrency.Text = "USD" 'Session("BRCURRCODE")
        Session("StripeCurrency") = "USD"
        If Not IsPostBack Then

            StripeConfiguration.ApiKey = stripeAPIkey
            Dim options = New PaymentIntentCreateOptions With {
                        .Amount = Amt * 100,
                        .Currency = Session("StripeCurrency"),
                        .AutomaticPaymentMethods = New PaymentIntentAutomaticPaymentMethodsOptions With
                            {
                            .Enabled = True
                            }
                        }




            Dim service = New PaymentIntentService()

            Dim paymentIntent = service.Create(options)


            hdnClientSecret.Value = paymentIntent.ClientSecret
            hdnTotalAmount.Value = Amt
            TotalAmount.Text = Amt
        End If

    End Sub
End Class
