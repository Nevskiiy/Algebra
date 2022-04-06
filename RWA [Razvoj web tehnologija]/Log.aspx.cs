using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NevenIvanisic_RWA._21
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                HttpCookie cookie = new HttpCookie("username");
                cookie.Value = txtUsername.Text;
                //cookie.Expires = DateTime.Now.AddSeconds(30);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);

                Response.Redirect("~/Customer.aspx");
            }
        }
    }
}