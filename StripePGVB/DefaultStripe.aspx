<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DefaultStripe.aspx.vb" Inherits="DefaultStripe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
<style type="text/css">

div.row{
    width: 98%;
    max-width: 500px;
    margin: 0 auto;
}
div.row span.Hdr{
    display: block;
    padding: 10px 0;
    font-weight: 600;
    font-size: 16px;
}
div.row div.col-md-4{
    border: 6px solid#ddd;
    padding: 20px 10px;
}
div.row div.col-md-4 span.Txt{
    display: block;   
    font-size: 13px;
}
div.row div.col-md-4 span.Txt.AmtTxt{
    display: inline-block;
}
div.row div.col-md-4 span.Amt{
    padding: 0 3px;
    font-weight: 600;
    display: inline-block;
}
div.PaymentHolder{
    display:  block;    
    padding: 30px 10px 20px 10px;
    border: 6px solid#ddd;
    margin-top: 20px;
}
span.Txt{
    line-height: 180%;
    padding: 5px 0;
    display: block;
}
div.InptHldr {    
    background-color: white;
    position: relative;
    height: 35px;
}
div.InptHldr > select.Input {
    width: 100%;
    padding: 0 5px;
    height: 100%;
    box-sizing: border-box;
    background-color: transparent;
    font-size: 13px;
    position: absolute;
    z-index: 6;
    cursor: pointer;
    outline: none;
    line-height: 140%;
    border: 1px solid #dddddd;
}
a.Link{
    text-align: center;
    margin-top: 0;
}

body {
    padding-top: 50px;
    padding-bottom: 20px;
}

.body-content {
    padding-left: 15px;
    padding-right: 15px;
}

.dl-horizontal dt {
    white-space: normal;
}

input,
select,
textarea {
    max-width: 280px;
}


:root {
    --body-color: rgb(247, 250, 252);
    --button-color: rgb(30, 166, 114);
    --accent-color: #0a721b;
    --link-color: #ffffff;
    --font-color: rgb(105, 115, 134);
    --body-font-family: -apple-system, BlinkMacSystemFont, sans-serif;
    --radius: 6px;
    --form-width: 400px;
}


* {
    box-sizing: border-box;
}

body {
    font-family: var(--body-font-family);
    font-size: 13px;
    -webkit-font-smoothing: antialiased;
}
span.OROption{
    display: block;
    padding: 20px 0 0 0;
    text-align: center;
    font-weight: bold;
}

.sr-root {
    display: flex;
    flex-direction: row;
    width: 100%;
    max-width: 980px;
    padding: 48px;
    align-content: center;
    justify-content: center;
    height: auto;
    min-height: 100vh;
    margin: 0 auto;
}

.sr-main {
    display: flex;
    flex-direction: column;
    justify-content: center;
    height: 100%;
    align-self: center;
    padding: 75px 50px;
    background: var(--body-color);
    width: var(--form-width);
    border-radius: var(--radius);
    box-shadow: 0px 0px 0px 0.5px rgba(50, 50, 93, 0.1), 0px 2px 5px 0px rgba(50, 50, 93, 0.1), 0px 1px 1.5px 0px rgba(0, 0, 0, 0.07);
}

.sr-field-error {
    color: var(--font-color);
    text-align: left;
    font-size: 13px;
    line-height: 17px;
    margin-top: 12px;
}


.sr-input,
input[type="text"] {
    border: 1px solid var(--gray-border);
    border-radius: var(--radius);
    padding: 5px 12px;
    height: 44px;
    width: 100%;
    transition: box-shadow 0.2s ease;
    background: white;
    -moz-appearance: none;
    -webkit-appearance: none;
    appearance: none;
}

    .sr-input:focus,
    input[type="text"]:focus,
    button:focus,
    .focused {
        box-shadow: 0 0 0 1px rgba(50, 151, 211, 0.3), 0 1px 1px 0 rgba(0, 0, 0, 0.07), 0 0 0 4px rgba(50, 151, 211, 0.3);
        outline: none;
        z-index: 9;
    }

    .sr-input::placeholder,
    input[type="text"]::placeholder {
        color: var(--gray-light);
    }

.sr-result {
    height: 44px;
    -webkit-transition: height 1s ease;
    -moz-transition: height 1s ease;
    -o-transition: height 1s ease;
    transition: height 1s ease;
    color: var(--font-color);
    overflow: auto;
}

    .sr-result code {
        overflow: scroll;
    }

    .sr-result.expand {
        height: 350px;
    }

.sr-combo-inputs-row {
    box-shadow: 0px 0px 0px 0.5px rgba(50, 50, 93, 0.1), 0px 2px 5px 0px rgba(50, 50, 93, 0.1), 0px 1px 1.5px 0px rgba(0, 0, 0, 0.07);
    border-radius: 7px;
}

/* Buttons and links */
button, a.Link {
    background: var(--accent-color);
    border-radius: var(--radius);
    color: white;
    border: 0;
    padding: 12px 16px;
    margin-top: 16px;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;
    display: block;
    box-shadow: 0px 4px 5.5px 0px rgba(0, 0, 0, 0.07);
    width: 100%;
}

    button:hover {
        filter: contrast(115%);
    }

    button:active {
        transform: translateY(0px) scale(0.98);
        filter: brightness(0.9);
    }

    button:disabled {
        opacity: 0.5;
        cursor: none;
    }

a {
    text-decoration: none;
    transition: all 0.2s ease;
}

    a:hover {
        filter: brightness(0.8);
    }

    a:active {
        filter: brightness(0.5);
    }


code,
pre {
    font-family: "SF Mono", "IBM Plex Mono", "Menlo", monospace;
    font-size: 12px;
}


.sr-card-element {
    padding-top: 12px;
}


@media (max-width: 720px) {
    .sr-root {
        flex-direction: column;
        justify-content: flex-start;
        padding: 48px 20px;
        min-width: 320px;
    }

    .sr-header__logo {
        background-position: center;
    }

    .sr-payment-summary {
        text-align: center;
    }

    .sr-content {
        display: none;
    }

    .sr-main {
        width: 100%;
        height: 305px;
        background: rgb(247, 250, 252);
        box-shadow: 0px 0px 0px 0.5px rgba(50, 50, 93, 0.1), 0px 2px 5px 0px rgba(50, 50, 93, 0.1), 0px 1px 1.5px 0px rgba(0, 0, 0, 0.07);
        border-radius: 6px;
    }
}



.spinner,
.spinner:before,
.spinner:after {
    border-radius: 50%;
}

.spinner {
    color: #ffffff;
    font-size: 22px;
    text-indent: -99999px;
    margin: 0px auto;
    position: relative;
    width: 20px;
    height: 20px;
    box-shadow: inset 0 0 0 2px;
    -webkit-transform: translateZ(0);
    -ms-transform: translateZ(0);
    transform: translateZ(0);
}

    .spinner:before,
    .spinner:after {
        position: absolute;
        content: "";
    }

    .spinner:before {
        width: 10.4px;
        height: 20.4px;
        background: var(--accent-color);
        border-radius: 20.4px 0 0 20.4px;
        top: -0.2px;
        left: -0.2px;
        -webkit-transform-origin: 10.4px 10.2px;
        transform-origin: 10.4px 10.2px;
        -webkit-animation: loading 2s infinite ease 1.5s;
        animation: loading 2s infinite ease 1.5s;
    }

    .spinner:after {
        width: 10.4px;
        height: 10.2px;
        background: var(--accent-color);
        border-radius: 0 10.2px 10.2px 0;
        top: -0.1px;
        left: 10.2px;
        -webkit-transform-origin: 0px 10.2px;
        transform-origin: 0px 10.2px;
        -webkit-animation: loading 2s infinite ease;
        animation: loading 2s infinite ease;
    }

@-webkit-keyframes loading {
    0% {
        -webkit-transform: rotate(0deg);
        transform: rotate(0deg);
    }

    100% {
        -webkit-transform: rotate(360deg);
        transform: rotate(360deg);
    }
}

@keyframes loading {
    0% {
        -webkit-transform: rotate(0deg);
        transform: rotate(0deg);
    }

    100% {
        -webkit-transform: rotate(360deg);
        transform: rotate(360deg);
    }
}

/* Animated form */

.sr-root {
    animation: 0.4s form-in;
    animation-fill-mode: both;
    animation-timing-function: ease;
}

.hidden {
    display: none;
}

@keyframes field-in {
    0% {
        opacity: 0;
        transform: translateY(8px) scale(0.95);
    }

    100% {
        opacity: 1;
        transform: translateY(0px) scale(1);
    }
}

@keyframes form-in {
    0% {
        opacity: 0;
        transform: scale(0.98);
    }

    100% {
        opacity: 1;
        transform: scale(1);
    }
}
    </style>
</head>
<body>
     <span class="Txt AmtTxt">You are paying</span><asp:Label runat="server" id="TotalAmount" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblCurrency" CssClass="Curr"> </asp:Label>
    <form id="paymentform" runat="server">
        <div class="sr-combo-inputs-row">
            <div id="payment-element">
                <!-- Elements will create form elements here -->
            </div>
        </div>


  <button id="submit">Submit</button>
  <div id="error-message">
    <!-- Display error message to your customers here -->
  </div>
 </div>


         <asp:HiddenField ID="hdnClientSecret" runat="server" Value="0" />
        <asp:HiddenField id="hdnTotalAmount" runat="server" Value="0" />
            <asp:HiddenField ID="hdnClientSecretDeposit" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSessionId" runat="server" Value="0" />
       
    </form>


        <script src="https://js.stripe.com/v3/"></script>
    <script>

        var stripe = Stripe('pk_test_Q26AAvn1dAJ1exqocr3hfvqS00qL7ruFEx');
        //var stripe = Stripe('pk_live_51JZp2DEVX74DuW0sWB5a8XwZxOVpiVc8aZnZNi8jfEXPHfK9YRBPfbi6nRiBjI85QRLUfBLJ3ieZb280SrEoupNX00b0wZFaAj');
        var elements = stripe.elements();
    </script>
    <script>

        var ClientSecreat = document.getElementById('hdnClientSecret').value;

        const options = {
            clientSecret: ClientSecreat,
            // Fully customizable with appearance API.
            appearance: {/*...*/},
        };

        // Set up Stripe.js and Elements to use in checkout form, passing the client secret obtained in step 2
        //const paymentElements = stripe.elements(options);
        elements = stripe.elements(options);

        // Create and mount the Payment Element
        const paymentElement = elements.create('payment');
        paymentElement.mount('#payment-element');

    </script>

    <script>
        const form = document.getElementById('paymentform');

        form.addEventListener('submit', function (event)  {
            console.log("payment process started");
            //changeLoadingState(true);
            event.preventDefault();

            stripe.confirmPayment({
                elements,
                confirmParams: {
                    // Return URL where the customer should be redirected after the PaymentIntent is confirmed.
                    return_url: 'http://localhost:61755/Default2.aspx?PG=Stripe',
                    payment_method_data: {
                        billing_details: {
                            name: 'Alex Pole',
                            email: '',
                        }
                    },
                },
            }).then(function(result) {
                if (result.error) {
                    // Inform the customer that there was an error.                   
                    console.log(result.error.message);
                } else {
                    if (result.paymentIntent.status === 'succeeded') {
                        window.location.href = "Default2.aspx?PG=Stripe&ClientSecret=" + result.paymentIntent.client_secret + "&PaymentIntentId=" + result.paymentIntent.id + "&TransactionNo=" + result.paymentIntent.created;
                    }
                }
                     
                 });
            /*****
            const error =  stripe.confirmPayment({
                //`Elements` instance that was used to create the Payment Element
                elements,
                confirmParams: {
                    return_url: 'Default2.aspx?PG=Stripe',
                },
            });

            if (error) {
                // This point will only be reached if there is an immediate error when
                // confirming the payment. Show error to your customer (for example, payment
                // details incomplete)
                const messageContainer = document.querySelector('#error-message');
                messageContainer.textContent = error.message;
            } else {
                // Your customer will be redirected to your `return_url`. For some payment
                // methods like iDEAL, your customer will be redirected to an intermediate
                // site first to authorize the payment, then redirected to the `return_url`.
            }

            *****/
        });
    </script>


</body>
</html>
