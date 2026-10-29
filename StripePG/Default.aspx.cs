//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;


//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualBasic;
//using Stripe;
//using Stripe.Checkout;
//using System.Data;
//using System.Data.SqlClient;

//public partial class _Default : System.Web.UI.Page
//{

//    //private SQlCode objSQL = new SQlCode();
//    private string ProductName = "";
//    private string City = ""; // hyd
//    private DateTime Checkindate = DateTime.Now.Date; // 08/27/2020
//    private string ItemType = ""; // package
//    private DataTable dtHead = new DataTable();
//    private string Description = ""; // package
//    private bool IsDepositAmout = false;
//    private bool IsSplitAmount = false;
//    private decimal iDepositAmount = 0;
//    private decimal RemainingAmnt = 0;
//    private double PayableDays = 90;


//    protected void Page_Load(object sender, EventArgs e)
//    {
//        'try
//        '{
//        '    string stripeAPIkey = System.Configuration.ConfigurationManager.AppSettings["StripeSecretKey"];
           
//        '    if ((Request.QueryString["Success"]) != null)
//        '    {
//        '        //if (System.Web.UI.Page.Request.QueryString["Success"] == "PaymentSubscriptionSuccess")
//        '        //    PaymentSubscriptionSuccess(stripeAPIkey);
//        '    }
//        '    else if (Request.QueryString["Cancel"] != null)
//        '    {
//        '        //if (System.Web.UI.Page.Request.QueryString["Cancel"] == "CancelPaymentSubscription")
//        '        //    CancelPaymentSubscription();
//        '    }
//        '    else
//        '    {


//        '        ProductName = "Demo Package";
//        '        City = "Dubai";

//        '        ItemType = "package"; // 
//        '        dtHead = new DataTable();
//        '        Description = "package"; // 
//        '        IsDepositAmout = false;
//        '        IsSplitAmount = false;
//        '        iDepositAmount = 100;
//        '        RemainingAmnt = 0;
//        '        PayableDays = 0;



                
//        '        lblCurrency.Text = "USD";
//        '        if (! IsPostBack)
//        '        {

//        '            // Session("StripeAmount") = 25
//        '            // Session("StripeCurrency") = "usd"
//        '            StripeConfiguration.ApiKey = stripeAPIkey;
//        '            var options = new PaymentIntentCreateOptions()
//        '            {
//        '                Amount = (123) * 100,
//        '                Currency = "USD",
//        '                Metadata = new Dictionary<string, string>()
//        '                {
//        '                    {
//        '                        "integration_check",
//        '                        "accept_a_payment"
//        '                    }
//        '                }
//        '            };
//        '            //if (IsDepositAmout)
//        '            //{
//        '            //    StripeConfiguration.ApiKey = stripeAPIkey;
//        '            //    var optionsDepositAmount = new PaymentIntentCreateOptions()
//        '            //    {
//        '            //        Amount = Convert.ToDecimal(iDepositAmount) * 100,
//        '            //        Currency = System.Web.UI.Page.Session["StripeCurrency"],
//        '            //        Metadata = new Dictionary<string, string>()
//        '            //        {
//        '            //            {
//        '            //                "integration_check",
//        '            //                "accept_a_payment"
//        '            //            }
//        '            //        }
//        '            //    };
//        '            //    lblDepositPay.Text = iDepositAmount;
//        '            //    lblDepositPayCurr.Text = System.Web.UI.Page.Session["BRCURRCODE"];
//        '            //    System.Web.UI.Page.Session["StripeSplitAmount"] = iDepositAmount + " " + System.Web.UI.Page.Session["BRCURRCODE"];
//        '            //    var serviceDeposit = new PaymentIntentService();

//        '            //    var paymentIntentDeposit = serviceDeposit.Create(optionsDepositAmount);
//        '            //    hdnClientSecretDeposit.Value = paymentIntentDeposit.ClientSecret;
//        '            //}

//        '            //DateTime FromYear = Convert.ToDateTime(DateTime.Now);
//        '            //DateTime ToYear = Convert.ToDateTime(Checkindate);
//        '            //// ToYear = Checkindate.AddDays(-90)
//        '            //ToYear = Checkindate.AddDays(-PayableDays);

//        '            //int Years = ToYear.Year - FromYear.Year;
//        '            //int month = ToYear.Month - FromYear.Month;

//        '            //int TotalMonths = (Years * 12) + month;
//        '            //int diffdate = ToYear.Day - FromYear.Day;
//        '            //if (diffdate < 1)
//        '            //    TotalMonths = TotalMonths - 1;


//        '            var service = new PaymentIntentService();

//        '            var paymentIntent = service.Create(options);


//        '            hdnClientSecret.Value = paymentIntent.ClientSecret;
//        '            hdnTotalAmount.Value = (options.Amount/ (double)100).ToString();

//        '            TotalAmount.Text = (options.Amount / (double)100).ToString();
//        '            //DataTable dtHotel = new DataTable();
//        '            //bool isBunkHouse = false;
//        '            //if (!Information.IsNothing(System.Web.UI.Page.Session["HOTELTABLE"]))
//        '            //{
//        '            //    dtHotel = (DataTable)System.Web.UI.Page.Session["HOTELTABLE"];
//        '            //    if (TTUtilities.IsBunkHouse(dtHotel.Rows[0].Item["Propertyid"]))
//        '            //        isBunkHouse = true;
//        '            //}
//        '            //if (TotalMonths >= 1 && !isBunkHouse)
//        '            //{
//        '            //    // ddlSplitOptions.Items.Insert(0, "Full Payment")
//        '            //    ddlSplitOptions.Items.Clear();
//        '            //    ddlSplitOptions.Items.Add(new ListItem("-Select Months-", 0));
//        '            //    for (var i = 1; i <= TotalMonths; i++)
//        '            //        ddlSplitOptions.Items.Add(new ListItem(i + 1, i + 1));
//        '            //    divSplitpayment.Style("display") = "";
//        '            //    divNoSplitPayment.Style("display") = "none";
//        '            //    spanOR.Style("display") = "";
//        '            //    if (IsDepositAmout)
//        '            //    {
//        '            //        divDepositAmt.Style("display") = "";
//        '            //        System.Web.UI.Page.Session["AllowDepositEmail"] = true;
//        '            //        spnFirstMsg.Style("display") = "";
//        '            //        spnSecondMsg.Style("display") = "none";
//        '            //    }
//        '            //}
//        '            //else
//        '            {
//        '                spanOR.Style["display"] = "none";
//        '                divSplitpayment.Style["display"] = "none";
//        '                divNoSplitPayment.Style["display"] = "";
//        '                span1.Style["display"] = "none";
//        '            }
//        '            //if (IsSplitAmount)
//        '            //{
//        '            //    DivFullPaymt.Style("display") = "none";
//        '            //    spanOR.Style("display") = "none";
//        '            //    divSplitpayment.Style("display") = "none";
//        '            //    divNoSplitPayment.Style("display") = "none";
//        '            //    span1.Style("display") = "none";

//        '            //    spnFirstMsg.Style("display") = "none";
//        '            //    spnSecondMsg.Style("display") = "";
//        '            //}
//        '        }
//        '    }
//        '}
//        'catch (Exception ex)
//        '{
            
//        '}
//    }
//}