<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubscriptionCancel.aspx.cs" Inherits="SubscriptionCancel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscription Cancelled</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; text-align: center; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #f44336; border-radius: 8px; background-color: #ffe6e6; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { color: #f44336; }
        p { color: #333; }
        a { color: #6772E5; text-decoration: none; }
        a:hover { text-decoration: underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Subscription Cancelled</h1>
            <p>You have cancelled the subscription process.</p>
            <p>If you change your mind, you can <a href="Subscribe.aspx">try again</a>.</p>
        </div>
    </form>
</body>
</html>
