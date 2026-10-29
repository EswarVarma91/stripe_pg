<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="Subscribe" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stripe Subscription</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { text-align: center; color: #333; }
        p { text-align: center; color: #666; }
        .button-container { text-align: center; margin-top: 30px; }
        .stripe-button {
            background-color: #6772E5;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .stripe-button:hover {
            background-color: #5469d4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Subscribe to Our Premium Plan</h1>
            <p>Get access to exclusive features for just $10.00/month.</p>
            <div class="button-container">
                <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe Now" CssClass="stripe-button" OnClick="btnSubscribe_Click" />
            </div>
        </div>
    </form>
</body>
</html>
