namespace testmicrolibsearch
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnPreferences = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnAddBook = new System.Windows.Forms.ToolStripButton();
            this.lblSearchFor = new System.Windows.Forms.ToolStripLabel();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdvancedSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.fileOpener = new System.Windows.Forms.OpenFileDialog();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tvBooks = new System.Windows.Forms.TreeView();
            this.lblBooksHeader = new System.Windows.Forms.Label();
            this.tabMainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageSearchResults = new System.Windows.Forms.TabPage();
            this.searchResultControl1 = new testmicrolibsearch.SearchResultControl();
            this.tabPageAddEditBook = new System.Windows.Forms.TabPage();
            this.addEditBookControl1 = new testmicrolibsearch.AddEditBookControl();
            this.lblNoResults = new System.Windows.Forms.Label();
            this.tabPageAdvancedSearch = new System.Windows.Forms.TabPage();
            this.advancedSearch1 = new testmicrolibsearch.AdvancedSearch();
            this.statusBar.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.tabMainTabControl.SuspendLayout();
            this.tabPageSearchResults.SuspendLayout();
            this.tabPageAddEditBook.SuspendLayout();
            this.tabPageAdvancedSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgress,
            this.statusText});
            this.statusBar.Location = new System.Drawing.Point(0, 638);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(793, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(100, 16);
            this.statusProgress.Visible = false;
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(56, 17);
            this.statusText.Text = "Loading...";
            this.statusText.Visible = false;
            // 
            // mainImageList
            // 
            this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
            this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mainImageList.Images.SetKeyName(0, "Book_angleHS.png");
            this.mainImageList.Images.SetKeyName(1, "Book_openHS.png");
            // 
            // btnPreferences
            // 
            this.btnPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreferences.Image = ((System.Drawing.Image)(resources.GetObject("btnPreferences.Image")));
            this.btnPreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.Size = new System.Drawing.Size(23, 22);
            this.btnPreferences.Text = "Preferences...";
            this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddBook,
            this.lblSearchFor,
            this.txtSearch,
            this.btnSearch,
            this.btnPreferences});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainToolStrip.Size = new System.Drawing.Size(793, 25);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // btnAddBook
            // 
            this.btnAddBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddBook.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBook.Image")));
            this.btnAddBook.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(23, 22);
            this.btnAddBook.Text = "Add New Book...";
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // lblSearchFor
            // 
            this.lblSearchFor.Name = "lblSearchFor";
            this.lblSearchFor.Size = new System.Drawing.Size(61, 22);
            this.lblSearchFor.Text = "Search for:";
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 25);
            this.txtSearch.ToolTipText = "Enter Search Query";
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSearch,
            this.mnuAdvancedSearch});
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(32, 22);
            this.btnSearch.Text = "Search";
            this.btnSearch.ButtonClick += new System.EventHandler(this.btnSearch_ButtonClick);
            // 
            // mnuSearch
            // 
            this.mnuSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuSearch.Name = "mnuSearch";
            this.mnuSearch.Size = new System.Drawing.Size(158, 22);
            this.mnuSearch.Text = "&Search";
            this.mnuSearch.Click += new System.EventHandler(this.mnuSearch_Click);
            // 
            // mnuAdvancedSearch
            // 
            this.mnuAdvancedSearch.Name = "mnuAdvancedSearch";
            this.mnuAdvancedSearch.Size = new System.Drawing.Size(158, 22);
            this.mnuAdvancedSearch.Text = "&Advanced Search";
            this.mnuAdvancedSearch.Click += new System.EventHandler(this.mnuAdvancedSearch_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select a folder";
            // 
            // fileOpener
            // 
            this.fileOpener.FileName = "openFileDialog1";
            this.fileOpener.Filter = "(*.*) All Files|*.*";
            this.fileOpener.Multiselect = true;
            this.fileOpener.SupportMultiDottedExtensions = true;
            this.fileOpener.Title = "Select files";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.tvBooks);
            this.mainSplitContainer.Panel1.Controls.Add(this.lblBooksHeader);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.tabMainTabControl);
            this.mainSplitContainer.Panel2.Controls.Add(this.lblNoResults);
            this.mainSplitContainer.Size = new System.Drawing.Size(793, 613);
            this.mainSplitContainer.SplitterDistance = 218;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // tvBooks
            // 
            this.tvBooks.CheckBoxes = true;
            this.tvBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBooks.ImageIndex = 0;
            this.tvBooks.ImageList = this.mainImageList;
            this.tvBooks.Location = new System.Drawing.Point(0, 23);
            this.tvBooks.Name = "tvBooks";
            this.tvBooks.SelectedImageIndex = 0;
            this.tvBooks.Size = new System.Drawing.Size(216, 588);
            this.tvBooks.TabIndex = 0;
            this.tvBooks.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvBooks_AfterCollapse);
            this.tvBooks.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvBooks_BeforeExpand);
            this.tvBooks.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvBooks_BeforeCollapse);
            this.tvBooks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBooks_AfterSelect);
            this.tvBooks.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvBooks_BeforeSelect);
            this.tvBooks.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvBooks_AfterExpand);
            // 
            // lblBooksHeader
            // 
            this.lblBooksHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBooksHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksHeader.Location = new System.Drawing.Point(0, 0);
            this.lblBooksHeader.Name = "lblBooksHeader";
            this.lblBooksHeader.Size = new System.Drawing.Size(216, 23);
            this.lblBooksHeader.TabIndex = 0;
            this.lblBooksHeader.Text = "Books";
            this.lblBooksHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabMainTabControl
            // 
            this.tabMainTabControl.Controls.Add(this.tabPageSearchResults);
            this.tabMainTabControl.Controls.Add(this.tabPageAddEditBook);
            this.tabMainTabControl.Controls.Add(this.tabPageAdvancedSearch);
            this.tabMainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainTabControl.Location = new System.Drawing.Point(0, 0);
            this.tabMainTabControl.Name = "tabMainTabControl";
            this.tabMainTabControl.SelectedIndex = 0;
            this.tabMainTabControl.Size = new System.Drawing.Size(569, 611);
            this.tabMainTabControl.TabIndex = 1;
            // 
            // tabPageSearchResults
            // 
            this.tabPageSearchResults.Controls.Add(this.searchResultControl1);
            this.tabPageSearchResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchResults.Name = "tabPageSearchResults";
            this.tabPageSearchResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchResults.Size = new System.Drawing.Size(561, 585);
            this.tabPageSearchResults.TabIndex = 0;
            this.tabPageSearchResults.Text = "Search Results";
            this.tabPageSearchResults.UseVisualStyleBackColor = true;
            // 
            // searchResultControl1
            // 
            this.searchResultControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultControl1.Location = new System.Drawing.Point(3, 3);
            this.searchResultControl1.Name = "searchResultControl1";
            this.searchResultControl1.Size = new System.Drawing.Size(555, 579);
            this.searchResultControl1.TabIndex = 0;
            // 
            // tabPageAddEditBook
            // 
            this.tabPageAddEditBook.Controls.Add(this.addEditBookControl1);
            this.tabPageAddEditBook.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddEditBook.Name = "tabPageAddEditBook";
            this.tabPageAddEditBook.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddEditBook.Size = new System.Drawing.Size(561, 585);
            this.tabPageAddEditBook.TabIndex = 1;
            this.tabPageAddEditBook.Text = "Add/Edit Book";
            this.tabPageAddEditBook.UseVisualStyleBackColor = true;
            // 
            // addEditBookControl1
            // 
            this.addEditBookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addEditBookControl1.Location = new System.Drawing.Point(3, 3);
            this.addEditBookControl1.Name = "addEditBookControl1";
            this.addEditBookControl1.Size = new System.Drawing.Size(555, 579);
            this.addEditBookControl1.TabIndex = 0;
            // 
            // lblNoResults
            // 
            this.lblNoResults.AutoSize = true;
            this.lblNoResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoResults.Location = new System.Drawing.Point(237, 294);
            this.lblNoResults.Name = "lblNoResults";
            this.lblNoResults.Size = new System.Drawing.Size(97, 20);
            this.lblNoResults.TabIndex = 0;
            this.lblNoResults.Text = "No Results";
            this.lblNoResults.Visible = false;
            // 
            // tabPageAdvancedSearch
            // 
            this.tabPageAdvancedSearch.Controls.Add(this.advancedSearch1);
            this.tabPageAdvancedSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvancedSearch.Name = "tabPageAdvancedSearch";
            this.tabPageAdvancedSearch.Size = new System.Drawing.Size(561, 585);
            this.tabPageAdvancedSearch.TabIndex = 2;
            this.tabPageAdvancedSearch.Text = "Advanced Search";
            this.tabPageAdvancedSearch.UseVisualStyleBackColor = true;
            // 
            // advancedSearch1
            // 
            this.advancedSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedSearch1.Location = new System.Drawing.Point(0, 0);
            this.advancedSearch1.Name = "advancedSearch1";
            this.advancedSearch1.Size = new System.Drawing.Size(561, 585);
            this.advancedSearch1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 660);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.statusBar);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.Panel2.PerformLayout();
            this.mainSplitContainer.ResumeLayout(false);
            this.tabMainTabControl.ResumeLayout(false);
            this.tabPageSearchResults.ResumeLayout(false);
            this.tabPageAddEditBook.ResumeLayout(false);
            this.tabPageAdvancedSearch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.TreeView tvBooks;
        private System.Windows.Forms.Label lblBooksHeader;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ToolStripButton btnPreferences;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton btnAddBook;
        private System.Windows.Forms.ToolStripLabel lblSearchFor;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.Label lblNoResults;
        private System.Windows.Forms.TabControl tabMainTabControl;
        private System.Windows.Forms.TabPage tabPageSearchResults;
        private SearchResultControl searchResultControl1;
        private System.Windows.Forms.ImageList mainImageList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.OpenFileDialog fileOpener;
        private System.Windows.Forms.TabPage tabPageAddEditBook;
        private AddEditBookControl addEditBookControl1;
        private System.Windows.Forms.ToolStripSplitButton btnSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuAdvancedSearch;
        private System.Windows.Forms.TabPage tabPageAdvancedSearch;
        private AdvancedSearch advancedSearch1;


    }
}