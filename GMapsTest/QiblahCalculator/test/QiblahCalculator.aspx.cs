using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    public partial class QiblahCalculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsMobile(Request.UserAgent))
                {
                    Response.Redirect("~/QiblahCalcMobile.aspx");
                    return;
                }
                if (IsPostBack)
                {
                    lblResult.Visible = true;
                    if (string.IsNullOrEmpty(txtAddress.Text))
                    {
                        lblResult.Text = "Please enter a valid address!";
                    }
                    else
                    {
                        GeoCoder geoCoder = Utils.CreateGeoCoder(txtAddress.Text, false);
                        geoCoder.FetchGeoCodeData();
                        lblResult.Text = Utils.StringizeQiblah(Utils.CalculateQiblah(geoCoder));
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error finding Qiblah for your location.";
                lblResult.Visible = true;
            }
        }
        private bool IsMobile(string userAgent)
        {
            userAgent = userAgent.ToLower();
            return userAgent.Contains("iphone") ||
                 userAgent.Contains("ppc") ||
                 userAgent.Contains("windows ce") ||
                 userAgent.Contains("blackberry") ||
                 userAgent.Contains("opera mini") ||
                 userAgent.Contains("mobile") ||
                 userAgent.Contains("iemobile") ||
                 userAgent.Contains("palm") ||
                 userAgent.Contains("portable");
        }
    }
}
