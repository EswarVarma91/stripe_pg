<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubscriptionSuccess.aspx.cs" Inherits="SubscriptionSuccess"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscription Success</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; text-align: center; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #4CAF50; border-radius: 8px; background-color: #e6ffe6; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { color: #4CAF50; }
        p { color: #333; }
        a { color: #6772E5; text-decoration: none; }
        a:hover { text-decoration: underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Subscription Successful!</h1>
            <p>Thank you for subscribing. Your account has been activated.</p>
            <p>You can now go to your <a href="Default.aspx">Dashboard</a>.</p>
            <asp:Literal ID="litPlan" runat="server"></asp:Literal>
            <asp:Literal ID="litRenewal" runat="server"></asp:Literal>
            <asp:Literal ID="litSessionId" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
