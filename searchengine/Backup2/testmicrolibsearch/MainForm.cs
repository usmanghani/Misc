using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace testmicrolibsearch
{
    public partial class MainForm : Form
    {
        private BookManager bookManager = new BookManager();

        public MainForm()
        {
            InitializeComponent();
        }

        private void _loadBookDataInTreeView()
        {

            double progress = 0.0;
            
            statusText.Text = "Loading Books List...";
            statusProgress.Value = (int)progress;

            statusText.Visible = true;
            statusProgress.Visible = true;
            
            tvBooks.Nodes.Clear();

            foreach (Book b in bookManager.Books)
            {
                TreeNode node = new TreeNode(b.Title);
                node.Nodes.Add("Author: " + b.Author);
                node.Nodes.Add("Pages: " + b.Pages);
                node.Nodes.Add("Publication Date: " + b.PublishedDate);
                node.Nodes.Add("Category: " + b.Category);

                if (!b.IsDirectory)
                    node.Nodes.Add("FileName: " + b.FileName);

                if (b.IsDirectory)
                    node.Nodes.Add("Directory: " + b.DirectoryName);

                tvBooks.Nodes.Add(node);

                progress += (100.0 / bookManager.Books.Count);

                statusProgress.Value = (int)progress;


            }
            statusProgress.Visible = false;
            statusText.Visible = false;
            

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bookManager.LoadBookListFromFile("c:/testdata.xml");
            _loadBookDataInTreeView();

        }

        private void tvBooks_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;

        }

        private void tvBooks_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;

        }

        private void tvBooks_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void tvBooks_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;

        }

        private void tvBooks_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.IsExpanded) e.Node.SelectedImageIndex = 1;
            else e.Node.SelectedImageIndex = 0;
        }

        private void tvBooks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsExpanded) e.Node.SelectedImageIndex = 1;
            else e.Node.SelectedImageIndex = 0;

        }
    }
}