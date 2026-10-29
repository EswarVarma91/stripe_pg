Imports Stripe
Partial Class Default2
    Inherits System.Web.UI.Page

    Private Sub Default2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not IsNothing(Request.QueryString("payment_intent")) Then
                    Dim res = Request.RequestContext
                    Dim str As String = Request.QueryString("TransactionNo")
                    str = Request.QueryString("payment_intent_client_secret")
                    Session("PaymentIntent") = Request.QueryString("payment_intent")
                    str = Request.QueryString("ClientSecret")
                End If

            End If
            '            ? Request.QueryString
            '{payment_intent=pi_3L07l7FJNRzQ3zKq0KU6kYcE&payment_intent_client_secret=pi_3L07l7FJNRzQ3zKq0KU6kYcE_secret_8CCLNju717B0AnMyxCn5MlPbT&redirect_status=succeeded}
            '    AllKeys: {Length=3}
            '    Count: 3 /// pi_3L07l7FJNRzQ3zKq0KU6kYcE
            '    IsReadOnly: True
            '    Keys: {System.Collections.Specialized.NameObjectCollectionBase.KeysCollection}
            '    Results View: Expanding the Results View will enumerate the IEnumerable
            Dim x = 0
            x = x + 1
            'request.UrlReferrer.Authority
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRefund.Click
        StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")
        'Dim options = New RefundCreateOptions With {
        '    .PaymentIntent = Session("PaymentIntent")
        '}

        Dim options = New RefundCreateOptions With {
            .PaymentIntent = txtPaymentIntent.Text
        }

        Dim service = New RefundService()
        Dim abc = service.Create(options)
        Dim i As Integer = 0

    End Sub
    Protected Sub btnGetPaymentIntentDetails_Click(sender As Object, e As EventArgs) Handles btnGetPaymentIntentDetails.Click
        Try
            StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")

            Dim service = New PaymentIntentService()
            Dim objAbc = service.Get(txtPaymentIntent.Text)
            Dim strAbc As String = objAbc.ToJson

            Dim obj = Newtonsoft.Json.JsonConvert.DeserializeObject(strAbc)
            Dim abc As PaymentIntentRS = Newtonsoft.Json.JsonConvert.DeserializeObject(Of PaymentIntentRS)(strAbc)
            'Dim 
            Dim j As Integer = 0
        Catch ex As Exception
            Dim strEr As String = ex.Message
        End Try
        Dim i As Integer = 0
    End Sub
End Class
