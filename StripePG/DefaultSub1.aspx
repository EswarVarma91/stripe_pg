<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultSub1.aspx.cs" Inherits="DefaultSub1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="/subscribe" method="POST">
    <script
        src="https://checkout.stripe.com/checkout.js" class="stripe-button"
        data-key="<%= StripePublishableKey %>"
        data-name="Your Subscription Service"
        data-description="Subscribe to our awesome plan"
        data-amount="1000" data-currency="USD" data-panel-label="Subscribe Now"
        data-allow-remember-me="false"
        data-email="<%= User.Identity.IsAuthenticated ? User.Identity.Name : "" %>" data-plan="price_1RQLLwEVX74DuW0s6YoDMaWh"> </script>
</form>

    <form id="form1" runat="server">
        <div>
        </div>
    </form>

</body>
</html>
