namespace TableStorageTest
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
            this.btnCreateTables = new System.Windows.Forms.Button();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnAddToCache = new System.Windows.Forms.Button();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnReadFromCache = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateTables
            // 
            this.btnCreateTables.Location = new System.Drawing.Point(255, 169);
            this.btnCreateTables.Name = "btnCreateTables";
            this.btnCreateTables.Size = new System.Drawing.Size(74, 42);
            this.btnCreateTables.TabIndex = 0;
            this.btnCreateTables.Text = "Create Tables";
            this.btnCreateTables.UseVisualStyleBackColor = true;
            this.btnCreateTables.Click += new System.EventHandler(this.btnCreateTables_Click);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(110, 15);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(138, 20);
            this.txtQuery.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Location/Address";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(254, 15);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.HorizontalScrollbar = true;
            this.lstResults.Location = new System.Drawing.Point(16, 73);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(232, 134);
            this.lstResults.TabIndex = 4;
            // 
            // btnAddToCache
            // 
            this.btnAddToCache.Location = new System.Drawing.Point(254, 73);
            this.btnAddToCache.Name = "btnAddToCache";
            this.btnAddToCache.Size = new System.Drawing.Size(74, 42);
            this.btnAddToCache.TabIndex = 5;
            this.btnAddToCache.Text = "Add to Cache";
            this.btnAddToCache.UseVisualStyleBackColor = true;
            this.btnAddToCache.Click += new System.EventHandler(this.btnAddToCache_Click);
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(16, 54);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(42, 13);
            this.lblResults.TabIndex = 6;
            this.lblResults.Text = "Results";
            // 
            // btnReadFromCache
            // 
            this.btnReadFromCache.Location = new System.Drawing.Point(255, 121);
            this.btnReadFromCache.Name = "btnReadFromCache";
            this.btnReadFromCache.Size = new System.Drawing.Size(74, 42);
            this.btnReadFromCache.TabIndex = 5;
            this.btnReadFromCache.Text = "Read From Cache";
            this.btnReadFromCache.UseVisualStyleBackColor = true;
            //this.btnReadFromCache.Click += new System.EventHandler(this.btnReadFromCache_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 447);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.btnReadFromCache);
            this.Controls.Add(this.btnAddToCache);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.btnCreateTables);
            this.Name = "Form1";
            this.Text = "Form1";
            //this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTables;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Button btnAddToCache;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnReadFromCache;
    }
}

