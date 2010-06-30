using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using LynkX.XFilter;

namespace testifilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TextFilter filter = new TextFilter(@"c:\urdutest.pdf");
            textBox1.Text = filter.DocumentText.ToString();
            IDictionary dict = filter.DocumentProperties;
            textBox1.AppendText(dict.Count.ToString());
            string props = string.Empty;
            string vals = string.Empty;
            foreach (string s in dict.Keys)
            {
                props += s;
                vals += dict[s];

            }
            textBox1.AppendText(props);


        }
    }
}