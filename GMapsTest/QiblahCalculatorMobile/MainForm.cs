using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QiblahCalculatorMobile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuGo_Click(object sender, EventArgs e)
        {
            ResetResult();
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                SetStatus(StatusType.Error, "Please enter a valid address!");
            }
            QiblahCalcWS.QiblahCalculatorWS svc =
                new QiblahCalcWS.QiblahCalculatorWS();
            //double qiblah = double.NaN;
            //bool isResultSpecified = true;
            SetStatus(StatusType.Progress, "Finding Qiblah direction for your location...");
            //double qiblah = svc.FindQiblah(txtAddress.Text, false);
            string q = svc.FindQiblahString(txtAddress.Text, false);
            ResetStatus();
            ShowResult(q);
        }

        private delegate void ShowResultDelegate(string msg);
        private void ShowResultImpl(string msg)
        {
            lblQiblah.Text = msg;
            lblQiblah.Visible = true;
            this.Refresh();
        }

        private delegate void ResetResultDelegate();
        private void ResetResultImpl()
        {
            lblQiblah.Visible = false;
            this.Refresh();
        }

        private delegate void SetStatusDelegate(string msg);
        private void SetStatusImpl(string msg)
        {
            lblStatus.Text = msg;
            lblStatus.Visible = true;
            this.Refresh();
        }

        private delegate void ResetStatusDelegate();
        private void ResetStatusImpl()
        {
            lblStatus.Visible = false;
            this.Refresh();
        }
        
        private void ShowResult(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowResultDelegate(ShowResultImpl), new object[] { msg });
            }
            else ShowResultImpl(msg);
        }

        private void ResetResult()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ResetResultDelegate(this.ResetResultImpl));
            }
            else ResetResultImpl();
        }

        private void ResetStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ResetStatusDelegate(this.ResetStatusImpl));
            }
            else ResetStatusImpl();
        }

        private void SetStatus(StatusType statusType, string msg)
        {
            switch (statusType)
            {
                case StatusType.Error:
                    MessageBox.Show(msg, "QiblahCalculator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    break;
                case StatusType.Information:
                    MessageBox.Show(msg, "QiblahCalculator", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    break;
                case StatusType.Progress:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new SetStatusDelegate(this.SetStatusImpl), new object[] { msg });
                    }
                    else
                        SetStatusImpl(msg);
                    break;
                default:
                    break;
            }
        }
        private string StringizeQiblah(double qiblah)
        {
            string formatString = "##0.0#";
            if (qiblah < 0)
            {
                return Math.Abs(qiblah).ToString(formatString) + " degrees West of North";
            }
            return qiblah.ToString(formatString) + " degrees East of North";
        }
    }
    public enum StatusType
    {
        Error, Information, Progress, Question
    }
}