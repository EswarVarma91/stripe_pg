Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnCheckout_Click(sender As Object, e As EventArgs)
        Dim promo As String = txtPromo.Text.Trim()
        Response.Redirect("Checkout.aspx?promo=" & promo)
    End Sub

End Class