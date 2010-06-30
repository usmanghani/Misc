using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

namespace praytt
{
	/// <summary>
	/// Summary description for TimePage.
	/// </summary>
	public class TimePageControl : System.Windows.Forms.UserControl
	{
		private string title = string.Empty;
        private ArrayList timedata = null;
        private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ListView listView1;
        private ColumnHeader colDate;
        private ColumnHeader colFajr;
        private ColumnHeader colTulu;
        private ColumnHeader colZawal;
        private ColumnHeader colZuhr;
        private ColumnHeader colAsrS;
        private ColumnHeader colAsrH;
        private ColumnHeader colMaghrib;
        private ColumnHeader colIsha;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private IContainer components;

		public TimePageControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimePageControl));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colFajr = new System.Windows.Forms.ColumnHeader();
            this.colTulu = new System.Windows.Forms.ColumnHeader();
            this.colZawal = new System.Windows.Forms.ColumnHeader();
            this.colZuhr = new System.Windows.Forms.ColumnHeader();
            this.colAsrS = new System.Windows.Forms.ColumnHeader();
            this.colAsrH = new System.Windows.Forms.ColumnHeader();
            this.colMaghrib = new System.Windows.Forms.ColumnHeader();
            this.colIsha = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.QueryPageSettings += new System.Drawing.Printing.QueryPageSettingsEventHandler(this.printDocument1_QueryPageSettings);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 470);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(590, 4);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.Document = this.printDocument1;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 474);
            this.panel1.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colFajr,
            this.colTulu,
            this.colZawal,
            this.colZuhr,
            this.colAsrS,
            this.colAsrH,
            this.colMaghrib,
            this.colIsha});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(590, 449);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick_1);
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            // 
            // colFajr
            // 
            this.colFajr.Text = "Fajr";
            // 
            // colTulu
            // 
            this.colTulu.Text = "Tulu";
            // 
            // colZawal
            // 
            this.colZawal.Text = "Zawal";
            // 
            // colZuhr
            // 
            this.colZuhr.Text = "Zuhr";
            // 
            // colAsrS
            // 
            this.colAsrS.Text = "AsrShafai";
            // 
            // colAsrH
            // 
            this.colAsrH.Text = "AsrHanafi";
            // 
            // colMaghrib
            // 
            this.colMaghrib.Text = "Maghrib";
            // 
            // colIsha
            // 
            this.colIsha.Text = "Isha";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(590, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(113, 22);
            this.toolStripLabel1.Text = "Prayer Timings for {0}";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "X";
            this.toolStripButton1.ToolTipText = "Close this tab";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Print";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // TimePageControl
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "TimePageControl";
            this.Size = new System.Drawing.Size(590, 474);
            this.Load += new System.EventHandler(this.TimePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// Handles the Load event of the TimePage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		private void TimePage_Load(object sender, System.EventArgs e)
		{
            listView1.Columns[0].TextAlign = HorizontalAlignment.Right;
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            for (int i = 0; i < timedata.Count; i++)
            {
                string[] data = (string[])timedata[i];
                for (int j = 0; j < data.Length; j++)
                {
                    if (data[j] == TimeSpan.Zero.ToString() || data[j] == string.Empty)
                    {

                        //this.listView1.Columns[j].Text = string.Empty;
                        this.listView1.Columns[j].Width = 0;

                    }
                }

                listView1.Items.Add(new ListViewItem(data));
                Application.DoEvents();
                
            }


			//			if ( timedata != null )
			//			{
			//				foreach ( string s in timedata.Keys )
			//				{
			//					TimeListElement elt = (TimeListElement)timedata [ s ];
			//					listView1.Items.Add ( new ListViewItem(new string []
			//					{
			//						DateTime.Parse(s).ToLongDateString (),
			//						elt.Fajr.ToString(),
			//						elt.Sunrise.ToString(),
			//						elt.Midday.ToString(),
			//						elt.Zuhr.ToString(),
			//						elt.AsrS.ToString(),
			//						elt.AsrH.ToString(),
			//						elt.Sunset.ToString(),
			//						elt.Isha.ToString()
			//						
			//					}));
			//
			//				}
			//			}
			//
			//listView1.ListViewItemSorter = new TimeItemComparer ( 0, true );

		}
        /// <summary>
        /// Gets the list view.
        /// </summary>
        /// <returns>ListView</returns>
		public ListView GetListView () 
		{
			return listView1;
		}
        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <returns>PrintDocument</returns>
		public PrintDocument GetDocument ()
		{
			return printDocument1;
		}
		public string GetTitle ()
		{
			return this.title;

		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			listView1.ListViewItemSorter = new TimeItemComparer ( e.Column, true );

		}
		public void SetData ( ArrayList data )
		{
			this.timedata = data;

		}
		public void SetTitle ( string title )
		{
			this.title = title;
			//this.label1.Text = title;
            this.toolStripLabel1.Text = "Prayer Timings for " + title;


		}
		string type;
		public void SetType ( string type )
		{
			this.type = type;
			
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			TabPage tp = (TabPage)this.Parent;
			TabControl tc = (TabControl)tp.Parent;
			tc.TabPages.Remove ( tp );
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
//			Printing printer = new Printing (this);
//			printer.Show();
			
			this.printPreviewDialog1.Document = this.printDocument1;
			this.printPreviewDialog1.ShowDialog();

		}
		private string createTruncatedString ( PrintPageEventArgs e, string s, int width ) 
		{
			string result = s;
			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			Font datafont = listView1.Font;			
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
			Font myfont = new Font ( datafont.FontFamily.Name, datafont.Size, FontStyle.Bold );
			headersize = g.MeasureString ( s, myfont, width, format );
			
			if ( headersize.Width > width )
			{
				int newwidth = (int)(width - g.MeasureString ( "...", datafont ).Width);
				string temp = "";
				SizeF newsize = g.MeasureString(temp,datafont);
				for ( int i = 0 ; i < s.Length && newsize.Width < newwidth ; i ++ )
				{
					if ( newsize.Width + g.MeasureString ( s[i].ToString(), datafont ).Width < newwidth )
					{

						temp += s[i];
						newsize = g.MeasureString( temp, datafont);
					}
					else break;

				}
				result = temp + "...";
				
			}
			return result;

		}
		private SizeF getTitleSize ( PrintPageEventArgs e )
		{

			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
			
			ListViewItem firstItem = listView1.Items[0];
			ListViewItem lastItem = listView1.Items [listView1.Items.Count - 1];
			DateTimeFormatInfo dfi = new DateTimeFormatInfo ();
			dfi.ShortDatePattern = "dd/MM/yyyy";
			DateTime StartDate = DateTime.Parse ( firstItem.Text, dfi);
			DateTime EndDate = DateTime.Parse ( lastItem.Text, dfi );

			string TitleString = string.Format ("TimeTable for {0}\n{1} - {2}", this.title, StartDate.ToLongDateString(), EndDate.ToLongDateString());
			Font TitleFont = new Font(listView1.Font.FontFamily.Name, listView1.Font.Size, FontStyle.Bold);
			SizeF titleSize = g.MeasureString(TitleString, TitleFont, bounds.Width, format  );
			titleSize.Height += 10;
            return titleSize;
			

		}

		private void printTitle ( PrintPageEventArgs e, ref PointF currentPosition, int startIndex, int count )
		{

			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			Font datafont = listView1.Font;			
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;

			int limit = Math.Min ( startIndex + count, listView1.Items.Count );
			ListViewItem firstItem = listView1.Items[currentIndex];
			ListViewItem lastItem = listView1.Items [limit - 1];
			DateTimeFormatInfo dfi = new DateTimeFormatInfo ();
			dfi.ShortDatePattern = "dd/MM/yyyy";
			DateTime StartDate = DateTime.Parse ( firstItem.Text, dfi);
			DateTime EndDate = DateTime.Parse ( lastItem.Text, dfi );

			string TitleString = string.Format ("TimeTable for {0}\n{1} - {2}", this.title, StartDate.ToLongDateString(), EndDate.ToLongDateString());
			Font TitleFont = new Font(datafont.FontFamily.Name, datafont.Size, FontStyle.Bold );
			SizeF titleSize = g.MeasureString(TitleString, TitleFont, bounds.Width, format  );
			RectangleF titleRect = new RectangleF(currentPosition, titleSize );
			g.DrawString ( TitleString, TitleFont , Brushes.Black, titleRect, format );

			currentPosition.X = bounds.Left;
			currentPosition.Y += titleRect.Height + 10;


		}
		private SizeF getHeaderSize ( PrintPageEventArgs e )
		{

			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
			SizeF result = new SizeF ();
			string coltext = listView1.Columns[0].Text;
			int width = listView1.Columns[0].Width;
			Font myfont = new Font ( listView1.Font.FontFamily.Name, listView1.Font.Size, FontStyle.Bold );
			float height = g.MeasureString ( coltext, myfont, width, format ).Height;
			width = 0;
			foreach ( ColumnHeader c in listView1.Columns )
			{
				width += c.Width;
			}
			result.Height = height + 10;
			result.Width = width;
			return result;
		}

		private void printHeader ( PrintPageEventArgs e, ref PointF currentPosition )
		{
			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			Font datafont = listView1.Font;			
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;

			foreach ( ColumnHeader col in listView1.Columns )
			{
				
				if ( col.Width > 0 )
				{
					string coltext = col.Text;	
					Font myfont = new Font ( datafont.FontFamily.Name, datafont.Size, FontStyle.Bold );
					headersize = g.MeasureString ( coltext, myfont, col.Width, format );
					RectangleF rect = new RectangleF (currentPosition, headersize);
					g.DrawString ( col.Text, myfont, Brushes.Black, rect, format );
					currentPosition.X += col.Width;
				}

                                				
			}
			currentPosition.X = bounds.Left;
			currentPosition.Y += headersize.Height + 10;

		}
		private SizeF getItemSize ( PrintPageEventArgs e )
		{
			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
			SizeF result = new SizeF ();
			string coltext = listView1.Items[0].Text;
			int width = listView1.Columns[0].Width;
			Font myfont = new Font ( listView1.Font.FontFamily.Name, listView1.Font.Size, FontStyle.Bold );
			float height = g.MeasureString ( coltext, myfont, width, format ).Height;
			width = 0;
			foreach ( ColumnHeader c in listView1.Columns )
			{
				width += c.Width;
			}
			result.Height = height + 10;
			result.Width = width;
			return result;

		}

		private void printItems ( PrintPageEventArgs e, ref PointF currentPosition, int startIndex, int count )
		{
			Graphics g = e.Graphics;
			Rectangle bounds = e.MarginBounds;
			Font titleFont = new Font ( FontFamily.GenericSerif, listView1.Font.SizeInPoints );
			Font datafont = listView1.Font;			
			SizeF headersize = SizeF.Empty;
			StringFormat format = new StringFormat ();
			format.Trimming = StringTrimming.EllipsisCharacter;
			format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit;
			int limit = Math.Min(startIndex + count, listView1.Items.Count);
			for ( int i = startIndex ; i < limit; i ++ )
			{
				ListViewItem item = listView1.Items[i];
				for ( int s = 0 ; s < item.SubItems.Count ; s ++ )
				{
					int width = listView1.Columns[s].Width;
					if ( width > 0 ) 
					{
						string coltext = item.SubItems[s].Text;
						datafont = item.SubItems[s].Font;
						headersize = g.MeasureString ( coltext, datafont, width, format );
						RectangleF rect = new RectangleF ( currentPosition, headersize );
						g.DrawString ( coltext, datafont, Brushes.Black, rect, format );
						currentPosition.X += width;
					}

                				
				}
		
				currentPosition.X = bounds.Left;
				currentPosition.Y += headersize.Height + 10;


			}


		}

		private int calculatePages ( PrintPageEventArgs e )
		{
			int lines = 0;
			SizeF size = getTitleSize(e);
			size.Height += getHeaderSize(e).Height;
			while ( size.Height < e.MarginBounds.Height )
			{
				lines ++ ;
				size.Height += getItemSize(e).Height;
			}
			return (int)Math.Ceiling((float)listView1.Items.Count / lines);

		}

		int currentIndex = 0;
		int currentPage = 1;
		int totalPages = 1;
		int itemsPerPage = 1;
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			if ( currentPage == 1 )
			{
				totalPages = calculatePages(e);
				itemsPerPage = (int)Math.Ceiling((float)listView1.Items.Count / totalPages);

			}
			e.HasMorePages = (currentPage != totalPages);
			Rectangle bounds = e.MarginBounds;
			PointF currpos = new PointF ( bounds.Left, bounds.Top );
			printTitle ( e, ref currpos, currentIndex, itemsPerPage );
			printHeader( e, ref currpos );
			printItems ( e, ref currpos, currentIndex, itemsPerPage );
			currentIndex += itemsPerPage;
			currentPage ++;

		}

		private void printDocument1_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
		{
			
		}

		private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		
		}

		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			currentIndex = 0;
			currentPage = 1;
                        
		}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TabPage tp = (TabPage)this.Parent;
            TabControl tc = (TabControl)tp.Parent;
            tc.TabPages.Remove(tp);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.ShowDialog();

        }

        private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            
        }


	}

	class TimeItemComparer : IComparer
	{
		private int col = 0;
		private bool Ascending = true;
		public TimeItemComparer()
		{
			col = 0;
		}
		public TimeItemComparer(int column, bool ascending)
		{
			col = column;
		}
		public int Compare(object x, object y)
		{
			DateTimeFormatInfo dfi = new DateTimeFormatInfo();
			dfi.ShortDatePattern = "dd/MM/yyyy";
			int result = DateTime.Parse((x as ListViewItem).Text,dfi).CompareTo(DateTime.Parse  ((y as ListViewItem).Text,dfi));
			if ( Ascending )
				return result;
			else return -result;

		}

	}
}
