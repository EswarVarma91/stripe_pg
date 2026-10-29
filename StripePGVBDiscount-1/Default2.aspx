<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>PaymentIntent :</span><asp:TextBox ID="txtPaymentIntent" runat="server"></asp:TextBox> <br />
        <asp:Button ID="btnRefund" runat="server" Text="Refund" style="height: 26px" />
    </div>
        <asp:Button ID="btnGetPaymentIntentDetails" runat="server" Text="GetPaymentIntentDetails" />
    </form>
</body>
</html>
