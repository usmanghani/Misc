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
                (node.Nodes.Add("ID: " + b.BookID) as TreeNode).Tag = "ID";
                (node.Nodes.Add("Author: " + b.Author) as TreeNode).Tag = "Author";
                (node.Nodes.Add("Pages: " + b.Pages) as TreeNode).Tag = "Pages";
                (node.Nodes.Add("Publication Date: " + b.PublishedDate.ToShortDateString()) as TreeNode).Tag = "PublicationDate";
                (node.Nodes.Add("Category: " + b.Category) as TreeNode).Tag = "Category";

                if (!b.IsDirectory)
                    (node.Nodes.Add("FileName: " + b.FileName) as TreeNode).Tag = "FileName";

                if (b.IsDirectory)
                {
                    (node.Nodes.Add("Directory: " + b.DirectoryName) as TreeNode).Tag = "Directory";
                    TreeNode node2 = new TreeNode("Files");
                    foreach (string file in b.Files)
                    {
                        node2.Nodes.Add(file);

                    }
                    node.Nodes.Add(node2);

                }


                tvBooks.Nodes.Add(node);

                progress += (100.0 / bookManager.Books.Count);

                statusProgress.Value = (int)progress;


            }
            statusProgress.Visible = false;
            statusText.Visible = false;
            

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bookManager.LoadBookListFromFile2("c:/testdata2.xml");
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabMainTabControl.TabPages.Clear();

        }


        private void btnAddBook_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage("Add/Edit Book");
            page.Controls.Add(new AddEditBookControl());
            tabMainTabControl.TabPages.Add(page);
        }

        private void _addSearchResult()
        {
            if (txtSearch.Text == string.Empty)
            {
                return;

            }
            TabPage page = new TabPage(txtSearch.Text + " - Results");
            SearchResultControl searchresults = new SearchResultControl();
            page.Controls.Add(searchresults);
            tabMainTabControl.TabPages.Add(page);


        }


        private void btnSearch_ButtonClick(object sender, EventArgs e)
        {

            _addSearchResult();

        }

        private void mnuSearch_Click(object sender, EventArgs e)
        {
            _addSearchResult();

        }

        private void mnuAdvancedSearch_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage("Advanced Search");
            AdvancedSearch advsearch = new AdvancedSearch();
            page.Controls.Add(advsearch);
            tabMainTabControl.TabPages.Add(page);

        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This option is under construction. Sorry for inconvenience.");
        }
    }
}