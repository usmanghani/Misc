using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace praytt
{
	/// <summary>
	/// Summary description for WaitDialog.
	/// </summary>
	public class WaitDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
        private ProgressBar progressBar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WaitDialog()
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
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(51, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Wait....";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(57, 48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(199, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // WaitDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(303, 81);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

		}
		#endregion

        private string message;
        public string DisplayMessage
        {
            get { return message; }
            set 
            { 
                message = value;
                updateMessageUI();

            }

        }
        private void updateMessageUI()
        {
            Font font = this.label1.Font;
            Graphics g = this.label1.CreateGraphics();
            SizeF size = g.MeasureString(message, font);
            if (size.Width > this.label1.Width)
            {
                while (size.Width > this.label1.Width) 
                {
                    font = new Font(font.FontFamily.ToString(), font.SizeInPoints - 1);
                    size = g.MeasureString(message, font);
                }

            }
            this.label1.Font = font;
            this.label1.Text = message;

        }

        private int progresstype;
        public int ProgressType
        {
            get { return progresstype; }
            set 
            { 
                progresstype = value;
                if (progresstype == 0)
                    this.progressBar1.Style = ProgressBarStyle.Marquee;
                else
                    this.progressBar1.Style = ProgressBarStyle.Blocks;

            }

        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                updateProgressUI();
            }

        }
        private void updateProgressUI ()
        {
            this.progressBar1.Value = progress;
            this.progressBar1.Update();
            Application.DoEvents();
        }
        public void ScrollProgress()
        {
            progress %= 100;
            updateProgressUI();
        }

        public void UpdateProgress(int increment)
        {
            progress += increment;
            updateProgressUI();

        }


	}
}
