namespace GreHelper
{
    partial class frmMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnShowLogs = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.btnShowResponse = new System.Windows.Forms.Button();
            this.btnNextWord = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurrentWord = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbOption4 = new System.Windows.Forms.PictureBox();
            this.pbOption3 = new System.Windows.Forms.PictureBox();
            this.pbOption2 = new System.Windows.Forms.PictureBox();
            this.pbOption1 = new System.Windows.Forms.PictureBox();
            this.optOption4 = new System.Windows.Forms.RadioButton();
            this.optOption3 = new System.Windows.Forms.RadioButton();
            this.optOption2 = new System.Windows.Forms.RadioButton();
            this.optOption1 = new System.Windows.Forms.RadioButton();
            this.lblCurrentWordLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDictionary = new System.Windows.Forms.TabPage();
            this.wbDictionary = new System.Windows.Forms.WebBrowser();
            this.tabThesaurus = new System.Windows.Forms.TabPage();
            this.wbThesaurus = new System.Windows.Forms.WebBrowser();
            this.tabNews = new System.Windows.Forms.TabPage();
            this.wbNews = new System.Windows.Forms.WebBrowser();
            this.tabWebSearch = new System.Windows.Forms.TabPage();
            this.wbWebSearch = new System.Windows.Forms.WebBrowser();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabDictionary.SuspendLayout();
            this.tabThesaurus.SuspendLayout();
            this.tabNews.SuspendLayout();
            this.tabWebSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnShowLogs);
            this.splitContainer1.Panel1.Controls.Add(this.pbStatus);
            this.splitContainer1.Panel1.Controls.Add(this.btnShowResponse);
            this.splitContainer1.Panel1.Controls.Add(this.btnNextWord);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lblCurrentWord);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lblCurrentWordLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(912, 756);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnShowLogs
            // 
            this.btnShowLogs.Location = new System.Drawing.Point(776, 180);
            this.btnShowLogs.Name = "btnShowLogs";
            this.btnShowLogs.Size = new System.Drawing.Size(124, 23);
            this.btnShowLogs.TabIndex = 8;
            this.btnShowLogs.Text = "Show Logs";
            this.btnShowLogs.UseVisualStyleBackColor = true;
            this.btnShowLogs.Click += new System.EventHandler(this.btnShowLogs_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.Image = global::GreHelper.Properties.Resources.correct;
            this.pbStatus.Location = new System.Drawing.Point(310, 12);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(44, 44);
            this.pbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStatus.TabIndex = 7;
            this.pbStatus.TabStop = false;
            this.pbStatus.Visible = false;
            // 
            // btnShowResponse
            // 
            this.btnShowResponse.Location = new System.Drawing.Point(776, 153);
            this.btnShowResponse.Name = "btnShowResponse";
            this.btnShowResponse.Size = new System.Drawing.Size(124, 23);
            this.btnShowResponse.TabIndex = 6;
            this.btnShowResponse.Text = "Show Answer";
            this.btnShowResponse.UseVisualStyleBackColor = true;
            this.btnShowResponse.Click += new System.EventHandler(this.btnShowResponse_Click);
            // 
            // btnNextWord
            // 
            this.btnNextWord.Location = new System.Drawing.Point(776, 126);
            this.btnNextWord.Name = "btnNextWord";
            this.btnNextWord.Size = new System.Drawing.Size(124, 23);
            this.btnNextWord.TabIndex = 5;
            this.btnNextWord.Text = "Next Word";
            this.btnNextWord.UseVisualStyleBackColor = true;
            this.btnNextWord.Click += new System.EventHandler(this.btnNextWord_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GreHelper.Properties.Resources.grelogo;
            this.pictureBox1.Location = new System.Drawing.Point(725, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblCurrentWord
            // 
            this.lblCurrentWord.AutoSize = true;
            this.lblCurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentWord.Location = new System.Drawing.Point(134, 22);
            this.lblCurrentWord.Name = "lblCurrentWord";
            this.lblCurrentWord.Size = new System.Drawing.Size(92, 20);
            this.lblCurrentWord.TabIndex = 3;
            this.lblCurrentWord.Text = "Study Word";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbOption4);
            this.groupBox1.Controls.Add(this.pbOption3);
            this.groupBox1.Controls.Add(this.pbOption2);
            this.groupBox1.Controls.Add(this.pbOption1);
            this.groupBox1.Controls.Add(this.optOption4);
            this.groupBox1.Controls.Add(this.optOption3);
            this.groupBox1.Controls.Add(this.optOption2);
            this.groupBox1.Controls.Add(this.optOption1);
            this.groupBox1.Location = new System.Drawing.Point(26, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose the correct/closest answer";
            // 
            // pbOption4
            // 
            this.pbOption4.Image = global::GreHelper.Properties.Resources.rightarrow;
            this.pbOption4.Location = new System.Drawing.Point(18, 99);
            this.pbOption4.Name = "pbOption4";
            this.pbOption4.Size = new System.Drawing.Size(49, 24);
            this.pbOption4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOption4.TabIndex = 8;
            this.pbOption4.TabStop = false;
            this.pbOption4.Visible = false;
            // 
            // pbOption3
            // 
            this.pbOption3.Image = global::GreHelper.Properties.Resources.rightarrow;
            this.pbOption3.Location = new System.Drawing.Point(18, 76);
            this.pbOption3.Name = "pbOption3";
            this.pbOption3.Size = new System.Drawing.Size(49, 24);
            this.pbOption3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOption3.TabIndex = 7;
            this.pbOption3.TabStop = false;
            this.pbOption3.Visible = false;
            // 
            // pbOption2
            // 
            this.pbOption2.Image = global::GreHelper.Properties.Resources.rightarrow;
            this.pbOption2.Location = new System.Drawing.Point(18, 53);
            this.pbOption2.Name = "pbOption2";
            this.pbOption2.Size = new System.Drawing.Size(49, 24);
            this.pbOption2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOption2.TabIndex = 6;
            this.pbOption2.TabStop = false;
            this.pbOption2.Visible = false;
            // 
            // pbOption1
            // 
            this.pbOption1.Image = global::GreHelper.Properties.Resources.rightarrow;
            this.pbOption1.Location = new System.Drawing.Point(18, 30);
            this.pbOption1.Name = "pbOption1";
            this.pbOption1.Size = new System.Drawing.Size(49, 24);
            this.pbOption1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOption1.TabIndex = 5;
            this.pbOption1.TabStop = false;
            this.pbOption1.Visible = false;
            // 
            // optOption4
            // 
            this.optOption4.AutoSize = true;
            this.optOption4.Location = new System.Drawing.Point(73, 99);
            this.optOption4.Name = "optOption4";
            this.optOption4.Size = new System.Drawing.Size(65, 17);
            this.optOption4.TabIndex = 4;
            this.optOption4.Text = "Option 4";
            this.optOption4.UseVisualStyleBackColor = true;
            this.optOption4.CheckedChanged += new System.EventHandler(this.optOption4_CheckedChanged);
            // 
            // optOption3
            // 
            this.optOption3.AutoSize = true;
            this.optOption3.Location = new System.Drawing.Point(73, 76);
            this.optOption3.Name = "optOption3";
            this.optOption3.Size = new System.Drawing.Size(65, 17);
            this.optOption3.TabIndex = 3;
            this.optOption3.Text = "Option 3";
            this.optOption3.UseVisualStyleBackColor = true;
            this.optOption3.CheckedChanged += new System.EventHandler(this.optOption3_CheckedChanged);
            // 
            // optOption2
            // 
            this.optOption2.AutoSize = true;
            this.optOption2.Location = new System.Drawing.Point(73, 53);
            this.optOption2.Name = "optOption2";
            this.optOption2.Size = new System.Drawing.Size(65, 17);
            this.optOption2.TabIndex = 2;
            this.optOption2.Text = "Option 2";
            this.optOption2.UseVisualStyleBackColor = true;
            this.optOption2.CheckedChanged += new System.EventHandler(this.optOption2_CheckedChanged);
            // 
            // optOption1
            // 
            this.optOption1.AutoSize = true;
            this.optOption1.Location = new System.Drawing.Point(73, 30);
            this.optOption1.Name = "optOption1";
            this.optOption1.Size = new System.Drawing.Size(65, 17);
            this.optOption1.TabIndex = 1;
            this.optOption1.Text = "Option 1";
            this.optOption1.UseVisualStyleBackColor = true;
            this.optOption1.CheckedChanged += new System.EventHandler(this.optOption1_CheckedChanged);
            // 
            // lblCurrentWordLabel
            // 
            this.lblCurrentWordLabel.AutoSize = true;
            this.lblCurrentWordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentWordLabel.Location = new System.Drawing.Point(23, 27);
            this.lblCurrentWordLabel.Name = "lblCurrentWordLabel";
            this.lblCurrentWordLabel.Size = new System.Drawing.Size(82, 13);
            this.lblCurrentWordLabel.TabIndex = 0;
            this.lblCurrentWordLabel.Text = "Current Word";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDictionary);
            this.tabControl1.Controls.Add(this.tabThesaurus);
            this.tabControl1.Controls.Add(this.tabNews);
            this.tabControl1.Controls.Add(this.tabWebSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(912, 515);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDictionary
            // 
            this.tabDictionary.Controls.Add(this.wbDictionary);
            this.tabDictionary.Location = new System.Drawing.Point(4, 22);
            this.tabDictionary.Name = "tabDictionary";
            this.tabDictionary.Padding = new System.Windows.Forms.Padding(3);
            this.tabDictionary.Size = new System.Drawing.Size(904, 489);
            this.tabDictionary.TabIndex = 0;
            this.tabDictionary.Text = "Dictionary";
            this.tabDictionary.UseVisualStyleBackColor = true;
            // 
            // wbDictionary
            // 
            this.wbDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbDictionary.Location = new System.Drawing.Point(3, 3);
            this.wbDictionary.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDictionary.Name = "wbDictionary";
            this.wbDictionary.ScriptErrorsSuppressed = true;
            this.wbDictionary.Size = new System.Drawing.Size(898, 483);
            this.wbDictionary.TabIndex = 0;
            // 
            // tabThesaurus
            // 
            this.tabThesaurus.Controls.Add(this.wbThesaurus);
            this.tabThesaurus.Location = new System.Drawing.Point(4, 22);
            this.tabThesaurus.Name = "tabThesaurus";
            this.tabThesaurus.Size = new System.Drawing.Size(904, 489);
            this.tabThesaurus.TabIndex = 2;
            this.tabThesaurus.Text = "Thesaurus";
            this.tabThesaurus.UseVisualStyleBackColor = true;
            // 
            // wbThesaurus
            // 
            this.wbThesaurus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbThesaurus.Location = new System.Drawing.Point(0, 0);
            this.wbThesaurus.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbThesaurus.Name = "wbThesaurus";
            this.wbThesaurus.ScriptErrorsSuppressed = true;
            this.wbThesaurus.Size = new System.Drawing.Size(904, 489);
            this.wbThesaurus.TabIndex = 0;
            // 
            // tabNews
            // 
            this.tabNews.Controls.Add(this.wbNews);
            this.tabNews.Location = new System.Drawing.Point(4, 22);
            this.tabNews.Name = "tabNews";
            this.tabNews.Padding = new System.Windows.Forms.Padding(3);
            this.tabNews.Size = new System.Drawing.Size(904, 489);
            this.tabNews.TabIndex = 1;
            this.tabNews.Text = "News";
            this.tabNews.UseVisualStyleBackColor = true;
            // 
            // wbNews
            // 
            this.wbNews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbNews.Location = new System.Drawing.Point(3, 3);
            this.wbNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbNews.Name = "wbNews";
            this.wbNews.ScriptErrorsSuppressed = true;
            this.wbNews.Size = new System.Drawing.Size(898, 483);
            this.wbNews.TabIndex = 0;
            // 
            // tabWebSearch
            // 
            this.tabWebSearch.Controls.Add(this.wbWebSearch);
            this.tabWebSearch.Location = new System.Drawing.Point(4, 22);
            this.tabWebSearch.Name = "tabWebSearch";
            this.tabWebSearch.Size = new System.Drawing.Size(904, 489);
            this.tabWebSearch.TabIndex = 3;
            this.tabWebSearch.Text = "Web Search";
            this.tabWebSearch.UseVisualStyleBackColor = true;
            // 
            // wbWebSearch
            // 
            this.wbWebSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWebSearch.Location = new System.Drawing.Point(0, 0);
            this.wbWebSearch.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWebSearch.Name = "wbWebSearch";
            this.wbWebSearch.ScriptErrorsSuppressed = true;
            this.wbWebSearch.Size = new System.Drawing.Size(904, 489);
            this.wbWebSearch.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 756);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.Text = "GRE Helper";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOption1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabDictionary.ResumeLayout(false);
            this.tabThesaurus.ResumeLayout(false);
            this.tabNews.ResumeLayout(false);
            this.tabWebSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optOption1;
        private System.Windows.Forms.Label lblCurrentWordLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDictionary;
        private System.Windows.Forms.TabPage tabNews;
        private System.Windows.Forms.Label lblCurrentWord;
        private System.Windows.Forms.RadioButton optOption4;
        private System.Windows.Forms.RadioButton optOption3;
        private System.Windows.Forms.RadioButton optOption2;
        private System.Windows.Forms.TabPage tabThesaurus;
        private System.Windows.Forms.WebBrowser wbDictionary;
        private System.Windows.Forms.WebBrowser wbThesaurus;
        private System.Windows.Forms.WebBrowser wbNews;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnShowResponse;
        private System.Windows.Forms.Button btnNextWord;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.PictureBox pbOption1;
        private System.Windows.Forms.PictureBox pbOption4;
        private System.Windows.Forms.PictureBox pbOption3;
        private System.Windows.Forms.PictureBox pbOption2;
        private System.Windows.Forms.Button btnShowLogs;
        private System.Windows.Forms.TabPage tabWebSearch;
        private System.Windows.Forms.WebBrowser wbWebSearch;
    }
}

