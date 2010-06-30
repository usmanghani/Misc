using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace scratch
{
    public partial class SearchResult : Form
    {
        private string _searchResult = string.Empty;
        public string Result
        {
            get
            {
                return _searchResult;

            }
            set
            {
                _searchResult = value;

            }


        }

        public SearchResult(string Title)
        {
            InitializeComponent();
            this.Text = Title;
        }

        private void SearchResult_Shown(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\results.html";
            File.WriteAllText(path, _searchResult);
            webBrowser1.Url = new Uri("file:///" + path);

           
        }
    }
}