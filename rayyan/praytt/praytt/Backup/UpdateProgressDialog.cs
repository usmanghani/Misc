using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace praytt
{
    public partial class UpdateProgressDialog : Form
    {
        public UpdateProgressDialog()
        {
            InitializeComponent();
        }
        public void UpdateProgress ( int percent ) 
        {
            this.progressBar1.Value = percent;

        }

    }
}