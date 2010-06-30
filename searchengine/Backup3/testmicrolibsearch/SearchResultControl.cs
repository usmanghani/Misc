using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    public partial class SearchResultControl : UserControl
    {
        public SearchResultControl()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TabPage page = this.Parent as TabPage;
            TabControl ctrl = page.Parent as TabControl;
            ctrl.TabPages.Remove(page);
            
        }
    }
}
