using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;

namespace dbdumper
{
	public class Form1 : System.Windows.Forms.Form
	{

		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
        private Label label1;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.AlternatingBackColor = System.Drawing.Color.White;
            this.dataGrid1.BackColor = System.Drawing.Color.White;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.Silver;
            this.dataGrid1.CaptionFont = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.CaptionForeColor = System.Drawing.Color.Black;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.FlatMode = true;
            this.dataGrid1.Font = new System.Drawing.Font("Courier New", 9F);
            this.dataGrid1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.dataGrid1.GridLineColor = System.Drawing.Color.DarkGray;
            this.dataGrid1.HeaderBackColor = System.Drawing.Color.DarkGreen;
            this.dataGrid1.HeaderFont = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.HeaderForeColor = System.Drawing.Color.White;
            this.dataGrid1.LinkColor = System.Drawing.Color.DarkGreen;
            this.dataGrid1.Location = new System.Drawing.Point(57, 111);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.DarkSeaGreen;
            this.dataGrid1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGrid1.Size = new System.Drawing.Size(341, 270);
            this.dataGrid1.TabIndex = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "LocData";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("en-US");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(505, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(449, 165);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(629, 436);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.dataSet1.ReadXmlSchema ( "locdataschema.xsd" );
			XmlDataDocument datadoc = new XmlDataDocument ( dataSet1 );
			datadoc.Load ( "locdata.xml" );
			this.dataGrid1.DataSource = this.dataSet1.Tables["loctest"];
			
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			XmlDataDocument doc = new XmlDataDocument ();			
			doc.Load ( "locdata.xml" );
			XmlNodeList nodes = doc.GetElementsByTagName("city");
			foreach ( XmlNode n in nodes )
			{
				listBox1.Items.Add ( n.InnerText );

			}
            label1.Text = string.Format("Total {0} Item(s).", listBox1.Items.Count);

//			StreamReader reader = new StreamReader ( "loctest2.csv" );
//			string contents = reader.ReadToEnd ();
//			reader.Close ();
//			foreach ( string s in contents.Split ( "\n\r".ToCharArray() ) )
//			{
//				if ( s.Trim() == string.Empty ) continue;
//				foreach ( string p in s.Split ( ",".ToCharArray() ) )
//				{
//					string temp = p.Replace ( "\"", "" );
//					listBox1.Items.Add ( temp.Trim() );
//				}
//
//
//			}

		}
	}
}
