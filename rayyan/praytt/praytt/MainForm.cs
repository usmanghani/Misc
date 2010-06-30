using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Xml;
using System.Reflection;
using System.Drawing.Printing;
using System.Globalization;
using System.Diagnostics;
using System.Deployment.Application;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using libpraytt;

namespace praytt
{
	
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		//private ArrayList timeoptions = new ArrayList ();
        private UpdateProgressDialog upd = new UpdateProgressDialog();
		private List<LocDataElement> locdata = new List<LocDataElement>();
        private ApplicationDeployment deploy;
        private BindingSource bindingSource1;
        private IContainer components;
        private StatusStrip StatusBar;
        private ToolStripProgressBar prgIndicator;
        private ToolStripStatusLabel lblStatusMsg;
        private MenuStrip MenuBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnUpdate;
        private GroupBox groupBox3;
        private CheckedListBox checkedListBox1;
        private Button btnGo;
        private GroupBox groupBox1;
        private Label label5;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private CheckBox checkBox1;
        private Label label3;
        private DateTimePicker dateTimePicker2;
        private ComboBox comboBox1;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private ToolStripMenuItem newLocationToolStripMenuItem;
        BindingList<LocDataElement> locdatalist = new BindingList<LocDataElement>();
        private Label lblToday;
        private Label lblLocation;
        private CheckedListBox checkedListBox2;
        private Label lblAlarmHelp;
        private GroupBox groupBox2;
        private ComboBox comboBox3;
        private Label label7;
        private Label label6;
        private ComboBox comboBox2;
        private ListView listView1;
        private ColumnHeader colCity;
        private ColumnHeader colCountry;
        private ColumnHeader colLat;
        private ColumnHeader colLong;
        private ColumnHeader colGMT;
        private NotifyIcon icoAlarm;
        private System.Windows.Forms.Timer tmrAlarm;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem showMainWindowToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private LocationItemComparer comparer = new LocationItemComparer();

        private bool _isLocDataLoaded = false;


		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//


		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.prgIndicator = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colCity = new System.Windows.Forms.ColumnHeader();
            this.colCountry = new System.Windows.Forms.ColumnHeader();
            this.colLat = new System.Windows.Forms.ColumnHeader();
            this.colLong = new System.Windows.Forms.ColumnHeader();
            this.colGMT = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAlarmHelp = new System.Windows.Forms.Label();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.icoAlarm = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrAlarm = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgIndicator,
            this.lblStatusMsg});
            this.StatusBar.Location = new System.Drawing.Point(0, 471);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(918, 22);
            this.StatusBar.TabIndex = 1;
            // 
            // prgIndicator
            // 
            this.prgIndicator.Name = "prgIndicator";
            this.prgIndicator.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(0, 17);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MenuBar.Size = new System.Drawing.Size(918, 24);
            this.MenuBar.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLocationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newLocationToolStripMenuItem
            // 
            this.newLocationToolStripMenuItem.Name = "newLocationToolStripMenuItem";
            this.newLocationToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newLocationToolStripMenuItem.Text = "&New Location...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblAlarmHelp);
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBox2);
            this.splitContainer1.Panel2.Controls.Add(this.lblLocation);
            this.splitContainer1.Panel2.Controls.Add(this.lblToday);
            this.splitContainer1.Size = new System.Drawing.Size(918, 447);
            this.splitContainer1.SplitterDistance = 633;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 447);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.btnGo);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(625, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(459, 388);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 22);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Location = new System.Drawing.Point(442, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 219);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Prayer";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.IntegralHeight = false;
            this.checkedListBox1.Items.AddRange(new object[] {
            "All",
            "Fajr",
            "Tulu",
            "Zawal",
            "Zuhr",
            "Asr Shafai",
            "Asr Hanafi",
            "Maghrib",
            "Isha"});
            this.checkedListBox1.Location = new System.Drawing.Point(6, 16);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(159, 196);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(540, 388);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 22);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Go";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBox3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new System.Drawing.Point(8, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 219);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location";
            // 
            // comboBox3
            // 
            this.comboBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(88, 53);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(331, 21);
            this.comboBox3.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Country :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "City : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(88, 20);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(331, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCity,
            this.colCountry,
            this.colLat,
            this.colLong,
            this.colGMT});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(8, 94);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(411, 118);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // colCity
            // 
            this.colCity.Text = "City";
            // 
            // colCountry
            // 
            this.colCountry.Text = "Country";
            // 
            // colLat
            // 
            this.colLat.Text = "Latitude";
            // 
            // colLong
            // 
            this.colLong.Text = "Longitude";
            // 
            // colGMT
            // 
            this.colGMT.Text = "GMTDiff";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 128);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Days";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "After every";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDown1.Location = new System.Drawing.Point(88, 56);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox1.Location = new System.Drawing.Point(16, 88);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 24);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Summarize";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "To:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateTimePicker2.Location = new System.Drawing.Point(383, 80);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.Items.AddRange(new object[] {
            "Year",
            "Month",
            "DateRange",
            "Date"});
            this.comboBox1.Location = new System.Drawing.Point(88, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateTimePicker1.Location = new System.Drawing.Point(383, 24);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "From:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timings for";
            // 
            // lblAlarmHelp
            // 
            this.lblAlarmHelp.Location = new System.Drawing.Point(22, 75);
            this.lblAlarmHelp.Name = "lblAlarmHelp";
            this.lblAlarmHelp.Size = new System.Drawing.Size(233, 32);
            this.lblAlarmHelp.TabIndex = 1;
            this.lblAlarmHelp.Text = "Check the prayer times that you want to be notified about.";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "Fajr at :",
            "Tulu at :",
            "Zawal at :",
            "Zuhr at :",
            "Asr Shafai at :",
            "Asr Hanafi at :",
            "Maghrib at :",
            "Isha at :"});
            this.checkedListBox2.Location = new System.Drawing.Point(22, 117);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(233, 274);
            this.checkedListBox2.TabIndex = 2;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(22, 38);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(91, 13);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location : {0}, {1}";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(19, 10);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(60, 13);
            this.lblToday.TabIndex = 0;
            this.lblToday.Text = "Today : {0}";
            // 
            // icoAlarm
            // 
            this.icoAlarm.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.icoAlarm.BalloonTipText = "It\'s time for : { 0 }";
            this.icoAlarm.BalloonTipTitle = "Prayer Times Calculator Prayer Alarm";
            this.icoAlarm.ContextMenuStrip = this.contextMenuStrip1;
            this.icoAlarm.Icon = ((System.Drawing.Icon)(resources.GetObject("icoAlarm.Icon")));
            this.icoAlarm.Text = "Prayer Times Calculator Prayer Alarm";
            this.icoAlarm.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 70);
            // 
            // showMainWindowToolStripMenuItem
            // 
            this.showMainWindowToolStripMenuItem.Name = "showMainWindowToolStripMenuItem";
            this.showMainWindowToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.showMainWindowToolStripMenuItem.Text = "Show Main Window";
            this.showMainWindowToolStripMenuItem.Click += new System.EventHandler(this.showMainWindowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(918, 493);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MenuBar);
            this.MainMenuStrip = this.MenuBar;
            this.Name = "MainForm";
            this.Text = "Prayer Times Calculator © Usman Ghani 2005";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion 


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try 
			{
                Application.EnableVisualStyles();
				Application.Run(new MainForm());
			}
			catch
			{

			}

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            
			dispatch ( );

		}
		private void dispatch ( )
		{
			ArrayList prayers = new ArrayList();
			bool allPrayers = false;
            DateTime fromdate = this.dateTimePicker1.Value;
			DateTime todate = this.dateTimePicker2.Value;
			string city = string.Empty;
			string country = string.Empty;
			LocDataElement location = LocDataElement.Zero;
			if ( this.listView1.SelectedIndices.Count == 0 )
			{

                MessageBox.Show("Please select a location first !!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

			}
            if (this.listView1.SelectedIndices.Count > 10)
            {
                DialogResult response = MessageBox.Show("You have selected too many locations. It may take a long time. Are you sure you want to continue?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.No)
                {
                    return;
                }

            }

            if (this.checkedListBox1.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Please select the prayer times to calculate !!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            prayers = getSelectedPrayers();
            allPrayers = this.checkedListBox1.CheckedItems.Count >= this.checkedListBox1.Items.Count - 1;
			Prayers [] p = (Prayers[])prayers.ToArray ( typeof(Prayers) );
			
			switch ( this.comboBox1.SelectedIndex )
			{
				case 0:
					fromdate = new DateTime (this.dateTimePicker1.Value.Year,1,1);
					todate = fromdate.AddYears(1);
					todate = todate.Subtract ( new TimeSpan(1,0,0,0) );
					break;
				case 1:
					fromdate = new DateTime ( this.dateTimePicker1.Value.Year, this.dateTimePicker1.Value.Month, 1);
					todate = fromdate.AddMonths ( 1 );
					todate = todate.Subtract ( new TimeSpan ( 1, 0 , 0, 0 ) );
					break;
				case 3:
					todate = fromdate;
					break;

			}
            
			TimeCalculatorAdapter adapter = new TimeCalculatorAdapter ();
            BugReportDialog reporter = new BugReportDialog();
            WaitDialog dlg = new WaitDialog();
            dlg.DisplayMessage = "Please Wait...";
            dlg.ProgressType = 0;
            dlg.Owner = this;
            dlg.Left = this.Left + (this.Width - dlg.Width) / 2;
            dlg.Top = this.Top + (this.Height - dlg.Height) / 2;
            dlg.Show();
            Application.DoEvents();
            ProgressCallback pcb = delegate ( int progress )
            {
                if ( progress - dlg.Progress > 10 )
                    dlg.Progress = progress;
            };

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                location = locdata[(int)item.Tag];
                ArrayList data;
                try
                {
                    data = adapter.GetTime(p, allPrayers, this.checkBox1.Checked, (int)this.numericUpDown1.Value, fromdate, todate, location, pcb);
                    addPage(location.City + "-" + location.Country, data);
                    dlg.Progress = 0;
                }
                catch ( Exception ex )
                {
                    MessageBox.Show(ex.Message);
                    reporter.AddBugReport(location, fromdate, todate, p);

                }
                Application.DoEvents();
                
            }
            dlg.Close();
            if (reporter.ReportCount > 0)
            {
                MessageBox.Show("A critical error has prevented the application from performing some of the required operations.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                reporter.ShowDialog();
                reporter.Dispose();

            }            

		}
        private ArrayList getSelectedPrayers()
        {

            ArrayList p = new ArrayList();
            foreach (int i in this.checkedListBox1.CheckedIndices)
            {

                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        p.Add(Prayers.Fajr);
                        break;
                    case 2:
                        p.Add(Prayers.Sunrise);
                        break;
                    case 3:
                        p.Add(Prayers.Midday);
                        break;
                    case 4:
                        p.Add(Prayers.Zuhr);
                        break;
                    case 5:
                        p.Add(Prayers.AsrS);
                        break;
                    case 6:
                        p.Add(Prayers.AsrH);
                        break;
                    case 7:
                        p.Add(Prayers.Sunset);
                        break;
                    case 8:
                        p.Add(Prayers.Isha);
                        break;
                    default:
                        break;

                }

            }
            return p;

        }

		private void addPage ( string title , ArrayList data )
		{
			TabPage pg = new TabPage( title );
			TimePageControl  tpc = new TimePageControl ();
			tpc.SetData(data);
			tpc.SetTitle(title);
			tpc.Dock = DockStyle.Fill;
			pg.Controls.Add(tpc);
			tabControl1.TabPages.Add(pg);
			tabControl1.SelectedTab = pg;

		}

		private void LoadLocationData ( string filename )
		{
			XmlDataDocument doc = new XmlDataDocument ( );
			doc.Load ( filename );
			XmlNodeList nodes = doc.GetElementsByTagName ( "loctest" );
			foreach ( XmlNode node in nodes )
			{

				string city = toProperCase(node.ChildNodes[0].InnerText);
				string country = toProperCase(node.ChildNodes[1].InnerText);
				string gmt = node.ChildNodes[3].InnerText;
				string la_dir = node.ChildNodes[4].InnerText;
				string la = node.ChildNodes[5].InnerText;
				string lo_dir = node.ChildNodes[6].InnerText;
				string lo = node.ChildNodes[7].InnerText;
				double latitude = double.Parse ( la );
				double longitude = double.Parse ( lo );
				bool sign = gmt[0] == '+' ? false : true;
				latitude = la_dir == "NORTH" ? latitude : -latitude;
				longitude = lo_dir == "EAST" ? longitude : -longitude;
				double GMTDiff = TimeSpan.Parse ( gmt.Remove (0,1) ).TotalHours;
				GMTDiff = sign ? -GMTDiff : GMTDiff;
				LocDataElement elt = new LocDataElement();
				elt.City = city;
				elt.Country = country;
				elt.DaylightAdjustment = 0;
				elt.GMTDiff = GMTDiff;
				elt.Latitude = latitude;
				elt.Longitude = longitude;
                locdata.Add(elt);
                locdatalist.Add(elt);

			}
			
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            comparer.Order = SortOrder.Ascending;
            this.listView1.ListViewItemSorter = comparer;

            
		}
        private void BindLVW2Collection ( ListView lvw, ICollection coll )
        {

        }
        private string toProperCase(string str)
        {
            string result = string.Empty;
            string[] words = str.ToLower().Split(" ".ToCharArray(), str.Length, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                result += word[0].ToString().ToUpper ( ) + word.Remove ( 0, 1 );

            }
            return result;

        }

		private void checkedListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if ( this.checkedListBox1.SelectedIndex == 0 )
                for (int i = 1; i < this.checkedListBox1.Items.Count; i++)
                    this.checkedListBox1.SetItemChecked(i, true);
			if ( this.checkedListBox1.CheckedIndices.Contains ( 0 ) && this.checkedListBox1.CheckedIndices.Count == 1 )
				for ( int i = 1 ; i < this.checkedListBox1.Items.Count ; i ++ )
					this.checkedListBox1.SetItemChecked ( i , true );
			else if ( this.checkedListBox1.CheckedIndices.Contains ( 0 ) && this.checkedListBox1.CheckedIndices.Count != this.checkedListBox1.Items.Count )
				this.checkedListBox1.SetItemChecked ( 0 , false );
			else if ( ! this.checkedListBox1.CheckedIndices.Contains(0) && this.checkedListBox1.CheckedIndices.Count == this.checkedListBox1.Items.Count - 1 )
				this.checkedListBox1.SetItemChecked ( 0 , true );


		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                
                if (!tabControl1.GetTabRect(0).Contains(e.X, e.Y))
                {
                    for  ( int i = 1 ; i < tabControl1.TabPages.Count ; i ++ )
                    {
                        if (tabControl1.GetTabRect(i).Contains(e.X, e.Y))
                            tabControl1.TabPages.RemoveAt(i);

                    }

                }

            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                deploy = ApplicationDeployment.CurrentDeployment;
                deploy.CheckForUpdateCompleted += new CheckForUpdateCompletedEventHandler(deploy_CheckForUpdateCompleted);
                deploy.CheckForUpdateProgressChanged += new DeploymentProgressChangedEventHandler(deploy_CheckForUpdateProgressChanged);
                upd.ShowDialog();
                deploy.CheckForUpdateAsync();
            }

        }

        void deploy_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {

            upd.UpdateProgress(e.ProgressPercentage);

        }

        void deploy_CheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            upd.Hide();
            MessageBox.Show("Update Completed", Text);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_isLocDataLoaded) return;

            WaitDialog dlg = new WaitDialog();
            dlg.Owner = this;
            dlg.DisplayMessage = "Loading location data...";
            dlg.ProgressType = 1;
            dlg.Left = this.Left + (this.Width - dlg.Width) / 2;
            dlg.Top = this.Top + (this.Height - dlg.Height) / 2;
            dlg.Show();

            Application.DoEvents();
            try
            {
                locdatalist.AllowEdit = false;
                locdatalist.AllowNew = false;
                locdatalist.AllowRemove = false;
                locdatalist.RaiseListChangedEvents = true;


                string datafilepath = string.Empty;
                if (ApplicationDeployment.IsNetworkDeployed)
                    datafilepath = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\locdata.xml";
                else
                    datafilepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\locdata.xml";
                LoadLocationData(datafilepath);
                _isLocDataLoaded = true;
                Application.DoEvents();
                comboBox1.SelectedIndex = 0;

                listView1.ListViewItemSorter = new LocationItemComparer(0, SortOrder.Ascending);

                float progressstep = 100 / (float)locdata.Count;
                float progress = 0;
                for (int i = 0; i < locdata.Count; i++)
                {

                    LocDataElement elt = locdata[i];
                    ListViewItem item = new ListViewItem();
                    item.Tag = i;
                    item.Text = elt.City;
                    item.SubItems.Add(elt.Country);
                    string temp = string.Empty;
                    if (elt.latitude >= 0)
                        temp = elt.Latitude.ToString() + "N";
                    else
                        temp = (-elt.Latitude).ToString() + "S";
                    item.SubItems.Add(temp);
                    if (elt.Longitude >= 0)
                        temp = elt.Longitude.ToString() + "E";
                    else
                        temp = (-elt.Longitude).ToString() + "W";
                    item.SubItems.Add(temp);

                    TimeSpan tempgmt = TimeSpan.FromHours(elt.GMTDiff);
                    item.SubItems.Add(tempgmt.ToString());
                    
                    listView1.Items.Add(item);

                    progress += progressstep ;
                    dlg.Progress = (int)progress;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(-1);
            }
            dlg.Close();
			
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (comparer.Column == e.Column)
            {
                if (comparer.Order == SortOrder.Ascending)
                    comparer.Order = SortOrder.Descending;
                else if (comparer.Order == SortOrder.Descending)
                    comparer.Order = SortOrder.Ascending;

            }
            else
            {
                comparer.Column = e.Column;
                comparer.Order = SortOrder.Ascending;

            }
            if ( comparer.Order == SortOrder.Ascending )             
            {
                
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Header.SortArrow.SortedUp);
                
            }

            
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.ApplicationExitCall:
                case CloseReason.WindowsShutDown:
                case CloseReason.TaskManagerClosing:
                    e.Cancel = false;
                    break;
                case CloseReason.None:
                case CloseReason.MdiFormClosing:
                case CloseReason.FormOwnerClosing:
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    icoAlarm.ShowBalloonTip(2000, "Prayer Times Calculator", "Prayer Times Calculator is running.", ToolTipIcon.Info);
                    break;

            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            dispatch();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void showMainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();

        }

	}

	public struct LocDataElement
	{

        public string city, country;
		public double latitude, longitude, daylightadjustment, gmtdiff;

        public static readonly LocDataElement Zero = getZero();


        private static LocDataElement getZero()
        {
            LocDataElement zero;
            zero.city = zero.country = string.Empty;
            zero.daylightadjustment = zero.gmtdiff = zero.longitude = zero.latitude = 0;
            return zero;

        }
        [Bindable(true)]
		public string City
		{
			get
			{
				return city;
			}
			set
			{
				city = value;
			}

		}
        [Bindable(true)]
		public string Country 
		{
			get
			{
				return country;
			}
			set
			{
				country = value;
			}
		}
        [Bindable(true)]
		public double Latitude
		{
			get
			{
				return latitude;

			}
			set
			{
				latitude = value;

			}


		}
        [Bindable(true)]
		public double Longitude
		{
			get 
			{
				return longitude;

			}
			set 
			{
				longitude = value;

			}


		}
        [Bindable(true)]
		public double DaylightAdjustment
		{
			get
			{
				return daylightadjustment;

			}
			set
			{
				daylightadjustment = value;

			}


		}
        [Bindable(true)]
		public double GMTDiff
		{
			get
			{
				return gmtdiff;

			}
			set
			{
				gmtdiff = value;

			}


		}

		
	}
    class LVWCollBinding
    {
        private ListView lvw;
        private ICollection coll;

        public LVWCollBinding(ListView lvw, ICollection coll)
        {
            this.lvw = lvw;
            this.coll = coll;

        }
        public void Refresh()
        {
            foreach (object o in coll)
            {

            }

        }


    }

	class MyComparer : IComparer
	{
		#region IComparer Members

		public int Compare(object x, object y)
		{			
			DateTimeFormatInfo dfi = new DateTimeFormatInfo ();
			dfi.ShortDatePattern = "dd/MM/yyyy";
			string[] lhs = (string[]) x;
			string[] rhs = (string[]) y;
			DateTime dt1 = DateTime.Parse ( lhs[0] , dfi );
			DateTime dt2 = DateTime.Parse ( rhs[0] , dfi );
			if ( dt1 < dt2 ) return -1;
			else if ( dt1 > dt2 ) return 1;
			else return 0;

		}

		#endregion
	}


	class TimeCalculatorAdapter
	{
		public ArrayList GetTime ( Prayers [] prayers, bool allPrayers, bool summarize, int granularity , DateTime fromdate, DateTime todate, LocDataElement location, ProgressCallback pcb ) 
		{
			TimeCalculator calc = new TimeCalculator (location.Longitude, location.Latitude, location.DaylightAdjustment, location.GMTDiff );
			Hashtable table = calc.GetPrayerTimingsBetween2 ( fromdate, todate, prayers, summarize, granularity, pcb );
			ArrayList result = new ArrayList ();

			foreach ( string s in table.Keys )
			{
				
				TimeListElement tle = (TimeListElement)table[s];
				FieldInfo [] fields = tle.GetType().GetFields();
				string [] vals = new string [ fields.Length ];
				for ( int i = 0 ; i < fields.Length; i ++  )
				{

					if ( fields[i].GetValue(tle).GetType() == typeof ( System.DateTime ) )
					{
						DateTimeFormatInfo dfi = new DateTimeFormatInfo();
						dfi.ShortDatePattern = "dd/MM/yyyy";
						vals [ i ] = ((DateTime)fields [ i ].GetValue (tle)).ToString("d",dfi);
						continue;
					}

					TimeSpan ts = (TimeSpan)fields[i].GetValue(tle);
					if ( ts != TimeSpan.Zero )
					{
						DateTime dt = new DateTime ( 1,1,1,ts.Hours, ts.Minutes, ts.Seconds );
						DateTimeFormatInfo dfi = new DateTimeFormatInfo ( );
						dfi.LongTimePattern = "hh:mm:ss";
						vals [ i ] = dt.ToString("T", dfi);

					}
					else vals [ i ] = string.Empty;

				}
				result.Add ( vals );

			}
			result.Sort(new MyComparer());
			return result;

		}

	}

	class LocationItemComparer : IComparer
	{
		private int col = 0;
		private SortOrder order = SortOrder.Ascending;

		public LocationItemComparer()
		{
			col = 0;
            order = SortOrder.None;

		}
		public LocationItemComparer(int column, SortOrder order)
		{
			col = column;
            this.order = order;

		}
		public int Compare(object x, object y)
		{

			int result = string.Compare((x as ListViewItem).Text,(y as ListViewItem).Text,true);
			if ( order == SortOrder.Ascending )
				return result;
			else if ( order == SortOrder.Descending )
                return -result;

            return 0;

		}
        public SortOrder Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;

            }


        }
        public int Column
        {
            get
            {
                return col;

            }
            set
            {
                col = value;

            }

        }

	}

}

			
