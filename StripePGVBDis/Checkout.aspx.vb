Imports Stripe
Imports Stripe.Checkout
Imports System
Imports System.Linq

Public Class Checkout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings("StripeSecretKey")

        Dim promoCode As String = Request.QueryString("promo")

        Dim options As New SessionCreateOptions With {
            .PaymentMethodTypes = New List(Of String) From {"card"},
            .Mode = "payment",
            .SuccessUrl = Request.Url.GetLeftPart(UriPartial.Authority) & "/Success.aspx?session_id={CHECKOUT_SESSION_ID}",
            .CancelUrl = Request.Url.GetLeftPart(UriPartial.Authority) & "/Cancel.aspx",
            .AllowPromotionCodes = True,
            .LineItems = New List(Of SessionLineItemOptions) From {
                New SessionLineItemOptions With {
                    .Quantity = 1,
                    .PriceData = New SessionLineItemPriceDataOptions With {
                        .Currency = "usd",
                        .UnitAmount = 2000, ' $20.00
                        .ProductData = New SessionLineItemPriceDataProductDataOptions With {
                            .Name = "Sample Product"
                        }
                    }
                }
            }
        }

        '' ===== APPLY PROMO CODE IF PROVIDED =====
        'If Not String.IsNullOrEmpty(promoCode) Then
        '    Try
        '        Dim promoService As New PromotionCodeService()
        '        Dim promoList = promoService.List(New PromotionCodeListOptions() With {
        '            .Code = promoCode,
        '            .Limit = 1
        '        })

        '        Dim promo = promoList.FirstOrDefault()

        '        If promo IsNot Nothing AndAlso promo.Active = True Then
        '            options.Discounts = New List(Of SessionDiscountOptions) From {
        '                New SessionDiscountOptions With {
        '                    .PromotionCode = promo.Id
        '                }
        '            }
        '        End If

        '    Catch ex As Exception
        '        ' Optional: log errors
        '    End Try
        'End If

        ' ===== CREATE CHECKOUT SESSION =====
        Dim service As New SessionService()
        Dim session As Session = service.Create(options)

        ' Redirect to Stripe Checkout
        Response.Redirect(session.Url, False)
    End Sub

End Class