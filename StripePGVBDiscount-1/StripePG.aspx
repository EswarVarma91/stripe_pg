<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StripePG.aspx.vb" Inherits="StripePG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    
    
    
<head runat="server" id="divHead">
    <title></title>
    <meta name="ROBOTS" content="INDEX, FOLLOW" />   
    <meta http-equiv="cache-control" content="max-age=0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="-1" />
    <meta http-equiv="expires" content="Tue, 01 Jan 1980 11:00:00 GMT" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link rel="shortcut icon" type="image/png" href="Images/Icon.png" />

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
    <%--<uc1:StripePGUH runat="server" ID="StripePGUH" />--%>
    <div class="row">
        <span class="Hdr">Stripe Payment</span>
    <div class="col-md-4" id="DivFullPaymt" runat="server">       
        
        <span class="Txt AmtTxt">You are paying</span><asp:Label runat="server" id="TotalAmount" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblCurrency" CssClass="Curr"> </asp:Label>
        <span class="Txt">Click below button to pay in full</span>


        <form id="paymentform" class="sr-payment-form" >
            <div class="sr-combo-inputs-row">
                <div id="payment-element" >
                    <!-- Elements will create form elements here -->
                </div>
            </div>

            <button id="submit">
                <div class="spinner hidden" id="spinner"></div>
                <span id="button-text">Pay</span><span id="order-amount"></span>
            </button>

            <div id="error-message" class="sr-field-error" role="alert">
                <!-- Display error message to your customers here -->
            </div>
        </form>
        <%--<form id="paymentform" class="sr-payment-form" >  
            <div class="sr-combo-inputs-row">
                <div class="sr-input sr-card-element" id="card-element"></div>
            </div>
            <div class="sr-field-error" id="card-errors" role="alert"></div>
            <button id="submit">
                <div class="spinner hidden" id="spinner"></div>
                <span id="button-text">Pay</span><span id="order-amount"></span>
            </button>
            
           </form>--%>


         </div>
        <span runat="server" id="spanOR" class="OROption">(OR)</span>
        <form runat="server" id="SplitPaymentform"> 
            <asp:HiddenField id="hdnAuthURL" runat="server" Value="0" />
                <asp:HiddenField id="hdnTotalAmount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnClientSecret" runat="server" Value="0" />
            <asp:HiddenField ID="hdnClientSecretDeposit" runat="server" Value="0" />
            <asp:HiddenField ID="hdnName" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSessionId" runat="server" Value="0" />
            <div class="PaymentHolder" id="divSplitpayment" runat="server" Style="display:none">                
                <div class="InptHldr">
                    <asp:DropDownList ID="ddlSplitOptions" runat="server" AutoPostBack = "true" OnSelectedIndexChanged="ddlSplitOptions_SelectedIndexChanged" CssClass="Input">           
                    </asp:DropDownList>
                </div>              
                <span class="Txt" id="btnSplitPaymnet" runat="server">Select No.of Months for Split Payment(First payment should be completed now.)</span>         
    <asp:Label runat="server" ID="txtPaymentoptions" Text="" class="Txt"></asp:Label> <asp:Linkbutton runat="server" ID="lnkSplitPayment" Text="Pay In EMI" OnClientClick="RedirectToPayment()" Style="display:none" CssClass="Link">  </asp:Linkbutton> <asp:Label runat="server" ID="lblfirstpaymeny" Text="" ></asp:Label> </div>
            <div runat="server" id="divNoSplitPayment" Style="display:none" class="SplitPayment"> 
                <span runat="server" class="Txt">Sorry,You are not having an option for split payment</span>
            </div>
        </form>
        <div runat="server" id="divDepositAmt" style="display:none" class="DepositAmount">
    <span runat="server" id="span1" class="OROption">(OR)</span>
    <span class="Txt">Click the below button to make the Deposit Amount</span>
            <span class="Txt AmtTxt" id="spnFirstMsg" runat="server">You will be paying deposit amount <asp:Label runat="server" id="lblDepositPay" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblDepositPayCurr" CssClass="Curr"></asp:Label> now and <asp:Label runat="server" id="lblPendingAmt" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblPendingAmtCurr" CssClass="Curr"></asp:Label> later for which payment can be made from My Orders page</span>
            <span class="Txt AmtTxt" id="spnSecondMsg" runat="server">You are paying <asp:Label runat="server" id="lblSecondAmt" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblSecondAmtCurr" CssClass="Curr"></asp:Label> of  remaining <asp:Label runat="server" id="lblSecondRemainingAmt" CssClass="Amt"> </asp:Label><asp:Label runat="server" id="lblSecondRemainingAmtCurr" CssClass="Curr"></asp:Label></span>
        <form id="Depositpaymentform" class="sr-payment-form" >           
                        <div class="sr-combo-inputs-row">
                <div class="sr-input sr-card-element" id="Depositcard-element"></div>
            </div>
            <div class="sr-field-error" id="Depositcard-errors" role="alert"></div>
            <button id="submitD">
                <div class="spinner hidden" id="Depositspinner"></div>
                <span id="Depositbutton-text">Pay</span><span id="Depositorder-amount"></span>
            </button>            
           </form>


    </div>
</div>
     <script src="https://js.stripe.com/v3/"></script>
    <script>        //'pk_live_51JZp2DEVX74DuW0sWB5a8XwZxOVpiVc8aZnZNi8jfEXPHfK9YRBPfbi6nRiBjI85QRLUfBLJ3ieZb280SrEoupNX00b0wZFaAj'
        //var publishkey = 'pk_test_Q26AAvn1dAJ1exqocr3hfvqS00qL7ruFEx';
        var publishkey = 'pk_test_51JZp2DEVX74DuW0sYTnS3qa5lFMJvftpaf8L8gO9jGIdwZdXvZtjMLg6FMQfBSAgDx6FdzQOXEsBiFeQpWzI2Ifj00vKFW45Kh';//JOYiZM
        var stripe = Stripe(publishkey);
        var elements = stripe.elements();

        var stripeDeposit = Stripe(publishkey);
        var elementsDeposit = stripeDeposit.elements();

    </script>
    <script>
        var style = {
            base: {
                color: "#32325d",
            }
        };
    </script>
    <script>
        //FULL PAYMENT NEW VERSION CODE
         var ClientSecreat = document.getElementById('hdnClientSecret').value;
        var Name = document.getElementById('hdnName').value;

        console.log("this is the client secret:" & ClientSecreat);
        console.log("this is the client secret:" & Name);

        const options = {
            clientSecret: ClientSecreat,
            // Fully customizable with appearance API.
            appearance: {/*...*/ },
        };
        elements = stripe.elements(options);

        const paymentElement = elements.create('payment');
        paymentElement.mount('#payment-element');

        const form = document.getElementById('paymentform');

        var AuthURI = document.getElementById('hdnAuthURL').value

        form.addEventListener('submit', function (event) {
            console.log("payment process started");
            //changeLoadingState(true);
            event.preventDefault();
            //-----Confirm payment method 1-------
            stripe.confirmPayment({
                elements,
                confirmParams: {
                    // Return URL where the customer should be redirected after the PaymentIntent is confirmed.
                    return_url: AuthURI + '/Review.aspx?PG=Stripe',
                    //return_url: AuthURI + '/Default2.aspx?PG=Stripe',
                    payment_method_data: {
                        billing_details: {
                            name: Name
                        }
                    },
                },
            }).then(function (result) {
                if (result.error) {
                    // Inform the customer that there was an error.                   
                    console.log(result.error.message);
                } else {
                    if (result.paymentIntent.status === 'succeeded') {
                        /*window.location.href = "Default2.aspx?PG=Stripe&ClientSecret=" + result.paymentIntent.client_secret + "&PaymentIntentId=" + result.paymentIntent.id + "&TransactionNo=" + result.paymentIntent.created;*/
                    }
                }

            });
            //-----Confirm payment method 1-------

            //-----Confirm payment method 2-------

            //-----Confirm payment method 2-------


        });
    </script>
    <script>
        //full payment previous version 
        
        //var ClientSecreat = document.getElementById('hdnClientSecret').value;
        //var Name = document.getElementById('hdnName').value;

        //console.log("this is the client secret:" & ClientSecreat);
        //console.log("this is the client secret:" & Name);

        //var card = elements.create("card", { style: style });
        //card.mount("#card-element");

        //card.on('change', function (event) {
        //    var displayError = document.getElementById('card-errors');
        //    if (event.error) {
        //        displayError.textContent = event.error.message;
        //        return false;
        //    } else {
        //        displayError.textContent = '';
        //    }
        //});


        //var formFullPayment = document.getElementById('paymentform');

        //formFullPayment.addEventListener('submit', function (ev) {
        //    console.log("payment process started");
        //    changeLoadingState(true);
        //    ev.preventDefault();
        //    stripe.confirmCardPayment(ClientSecreat, {
        //        payment_method: {
        //            card: card,
        //            billing_details: {
        //                name: Name
        //            }
        //        }
        //    }).then(function (result) {
        //        changeLoadingState(false);
        //        console.log("payment process completed");
        //        console.log(result);
        //        if (result.error) {
        //            if (result.error.type != 'validation_error') {

        //                window.location.href = "ServiceError.aspx?Errorcode=5"

        //            }
        //            // Show error to your customer (e.g., insufficient funds)
        //            showError(result.error.message);
        //            console.log(result.error.message);
        //        } else {
        //            // The payment has been processed!
        //            if (result.paymentIntent.status === 'succeeded') {
        //                window.location.href = "Review.aspx?PG=Stripe&TransactionNo=" + result.paymentIntent.created;
        //                // Show a success message to your customer
        //                // There's a risk of the customer closing the window before callback
        //                // execution. Set up a webhook or plugin to listen for the
        //                // payment_intent.succeeded event that handles any business critical
        //                // post-payment actions.
        //            } else {
        //                window.location.href = "ServiceError.aspx?Errorcode=5"
        //            }
        //        }
        //    });
        //});

        //var changeLoadingState = function (isLoading) {
        //    if (isLoading) {
        //        document.querySelector("button").disabled = true;
        //        document.querySelector("#spinner").classList.remove("hidden");
        //        document.querySelector("#button-text").classList.add("hidden");
        //    } else {
        //        document.querySelector("button").disabled = false;
        //        document.querySelector("#spinner").classList.add("hidden");
        //        document.querySelector("#button-text").classList.remove("hidden");
        //    }
        //};

        //var showError = function (errorMsgText) {
        //    changeLoadingState(false);
        //    var errorMsg = document.querySelector(".sr-field-error");
        //    errorMsg.textContent = errorMsgText;
        //    setTimeout(function () {
        //        errorMsg.textContent = "";
        //    }, 4000);
        //};
    </script>
    <script>
                

        

        //Deposit code
        var ClientSecreatDeposit = document.getElementById('hdnClientSecretDeposit').value;
        var cardDeposit = elementsDeposit.create("card", { style: style });
        cardDeposit.mount("#Depositcard-element");
        cardDeposit.on('change', function (event) {
            var displayError = document.getElementById('Depositcard-errors');
            if (event.error) {
                displayError.textContent = event.error.message;
                return false;
            } else {
                displayError.textContent = '';
            }
        });

        var formDepositPayment = document.getElementById('Depositpaymentform');

        formDepositPayment.addEventListener('submit', function (ev) {
           
            console.log("payment process started");
            changeLoadingStateDeposit(true);
            ev.preventDefault();
            stripeDeposit.confirmCardPayment(ClientSecreatDeposit, {
                payment_method: {
                    card: cardDeposit,
                    billing_details: {
                        name: Name
                    }
                }
            }).then(function (result) {
                changeLoadingStateDeposit(false);
                console.log("payment process completed");
                console.log(result);
                if (result.error) {
                    if (result.error.type != 'validation_error') {

                        window.location.href = "ServiceError.aspx?Errorcode=5"
                    }
                    // Show error to your customer (e.g., insufficient funds)
                    showErrorDeposit(result.error.message);
                    console.log(result.error.message);
                } else {
                    // The payment has been processed!
                    if (result.paymentIntent.status === 'succeeded') {
                        window.location.href = "Review.aspx?PG=Stripe&TransactionNo=" + result.paymentIntent.created + "&SplitAmount= " + result.paymentIntent.amount/100;
                        // Show a success message to your customer
                        // There's a risk of the customer closing the window before callback
                        // execution. Set up a webhook or plugin to listen for the
                        // payment_intent.succeeded event that handles any business critical
                        // post-payment actions.
                    } else {
                        window.location.href = "ServiceError.aspx?Errorcode=5"
                    }
                }
            });
        });
        var changeLoadingStateDeposit = function (isLoading) {
            if (isLoading) {
                document.querySelector("button").disabled = true;
                document.querySelector("#Depositspinner").classList.remove("hidden");
                document.querySelector("#Depositbutton-text").classList.add("hidden");
            } else {
                document.querySelector("button").disabled = false;
                document.querySelector("#Depositspinner").classList.add("hidden");
                document.querySelector("#Depositbutton-text").classList.remove("hidden");
            }
        };

        var showErrorDeposit = function (errorMsgText) {
            changeLoadingStateDeposit(false);
            var errorMsg = document.querySelector(".sr-field-error");
            errorMsg.textContent = errorMsgText;
            setTimeout(function () {
                errorMsg.textContent = "";
            }, 4000);
        };
    </script>
    <script>
    //var stripe = Stripe('pk_test_Q26AAvn1dAJ1exqocr3hfvqS00qL7ruFEx');



        function RedirectToPayment() {
            stripe.redirectToCheckout({
                // Make the id field from the Checkout Session creation API response
                // available to this file, so you can provide it as parameter here
                // instead of the {{CHECKOUT_SESSION_ID}} placeholder.
                sessionId: document.getElementById('hdnSessionId').value
            }).then(function (result) {
                // If `redirectToCheckout` fails due to a browser or network
                // error, display the localized error message to your customer
                // using `result.error.message`.
            });
        }
  
    //$(document).ready(function () {
    //    $("#lnkSplitPayment").click(function () {
    //    console.log("clicked");
       
    //    });
    //});
    

    </script>
    <%--<uc1:StripePGLH runat="server" ID="StripePGLH" />--%>
</body>
</html>
