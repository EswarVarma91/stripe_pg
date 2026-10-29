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

        /* Plan selection */
        .plans { display: flex; gap: 16px; justify-content: center; margin-top: 24px; }
        .plan-card {
            flex: 1; display: block; text-align: center; cursor: pointer;
            border: 2px solid #ddd; border-radius: 8px; padding: 16px 10px;
            transition: border-color 0.2s ease, box-shadow 0.2s ease, background-color 0.2s ease;
        }
        .plan-card:hover { border-color: #6772E5; }
        .plan-card:has(input:checked), .plan-card.selected {
            border-color: #6772E5; background-color: #f7f8ff;
            box-shadow: 0 2px 8px rgba(103,114,229,0.25);
        }
        .plan-radio { display: block; margin: 0 auto 8px; }
        .plan-name { display: block; font-weight: bold; color: #333; }
        .plan-price { display: block; font-size: 28px; color: #333; margin: 8px 0 4px; }
        .plan-period { display: block; color: #666; font-size: 14px; }
        .plan-badge {
            display: inline-block; margin-top: 10px; background-color: #4CAF50;
            color: #fff; font-size: 12px; padding: 3px 10px; border-radius: 10px;
        }
        .error { display: block; text-align: center; color: #d33; margin-top: 14px; }
        .note { font-size: 13px; color: #888; margin-top: 18px; }

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
            <p>Get access to exclusive features. Choose monthly or yearly billing.</p>

            <div class="plans" onchange="highlightPlans()">
                <label class="plan-card selected">
                    <asp:RadioButton ID="radMonthly" runat="server" GroupName="plan" Checked="true" CssClass="plan-radio" Text="" />
                    <span class="plan-name">Monthly</span>
                    <span class="plan-price"><%= MonthlyPriceText %></span>
                    <span class="plan-period">per month</span>
                </label>

                <label class="plan-card" id="pnlYearly" runat="server">
                    <asp:RadioButton ID="radYearly" runat="server" GroupName="plan" CssClass="plan-radio" Text="" />
                    <span class="plan-name">Yearly</span>
                    <span class="plan-price"><%= YearlyPriceText %></span>
                    <span class="plan-period">per year</span>
                    <span class="plan-badge"><%= YearlyBadgeText %></span>
                </label>
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>

            <div class="button-container">
                <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe Now" CssClass="stripe-button" OnClick="btnSubscribe_Click" />
            </div>

            <p class="note">Bills through Stripe. Cancel anytime from the customer portal.</p>
        </div>
    </form>

    <script type="text/javascript">
        // Highlights the selected plan card (works even where :has() is unsupported).
        function highlightPlans() {
            var cards = document.querySelectorAll('.plan-card');
            for (var i = 0; i < cards.length; i++) {
                var radio = cards[i].querySelector('input[type=radio]');
                if (radio && radio.checked) { cards[i].className = 'plan-card selected'; }
                else { cards[i].className = 'plan-card'; }
            }
        }
        highlightPlans();
    </script>
</body>
</html>
