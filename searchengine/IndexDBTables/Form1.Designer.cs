namespace IndexDBTables
{
    partial class Form1
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
            this.btnIndex = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.databaseDataSet = new IndexDBTables.DatabaseDataSet();
            this.transBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transTableAdapter = new IndexDBTables.DatabaseDataSetTableAdapters.transTableAdapter();
            this.prgIndexing = new System.Windows.Forms.ProgressBar();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDBpath = new System.Windows.Forms.TextBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.btnDBpath = new System.Windows.Forms.Button();
            this.btnTargetPath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblHits = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.databaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIndex
            // 
            this.btnIndex.Location = new System.Drawing.Point(339, 81);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(56, 23);
            this.btnIndex.TabIndex = 0;
            this.btnIndex.Text = "Index";
            this.btnIndex.UseVisualStyleBackColor = true;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.HorizontalScrollbar = true;
            this.lstResults.Location = new System.Drawing.Point(29, 182);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(365, 173);
            this.lstResults.TabIndex = 1;
            this.lstResults.SelectedIndexChanged += new System.EventHandler(this.lstResults_SelectedIndexChanged);
            // 
            // databaseDataSet
            // 
            this.databaseDataSet.DataSetName = "DatabaseDataSet";
            this.databaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // transBindingSource
            // 
            this.transBindingSource.DataMember = "trans";
            this.transBindingSource.DataSource = this.databaseDataSet;
            // 
            // transTableAdapter
            // 
            this.transTableAdapter.ClearBeforeFill = true;
            // 
            // prgIndexing
            // 
            this.prgIndexing.Location = new System.Drawing.Point(110, 81);
            this.prgIndexing.Name = "prgIndexing";
            this.prgIndexing.Size = new System.Drawing.Size(223, 23);
            this.prgIndexing.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(110, 117);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(223, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 12;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(415, 18);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(375, 337);
            this.webBrowser1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "DB path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Index target";
            // 
            // txtDBpath
            // 
            this.txtDBpath.Location = new System.Drawing.Point(110, 20);
            this.txtDBpath.Name = "txtDBpath";
            this.txtDBpath.Size = new System.Drawing.Size(250, 20);
            this.txtDBpath.TabIndex = 16;
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Location = new System.Drawing.Point(110, 55);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(250, 20);
            this.txtTargetPath.TabIndex = 17;
            this.txtTargetPath.TextChanged += new System.EventHandler(this.txtTargetPath_TextChanged);
            // 
            // btnDBpath
            // 
            this.btnDBpath.Location = new System.Drawing.Point(366, 18);
            this.btnDBpath.Name = "btnDBpath";
            this.btnDBpath.Size = new System.Drawing.Size(29, 18);
            this.btnDBpath.TabIndex = 18;
            this.btnDBpath.Text = "...";
            this.btnDBpath.UseVisualStyleBackColor = true;
            this.btnDBpath.Click += new System.EventHandler(this.btnDBpath_Click);
            // 
            // btnTargetPath
            // 
            this.btnTargetPath.Location = new System.Drawing.Point(366, 56);
            this.btnTargetPath.Name = "btnTargetPath";
            this.btnTargetPath.Size = new System.Drawing.Size(28, 18);
            this.btnTargetPath.TabIndex = 19;
            this.btnTargetPath.Text = "...";
            this.btnTargetPath.UseVisualStyleBackColor = true;
            this.btnTargetPath.Click += new System.EventHandler(this.btnTargetPath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Progress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(339, 112);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(56, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblHits
            // 
            this.lblHits.AutoSize = true;
            this.lblHits.Location = new System.Drawing.Point(26, 156);
            this.lblHits.Name = "lblHits";
            this.lblHits.Size = new System.Drawing.Size(126, 13);
            this.lblHits.TabIndex = 23;
            this.lblHits.Text = "Hits are reported here...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 374);
            this.Controls.Add(this.lblHits);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnTargetPath);
            this.Controls.Add(this.btnDBpath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.txtDBpath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.prgIndexing);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnIndex);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.databaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.ListBox lstResults;
        private DatabaseDataSet databaseDataSet;
        private System.Windows.Forms.BindingSource transBindingSource;
        private IndexDBTables.DatabaseDataSetTableAdapters.transTableAdapter transTableAdapter;
        private System.Windows.Forms.ProgressBar prgIndexing;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDBpath;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Button btnDBpath;
        private System.Windows.Forms.Button btnTargetPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblHits;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

