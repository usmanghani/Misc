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
        private BookManager _bookManager = new BookManager("c:/testdata2.xml");

        public MainForm()
        {
            InitializeComponent();

            _bookManager.BookAdded += new BookManager.OnBookAddedDelegate(bookManager_BookAdded);
            _bookManager.BookRemoved += new BookManager.OnBookRemovedDelegate(bookManager_BookRemoved);
            _bookManager.Refreshed += new BookManager.OnRefreshedDelegate(bookManager_Refreshed);
        }

        void bookManager_Refreshed(object sender)
        {
            _loadBookDataInTreeView();
            _bookManager.CommitChanges();


        }

        void bookManager_BookRemoved(object sender, BookManager.BookManagerChangeEventArgs e)
        {
            //_loadBookDataInTreeView();
            _removeBookFromTreeView(e.Book);
            _bookManager.CommitChanges();


        }

        private void _removeBookFromTreeView(Book book)
        {
            foreach (TreeNode node in _findNodeFromID(book.BookID))
                tvBooks.Nodes.Remove(node);

            //TreeNode node = _findNodeFromID(book.BookID);
            //tvBooks.Nodes.Remove(node);

        }

        private TreeNode[] _findNodeFromID(string ID)
        {
            return tvBooks.Nodes.Find("ID: " + ID, true);
            
            //foreach (TreeNode node in tvBooks.Nodes)
            //{
            //    foreach (TreeNode child in node.Nodes)
            //    {
            //        if (child.Tag == "ID")
            //        {
            //            if (child.Text == "ID: " + ID)
            //                return child;


            //        }

            //    }

            //}

        }

        void bookManager_BookAdded(object sender, BookManager.BookManagerChangeEventArgs e)
        {
            //_loadBookDataInTreeView();
            _appendBookToTreeView(e.Book);
            _bookManager.CommitChanges();

        }

        private void _appendBookToTreeView(Book book)
        {
            this.tvBooks.Nodes.Add(CreateNodeFromBookData(book));
        }

        private void _loadBookDataInTreeView()
        {

            double progress = 0.0;
            
            statusText.Text = "Loading Books List...";
            statusProgress.Value = (int)progress;

            statusText.Visible = true;
            statusProgress.Visible = true;
            
            tvBooks.Nodes.Clear();

            foreach (Book b in _bookManager.BookCollection)
            {
                TreeNode node = CreateNodeFromBookData(b);

                tvBooks.Nodes.Add(node);

                progress += (100.0 / _bookManager.Books.Count);

                statusProgress.Value = (int)progress;


            }
            statusProgress.Visible = false;
            statusText.Visible = false;
            

        }

        private TreeNode CreateNodeFromBookData(Book b)
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

            return node;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _bookManager.LoadBookList();
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
            AddEditBookControl editor = new AddEditBookControl();
            editor.BookManager = this._bookManager;
            _addTab(editor, "Add/Edit Book");
            
        }

        private void _addSearchResult()
        {
            if (txtSearch.Text == string.Empty)
            {
                return;

            }
            SearchResultControl searchresults = new SearchResultControl();
            _addTab(searchresults, txtSearch.Text + " - Results");
            

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
            
            AdvancedSearch advsearch = new AdvancedSearch();
            _addTab(advsearch, "Advanced Search");

        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This option is under construction. Sorry for inconvenience.");
        }

        private void _addTab(UserControl ctrl, string title)
        {
            TabPage page = new TabPage(title);
            page.Controls.Add(ctrl);
            tabMainTabControl.TabPages.Add(page);
            page.Focus();

        }

    }
}