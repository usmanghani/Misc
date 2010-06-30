using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using libpraytt;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;


namespace praytt
{
	/// <summary>
	/// Summary description for BugReport.
	/// </summary>
	public class BugReportDialog : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
        private Button button3;
        List<BugReport> reports = new List<BugReport>();

        public List<BugReport> Reports
        {
            get { return reports; }
        }

        public int ReportCount
        {
            get { return reports.Count; }

        }


		public BugReportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            reports.Clear();
            //this.location = location;
            //this.fromdate = fromdate;
            //this.todate = todate;
            //this.p = p;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 10);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(342, 287);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(372, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Copy To Clipboard";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(373, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "Help";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(373, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "Send Error Report";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BugReportDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(470, 309);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BugReportDialog";
            this.Text = "BugReport";
            this.Shown += new System.EventHandler(this.BugReportDialog_Shown);
            this.Load += new System.EventHandler(this.BugReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Press the Copy To Clipboard button to copy the bug report to the clipboard.\n\rPaste this into the email client you are using and send it to usman.ghani@gmail.com", "Prayer Time Calculator © 2005 Usman Ghani");
            
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            button1.Enabled = false;
            try
            {
                
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    Clipboard.Clear();

                }
                Clipboard.SetText(this.textBox1.Text, TextDataFormat.Text);
            

            }
            catch
            {

                
                                
            }
            button1.Enabled = true;

		}

        public void AddBugReport(LocDataElement location, DateTime fromdate, DateTime todate, Prayers[] p)
        {
            BugReport r = new BugReport(location, fromdate, todate, p);
            reports.Add(r);

        }
        
        private string buildErrorReport(BugReport r)
        {
            string result = string.Empty;
            result += "City : " + r.Location.City + Environment.NewLine;
            result += "Country : " + r.Location.Country + Environment.NewLine;
            result += "Latitude : " + r.Location.Latitude + Environment.NewLine;
            result += "Longitude : " + r.Location.Longitude + Environment.NewLine;
            result += "GMTDiff : " + r.Location.GMTDiff + Environment.NewLine;
            result += "Daylight Adjustment : " + r.Location.DaylightAdjustment + Environment.NewLine;
            result += "DateRange : " + r.FromDate.ToLongDateString() + " -- " + r.ToDate.ToLongDateString() + Environment.NewLine;
            result += "Prayers : " + Environment.NewLine;
            foreach (Prayers prayer in r.Prayers)
            {
                result += prayer.ToString() + Environment.NewLine;

            }

            return result;

        }

		private void BugReport_Load(object sender, System.EventArgs e)
		{


		}

        private void button3_Click(object sender, EventArgs e)
        {
            

        }
        private void SendMail(string from, string to, string subject, string body)
        {


        }

        private void BugReportDialog_Shown(object sender, EventArgs e)
        {
            this.textBox1.Clear();

            foreach (BugReport r in reports)
            {
                string repstr = buildErrorReport(r);
                this.textBox1.Text += repstr + Environment.NewLine;

            }

        }

	}
}
