Imports Stripe
Imports Stripe.Checkout
Imports System
Imports System.Collections.Generic
Imports StripePaymentGateway.Models
Imports System.Data
Imports System.Data.SqlClient

Partial Class StripePG
    Inherits System.Web.UI.Page
    'Dim objSQL As New SQlCode
    Dim ProductName As String = ""
    Dim City As String = "" 'hyd
    Dim Checkindate As Date = Now.Date '08/27/2020
    Dim ItemType As String = "" 'package
    Dim dtHead As New DataTable
    Dim Description As String = "" 'package
    Dim IsDepositAmout As Boolean = False
    Dim IsSplitAmount As Boolean = False
    Dim iDepositAmount As Decimal = 0
    Dim RemainingAmnt As Decimal = 0
    Dim PayableDays As Double = 90


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim stripeAPIkey As String = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")
            'If TTUtilities.OWNERID = SQlCode.Administrator.STITCHINHEAVENLIVE Then
            '    stripeAPIkey = "[live Stripe key removed]"
            'ElseIf TTUtilities.OWNERID = SQlCode.Administrator.JOYIZMLIVE Then
            '    stripeAPIkey = "[live Stripe key removed]"
            'End If
            'stripeAPIkey = "[live Stripe key removed]" 'TTUtilities.StripeAPIKey
            Session("StripeCurrency") = "CAD"
            Session("StripeAmount") = 20
            hdnAuthURL.Value = Request.Url.Authority ' for stripe PG return url
            Dim mobileApp As Boolean = False
            If Not IsNothing(Session("IsMobileApp")) Then
                If Session("IsMobileApp") Then
                    mobileApp = True
                End If
            End If
            If mobileApp Then
                Dim cssMobileAppView As New HtmlLink
                cssMobileAppView.Href = "Stylesheets/MobileAppView.css"
                cssMobileAppView.Attributes.Add("rel", "stylesheet")
                divHead.Controls.Add(cssMobileAppView)
            Else
                Dim cssOverwrite As New HtmlLink
                cssOverwrite.Href = "Stylesheets/OverWrite.css"
                cssOverwrite.Attributes.Add("rel", "stylesheet")
                divHead.Controls.Add(cssOverwrite)
            End If
            If Not IsNothing(Request.QueryString("Success")) Then
                If Request.QueryString("Success") = "PaymentSubscriptionSuccess" Then
                    PaymentSubscriptionSuccess(stripeAPIkey)
                End If
            ElseIf Not IsNothing(Request.QueryString("Cancel")) Then
                If Request.QueryString("Cancel") = "CancelPaymentSubscription" Then
                    CancelPaymentSubscription()
                End If

            Else
                If Not IsNothing(Session("Package")) Then
                    dtHead = Session("Package")
                    If Not IsNothing(dtHead.Rows(0).Item("PACKAGENAME")) Then
                        ProductName = dtHead.Rows(0).Item("PACKAGENAME")
                    End If
                    If Not IsNothing(dtHead.Rows(0).Item("CHECKINDATE")) Then
                        Checkindate = CDate(dtHead.Rows(0).Item("CHECKINDATE"))
                    End If
                    If Not IsNothing(dtHead.Rows(0).Item("CITY")) Then
                        City = dtHead.Rows(0).Item("CITY")
                    End If
                    If Not IsNothing(dtHead.Rows(0).Item("BROPACKDETAILID")) Then
                        Dim cmd As New SqlCommand
                        Dim dsPayablebeforeDays As New DataSet
                        cmd = New SqlCommand("SELECT PAYBALEBEFOREDAYS FROM TRES_BROPACKDETAIL WHERE BROPACKDETAILID=@BROPACKDETAILID")
                        cmd.Parameters.AddWithValue("@BROPACKDETAILID", dtHead.Rows(0).Item("BROPACKDETAILID"))
                        'dsPayablebeforeDays = objSQL.GetData(cmd)
                        'If dsPayablebeforeDays.Tables(0).Rows.Count Then
                        '    If Not IsDBNull(dsPayablebeforeDays.Tables(0).Rows(0).Item("PAYBALEBEFOREDAYS")) Then
                        '        If dsPayablebeforeDays.Tables(0).Rows(0).Item("PAYBALEBEFOREDAYS").ToString.Length > 0 Then
                        '            PayableDays = dsPayablebeforeDays.Tables(0).Rows(0).Item("PAYBALEBEFOREDAYS")
                        '        End If
                        '    End If
                        'End If
                    End If
                    If Not IsNothing(Session("IsPackageOnly")) Then
                        If Session("IsPackageOnly") = True Then
                            IsDepositAmout = True
                            If Not IsNothing(dtHead.Rows(0).Item("DepositAmount")) Then
                                iDepositAmount = dtHead.Rows(0).Item("DepositAmount")
                                RemainingAmnt = Val(GetRemainingAmount(dtHead.Rows(0).Item("TRESPACKBOOKINGID"), iDepositAmount, Session("StripeAmount")))
                                lblDepositPay.Text = iDepositAmount
                                lblDepositPayCurr.Text = Session("StripeCurrency")
                                lblPendingAmt.Text = RemainingAmnt
                                lblPendingAmtCurr.Text = Session("StripeCurrency")

                                lblSecondAmt.Text = iDepositAmount
                                lblSecondAmtCurr.Text = Session("StripeCurrency")
                                lblSecondRemainingAmt.Text = RemainingAmnt
                                lblSecondRemainingAmtCurr.Text = Session("StripeCurrency")
                            End If
                            If dtHead.Columns.Contains("SplitAmount") Then
                                IsSplitAmount = True
                            End If
                        End If
                    End If
                    ItemType = "Package"
                    Description = "This is a Package Trip to" & City
                End If
                lblCurrency.Text = Session("BRCURRCODE")
                If Not IsPostBack Then

                    'Session("StripeAmount") = 25
                    'Session("StripeCurrency") = "usd"
                    StripeConfiguration.ApiKey = stripeAPIkey
                    'Dim options = New PaymentIntentCreateOptions With {
                    '    .Amount = (Convert.ToDecimal(Session("StripeAmount"))) * 100,
                    '    .Currency = Session("StripeCurrency"),
                    '    .Metadata = New Dictionary(Of String, String) From {
                    '        {"integration_check", "accept_a_payment"}
                    '    }
                    '}
                    Dim options = New PaymentIntentCreateOptions With {
                        .Amount = (Convert.ToDecimal(Session("StripeAmount"))) * 100,
                        .Currency = Session("StripeCurrency"),
                        .AutomaticPaymentMethods = New PaymentIntentAutomaticPaymentMethodsOptions With
                            {
                            .Enabled = True
                            }
                    }
                    If IsDepositAmout Then
                        StripeConfiguration.ApiKey = stripeAPIkey
                        'Dim optionsDepositAmount = New PaymentIntentCreateOptions With {
                        '    .Amount = Convert.ToDecimal(iDepositAmount) * 100,
                        '    .Currency = Session("StripeCurrency"),
                        '    .Metadata = New Dictionary(Of String, String) From {
                        '        {"integration_check", "accept_a_payment"}
                        '    }
                        '}
                        Dim optionsDepositAmount = New PaymentIntentCreateOptions With {
                            .Amount = Convert.ToDecimal(iDepositAmount) * 100,
                            .Currency = Session("StripeCurrency")
                        }
                        lblDepositPay.Text = iDepositAmount
                        lblDepositPayCurr.Text = Session("BRCURRCODE")
                        Session("StripeSplitAmount") = iDepositAmount & " " & Session("BRCURRCODE")
                        Dim serviceDeposit = New PaymentIntentService()

                        Dim paymentIntentDeposit = serviceDeposit.Create(optionsDepositAmount)
                        hdnClientSecretDeposit.Value = paymentIntentDeposit.ClientSecret
                    End If

                    Dim FromYear As DateTime = Convert.ToDateTime(Date.Now)
                    Dim ToYear As DateTime = Convert.ToDateTime(Checkindate)
                    'ToYear = Checkindate.AddDays(-90)
                    ToYear = Checkindate.AddDays(-PayableDays)

                    Dim Years As Integer = ToYear.Year - FromYear.Year
                    Dim month As Integer = ToYear.Month - FromYear.Month

                    Dim TotalMonths As Integer = (Years * 12) + month
                    Dim diffdate As Integer = ToYear.Day - FromYear.Day
                    If diffdate < 1 Then
                        TotalMonths = TotalMonths - 1
                    End If


                    Dim service = New PaymentIntentService()

                    Dim paymentIntent = service.Create(options)


                    hdnClientSecret.Value = paymentIntent.ClientSecret
                    hdnName.Value = Session("FName")
                    hdnTotalAmount.Value = options.Amount / 100
                    TotalAmount.Text = (options.Amount / 100) & "CAD"
                    Dim dtHotel As New DataTable
                    Dim isBunkHouse As Boolean = False
                    If Not IsNothing(Session("HOTELTABLE")) Then
                        dtHotel = CType(Session("HOTELTABLE"), DataTable)
                        'If TTUtilities.IsBunkHouse(dtHotel.Rows(0).Item("Propertyid")) Then
                        '    isBunkHouse = True
                        'End If
                    End If
                    If TotalMonths >= 1 AndAlso Not isBunkHouse Then
                        'ddlSplitOptions.Items.Insert(0, "Full Payment")
                        ddlSplitOptions.Items.Clear()
                        ddlSplitOptions.Items.Add(New ListItem("-Select Months-", 0))
                        For i = 1 To TotalMonths
                            ddlSplitOptions.Items.Add(New ListItem(i + 1, i + 1))
                        Next
                        divSplitpayment.Style("display") = ""
                        divNoSplitPayment.Style("display") = "none"
                        spanOR.Style("display") = ""
                        If IsDepositAmout Then
                            divDepositAmt.Style("display") = ""
                            Session("AllowDepositEmail") = True
                            spnFirstMsg.Style("display") = ""
                            spnSecondMsg.Style("display") = "none"
                        End If
                    Else
                        spanOR.Style("display") = "none"
                        divSplitpayment.Style("display") = "none"
                        divNoSplitPayment.Style("display") = "none"
                        span1.Style("display") = "none"
                    End If
                    If IsSplitAmount Then
                        DivFullPaymt.Style("display") = "none"
                        spanOR.Style("display") = "none"
                        divSplitpayment.Style("display") = "none"
                        divNoSplitPayment.Style("display") = "none"
                        span1.Style("display") = "none"

                        spnFirstMsg.Style("display") = "none"
                        spnSecondMsg.Style("display") = ""
                    End If
                End If
                'aSplitPaymnet.HRef = "/home/SplitPayment?amount=" & options.Amount
            End If
        Catch ex As Exception
            'objSQL.CreateLog(ex.Message & "|" & ex.StackTrace)
            Dim str = ex.Message
        End Try
    End Sub


    Protected Sub ddlSplitOptions_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Try
        '    Dim stripeAPIkey As String = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")
        '    'If TTUtilities.OWNERID = SQlCode.Administrator.STITCHINHEAVENLIVE Then
        '    '    stripeAPIkey = "[live Stripe key removed]"
        '    'ElseIf TTUtilities.OWNERID = SQlCode.Administrator.JOYIZMLIVE Then
        '    '    stripeAPIkey = "[live Stripe key removed]"
        '    'End If
        '    stripeAPIkey = TTUtilities.StripeAPIKey
        '    lnkSplitPayment.Style("display") = ""
        '    Dim PaymentMonths As Integer = ddlSplitOptions.SelectedItem.Value
        '    Dim SplitAmount As Double = Double.Parse(Session("StripeAmount"))
        '    'Dim objpaymentOptions As IList(Of StripePaymentGateway.Models.PaymentOptions) = New List(Of StripePaymentGateway.Models.PaymentOptions)()
        '    'Dim objPO As New StripePaymentGateway.Models.PaymentOptions
        '    'objPO.NumberOfMonths = 3
        '    'objPO.Amount = Val(10) / 3
        '    'objpaymentOptions.Add(objPO)

        '    'objpaymentOptions.Add(New StripePaymentGateway.Models.PaymentOptions With {.NumberOfMonths = 3, .Amount = Val(10) / 3})
        '    Dim objPaymentoptions As StripePaymentGateway.Models.PaymentOptions = New StripePaymentGateway.Models.PaymentOptions
        '    objPaymentoptions.Amount = (SplitAmount * 100) / PaymentMonths
        '    objPaymentoptions.NumberOfMonths = PaymentMonths

        '    Dim paymentOptionsViewModel As StripePaymentGateway.Models.PaymentOptionsViewModel = New StripePaymentGateway.Models.PaymentOptionsViewModel With {
        '        .PaymentOptions = objPaymentoptions,
        '        .TotalAmount = SplitAmount * 100
        '    }
        '    StripeConfiguration.ApiKey = stripeAPIkey
        '    Dim productService = New ProductService()
        '    Dim productOptions = New ProductCreateOptions With {
        '    .Name = ItemType,
        '    .Description = Description
        '     }
        '    Dim product = productService.Create(productOptions)
        '    StripeConfiguration.ApiKey = stripeAPIkey
        '    Dim customerOptions = New CustomerCreateOptions With {
        '        .Email = Session("Email"),
        '        .Name = Session("FName"),
        '        .Phone = Session("Phone"),
        '        .Description = Description
        '    }
        '    Dim customerService = New CustomerService()
        '    Dim customer = customerService.Create(customerOptions)
        '    Dim priceService = New PriceService()
        '    Dim priceOptions = New PriceCreateOptions With {
        '        .Product = product.Id,
        '        .UnitAmount = (Convert.ToDecimal(TotalAmount.Text) / PaymentMonths) * 100,
        '        .Currency = Session("StripeCurrency"),
        '        .Recurring = New PriceRecurringOptions With {
        '            .Interval = "month",
        '            .IntervalCount = 1
        '        }
        '    }
        '    Dim price = priceService.Create(priceOptions)
        '    Dim objsessionOptions = New SessionCreateOptions With {
        '        .Customer = customer.Id,
        '        .PaymentMethodTypes = New List(Of String) From {
        '            "card"
        '        },
        '        .LineItems = New List(Of SessionLineItemOptions) From {
        '            New SessionLineItemOptions With {
        '                .Price = price.Id,
        '                .Quantity = 1
        '            }
        '        },
        '        .Mode = "subscription",
        '        .SuccessUrl = Request.Url.OriginalString + "?Success=PaymentSubscriptionSuccess&TotAmount=" & Double.Parse(Session("StripeAmount")) & "&SplitAmount= " & objPaymentoptions.Amount / 100 & "&PaymentMonths=" & PaymentMonths & "",
        '        .CancelUrl = Request.Url.OriginalString + "?Cancel=CancelPaymentSubscription"
        '    }

        '    Dim objsessionService = New SessionService()
        '    Dim objsession As Stripe.Checkout.Session = objsessionService.Create(objsessionOptions)

        '    hdnSessionId.Value = objsession.Id
        '    Session("StripeSessionID") = objsession.Id
        '    Session("StripePriceID") = price.Id
        '    Session("StripecustomerID") = customer.Id
        '    Session("StripeproductID") = product.Id
        '    txtPaymentoptions.Text = "For " & PaymentMonths & " Months, you will be paying " & objPaymentoptions.Amount / 100 & " " & Session("BRCURRCODE")

        'Catch ex As Exception
        '    objSQL.CreateLog(ex.Message & "|" & ex.StackTrace)
        'End Try

    End Sub
    Public Sub PaymentSubscriptionSuccess(ByVal stripeAPIkey As String)
        'Try
        '    Dim PaymentMonths As Integer = Request.QueryString("PaymentMonths")
        '    Dim session_id As String = Session("StripeSessionID")
        '    StripeConfiguration.ApiKey = stripeAPIkey
        '    Dim service = New SessionService()
        '    Dim objsession = service.[Get](session_id)
        '    'Dim Transservice = New TransactionService()
        '    Dim Transactionumber As String = DateTime.Now.ToString("ddMMyyHHmmss")
        '    Dim cancellationDate As DateTime = DateTime.Now.AddMonths(PaymentMonths - 1).AddDays(1)
        '    Dim options = New SubscriptionUpdateOptions With {
        '        .CancelAt = cancellationDate
        '    }
        '    Dim subscriptionService = New SubscriptionService()
        '    Dim subscription = subscriptionService.Update(objsession.SubscriptionId, options)
        '    Dim cmd As New SqlCommand
        '    cmd = New SqlCommand("INSERT INTO TRES_StripPGPaidInfo(AdminId,ProductId,PriceId,CustomerId,SessionId,SubscriptionId,TransactionNo) VALUES(@AdminId,@ProductId,@PriceId,@CustomerId,@SessionId,@SubscriptionId,@TransactionNo)")
        '    cmd.Parameters.AddWithValue("@ADMINID", Session("OWNERID"))
        '    cmd.Parameters.AddWithValue("@ProductId", Session("StripeproductID"))
        '    cmd.Parameters.AddWithValue("@PriceId", Session("StripePriceID"))
        '    cmd.Parameters.AddWithValue("@CustomerId", Session("StripecustomerID"))
        '    cmd.Parameters.AddWithValue("@SessionId", Session("StripeSessionID"))
        '    cmd.Parameters.AddWithValue("@SubscriptionId", objsession.SubscriptionId)
        '    cmd.Parameters.AddWithValue("@TransactionNo", Transactionumber)
        '    If objSQL.GetInsUpdDel(cmd) Then
        '    Else
        '        objSQL.CreateLog("StripePgPaidinfo insertion failed.Booking completed")
        '    End If
        '    Session("AllowDepositEmail") = False
        '    Response.Redirect("review.aspx?PG=Stripe&TransactionNo=" & Transactionumber & "&TotAmount=" & Request.QueryString("TotAmount") & "&SplitAmount=" & Request.QueryString("SplitAmount") & "")
        'Catch ex As Exception
        '    objSQL.CreateLog(ex.Message & "|" & ex.StackTrace)
        'End Try


    End Sub
    Public Sub CancelPaymentSubscription()
        'Try
        '    TTUtilities.ErrorCode = 5
        '    Response.Redirect(URLRewrite.ChangeURL("ServiceError.aspx"))
        'Catch ex As Exception
        '    objSQL.CreateLog(ex.Message & "|" & ex.StackTrace)
        'End Try
    End Sub
    Function GetRemainingAmount(ByVal strbkgid As String, ByVal iDepositAmount As Decimal, ByVal itotalAmt As Decimal) As String
        Dim ds As New DataSet
        Dim str As String = ""
        Dim cmd As New SqlCommand
        'Try
        '    cmd = New SqlCommand("select sum(paymentamount) as paymentamount from tres_bookingpayments where productid=7 and sourceid=1 and bkgid=@bkgid")
        '    cmd.Parameters.AddWithValue("@bkgid", strbkgid)
        '    ds = objSQL.GetData(cmd)
        '    If ds.Tables(0).Rows.Count Then
        '        If Not IsDBNull(ds.Tables(0).Rows(0).Item("paymentamount")) Then
        '            str = itotalAmt - Val(ds.Tables(0).Rows(0).Item("paymentamount"))
        '        Else
        '            str = itotalAmt
        '        End If
        '    End If
        'Catch ex As Exception
        '    objSQL.CreateLog(ex.Message & "|" & ex.StackTrace)
        'End Try
        Return str
    End Function
End Class
