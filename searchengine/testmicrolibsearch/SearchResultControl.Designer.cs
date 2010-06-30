namespace testmicrolibsearch
{
    partial class SearchResultControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResultControl));
            this.resultsDisplayPanel = new System.Windows.Forms.Panel();
            this.searchResultsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.resultsDisplay = new System.Windows.Forms.WebBrowser();
            this.searchResultsToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnViewOriginalDoc = new System.Windows.Forms.ToolStripButton();
            this.lblNumHits = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.resultsDisplayPanel.SuspendLayout();
            this.searchResultsSplitContainer.Panel1.SuspendLayout();
            this.searchResultsSplitContainer.Panel2.SuspendLayout();
            this.searchResultsSplitContainer.SuspendLayout();
            this.searchResultsToolStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultsDisplayPanel
            // 
            this.resultsDisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultsDisplayPanel.Controls.Add(this.searchResultsSplitContainer);
            this.resultsDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsDisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.resultsDisplayPanel.Name = "resultsDisplayPanel";
            this.resultsDisplayPanel.Size = new System.Drawing.Size(619, 585);
            this.resultsDisplayPanel.TabIndex = 1;
            // 
            // searchResultsSplitContainer
            // 
            this.searchResultsSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchResultsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.searchResultsSplitContainer.Name = "searchResultsSplitContainer";
            // 
            // searchResultsSplitContainer.Panel1
            // 
            this.searchResultsSplitContainer.Panel1.Controls.Add(this.lstResults);
            // 
            // searchResultsSplitContainer.Panel2
            // 
            this.searchResultsSplitContainer.Panel2.Controls.Add(this.resultsDisplay);
            this.searchResultsSplitContainer.Size = new System.Drawing.Size(617, 583);
            this.searchResultsSplitContainer.SplitterDistance = 194;
            this.searchResultsSplitContainer.TabIndex = 0;
            // 
            // lstResults
            // 
            this.lstResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResults.FormattingEnabled = true;
            this.lstResults.IntegralHeight = false;
            this.lstResults.Location = new System.Drawing.Point(0, 0);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(190, 579);
            this.lstResults.TabIndex = 0;
            // 
            // resultsDisplay
            // 
            this.resultsDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsDisplay.IsWebBrowserContextMenuEnabled = false;
            this.resultsDisplay.Location = new System.Drawing.Point(0, 0);
            this.resultsDisplay.MinimumSize = new System.Drawing.Size(20, 20);
            this.resultsDisplay.Name = "resultsDisplay";
            this.resultsDisplay.Size = new System.Drawing.Size(415, 579);
            this.resultsDisplay.TabIndex = 1;
            // 
            // searchResultsToolStrip
            // 
            this.searchResultsToolStrip.AutoSize = false;
            this.searchResultsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.searchResultsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnViewOriginalDoc,
            this.lblNumHits});
            this.searchResultsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.searchResultsToolStrip.Name = "searchResultsToolStrip";
            this.searchResultsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.searchResultsToolStrip.Size = new System.Drawing.Size(619, 25);
            this.searchResultsToolStrip.Stretch = true;
            this.searchResultsToolStrip.TabIndex = 3;
            this.searchResultsToolStrip.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btnViewOriginalDoc
            // 
            this.btnViewOriginalDoc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnViewOriginalDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewOriginalDoc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.btnViewOriginalDoc.ForeColor = System.Drawing.Color.Blue;
            this.btnViewOriginalDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnViewOriginalDoc.Image")));
            this.btnViewOriginalDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewOriginalDoc.Name = "btnViewOriginalDoc";
            this.btnViewOriginalDoc.Size = new System.Drawing.Size(135, 22);
            this.btnViewOriginalDoc.Text = "View Original Document...";
            // 
            // lblNumHits
            // 
            this.lblNumHits.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblNumHits.Name = "lblNumHits";
            this.lblNumHits.Size = new System.Drawing.Size(55, 22);
            this.lblNumHits.Text = "{0} hit(s).";
            this.lblNumHits.Visible = false;
            // 
            // toolStripContainer
            // 
            this.toolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.AutoScroll = true;
            this.toolStripContainer.ContentPanel.Controls.Add(this.resultsDisplayPanel);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(619, 585);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(619, 610);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.searchResultsToolStrip);
            this.toolStripContainer.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // SearchResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer);
            this.Name = "SearchResultControl";
            this.Size = new System.Drawing.Size(619, 610);
            this.resultsDisplayPanel.ResumeLayout(false);
            this.searchResultsSplitContainer.Panel1.ResumeLayout(false);
            this.searchResultsSplitContainer.Panel2.ResumeLayout(false);
            this.searchResultsSplitContainer.ResumeLayout(false);
            this.searchResultsToolStrip.ResumeLayout(false);
            this.searchResultsToolStrip.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel resultsDisplayPanel;
        private System.Windows.Forms.SplitContainer searchResultsSplitContainer;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.WebBrowser resultsDisplay;
        private System.Windows.Forms.ToolStrip searchResultsToolStrip;
        private System.Windows.Forms.ToolStripButton btnViewOriginalDoc;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStripLabel lblNumHits;
    }
}
