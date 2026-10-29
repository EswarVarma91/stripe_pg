<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="StripePGVBDis._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Buy Product</h2>

    <label>Promo Code:</label>
    <asp:TextBox ID="txtPromo" runat="server" />

    <br /><br />

    <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Payment"
                OnClick="btnCheckout_Click" />
    </form>
</body>
</html>
