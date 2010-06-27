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
                    
                    if (string.IsNullOrEmpty(txtAddress.Text))
                    {
                        lblResult.Text = "Please enter a valid address!";
                        lblResult.Visible = true;
                    }
                    else
                    {
                        rptResults.Visible = true;
                        GeoCoder geoCoder = Utils.CreateGeoCoder(txtAddress.Text, false);
                        List<Placemark> placemarks = geoCoder.FetchExtendedGeoCodeData();
                        rptResults.DataSource = placemarks;
                        rptResults.DataBind();
                        lblResult.Visible = false;
                        //lblResult.Text = Utils.StringizeQiblah(Utils.CalculateQiblah(geoCoder));
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error finding Qiblah for your location. <br/> " + ex.Message;
                lblResult.Visible = true;
                rptResults.Visible = false;
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
