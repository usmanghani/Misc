#region Using directives

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;

#endregion

namespace prayttmobile
{
    /// <summary>
    /// Summary description for form.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private MenuItem menuItem1;
        private ListView listView1;
        private MenuItem menuItem2;
        private ColumnHeader columnHeader1;
        /// <summary>
        /// Main menu for the form.
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu1;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Calculate";
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, -1);
            this.listView1.Size = new System.Drawing.Size(176, 181);
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Locations";
            this.columnHeader1.Width = 60;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.listView1);
            this.Menu = this.mainMenu1;
            this.Text = "PrayerTimeTable";
            this.Load += new System.EventHandler(this.Form1_Load);

        }

        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string basestr = Properties.Resources.locdata;
            MemoryStream stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(basestr));
            //StringReader reader = new StringReader(basestr);
            string city = string.Empty;
            string country = string.Empty;
            string green = string.Empty;
            string la_dir = string.Empty;
            string latitude = string.Empty;
            string lo_dir = string.Empty;
            string longitude = string.Empty;
            bool readloc = false;
            XmlTextReader reader = new XmlTextReader(stream);
            try
            {
                while (reader.Read())
                {

                    if ((reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement))
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "loctest")
                        {
                            readloc = true;
                        }
                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "loctest")
                        {
                            readloc = false;

                        }
                        if (readloc == false && reader.Name == "loctest")
                        {
                            listView1.Items.Add(new ListViewItem(city + " - " + country + " - " + latitude + " - " + longitude + " - " + green));
                            continue;
                        }

                        switch (reader.Name)
                        {
                            case "city":
                                city = reader.ReadInnerXml();
                                break;
                            case "country":
                                country = reader.ReadInnerXml();
                                break;
                            case "green":
                                green = reader.ReadInnerXml();
                                break;
                            case "latitude":
                                latitude = reader.ReadInnerXml();
                                break;
                            case "longitude":
                                longitude = reader.ReadInnerXml();
                                break;
                            case "la_dir":
                                la_dir = reader.ReadInnerXml();
                                latitude = latitude + la_dir[0];
                                break;
                            case "lo_dir":
                                lo_dir = reader.ReadInnerXml();
                                longitude = longitude + lo_dir[0];
                                break;

                        }

                    }

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                if (reader.ReadState != ReadState.Closed)
                    reader.Close();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader.ReadState != ReadState.Closed)
                    reader.Close();

            }


        }

    }

}

