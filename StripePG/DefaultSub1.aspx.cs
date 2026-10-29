using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/*
 * Live
 publish key  = pk_live_51JZp2DEVX74DuW0sWB5a8XwZxOVpiVc8aZnZNi8jfEXPHfK9YRBPfbi6nRiBjI85QRLUfBLJ3ieZb280SrEoupNX00b0wZFaAj
 secret key   = [live Stripe key removed]

 * Test
 publish key = pk_test_51JZp2DEVX74DuW0sYTnS3qa5lFMJvftpaf8L8gO9jGIdwZdXvZtjMLg6FMQfBSAgDx6FdzQOXEsBiFeQpWzI2Ifj00vKFW45Kh
 Secret Key  = [test secret key removed - see LOCAL_STRIPE_KEYS.txt]
 */
public partial class DefaultSub1 : System.Web.UI.Page
{
    protected string StripePublishableKey { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        StripePublishableKey = "pk_test_51JZp2DEVX74DuW0sYTnS3qa5lFMJvftpaf8L8gO9jGIdwZdXvZtjMLg6FMQfBSAgDx6FdzQOXEsBiFeQpWzI2Ifj00vKFW45Kh";
    }
}