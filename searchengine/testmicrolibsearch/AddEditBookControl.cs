using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    public partial class AddEditBookControl : UserControl
    {
        private bool _modified = false;

        private Book _book = new Book();
        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }
        BookManager _bookmanager = null;
        public BookManager BookManager
        {
            get { return _bookmanager; }
            set { _bookmanager = value; }

        }

        public AddEditBookControl()
        {
            InitializeComponent();
            this.bookProperties.SelectedObject = _book;
                        
        }
        private void _closeTab()
        {
            TabPage page = this.Parent as TabPage;
            TabControl ctrl = page.Parent as TabControl;
            ctrl.TabPages.Remove(page);

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_modified)
            {
                DialogResult response = MessageBox.Show("Do you want to save this book?", "Search Engine", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (response == DialogResult.Cancel)
                    return;
                if (response == DialogResult.No)
                    _closeTab();
                if (response == DialogResult.Yes)
                    _bookmanager.AddBook(_book);

            }
   
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _bookmanager.AddBook(_book);
        }

        private void bookProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _modified = true;

            if (e.ChangedItem.Label == "Directory Name")
            {
                string dirname = e.ChangedItem.Value as string;
                _book.Files = System.IO.Directory.GetFiles(dirname);
                _book.IsDirectory = true;
                bookProperties.Refresh();
                
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _closeTab();
        }
    }
}
