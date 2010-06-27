using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    public partial class QiblahCalcMobile : System.Web.UI.MobileControls.MobilePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                DoIt();
            }
        }

        protected void cmdGo_Click(object sender, EventArgs e)
        {
            DoIt();
        }

        private void DoIt()
        {
            try
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
            catch (Exception ex)
            {
                lblResult.Text = "Error finding Qiblah for your location.";
                lblResult.Visible = true;
            }
        }
    }
}