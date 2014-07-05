using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!Page.IsPostBack) { 
                 if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"])) // This is an unauthorized, authenticated request...
                     Response.Redirect("~/Account/UnauthorizedAccess.aspx"); 
             }

            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }
    }
}
