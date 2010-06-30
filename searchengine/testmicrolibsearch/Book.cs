using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms;


namespace testmicrolibsearch
{
    public class Book
    {

        private string _bookID = string.Empty;
        [Description("Unique ID of the book."), DisplayName("Book ID")]
        public string BookID
        {
            get { return _bookID; }
            set { _bookID = value; }

        }


        private string _title = string.Empty;
        [Description("Title of the book.")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _author = string.Empty;
        [Description("Author of the book.")]
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private DateTime _publishedDate = DateTime.MinValue;
        [Description("The date when the book was published."), DisplayName("Published Date")]
        public DateTime PublishedDate
        {
            get { return _publishedDate; }
            set { _publishedDate = value; }
        }

        private int _pages = 0;
        [Description("The number of pages in the book.")]
        public int Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        private string _category = string.Empty;
        [Description("The category to which the book belongs.")]
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _filename = string.Empty;
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof ( System.Drawing.Design.UITypeEditor))
        , Description("The file which contains book data"), DisplayName("File Name")]
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private string _directoryName = string.Empty;
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))
        , Description("Directory which contains the files of the book."), DisplayName("Directory Name"), NotifyParentProperty(true), RefreshProperties(RefreshProperties.Repaint)]
        public string DirectoryName
        {
            get { return _directoryName; }
            set { _directoryName = value; IsDirectory = true; }
        }

        private bool _isDirectory = false;
        [Browsable(false), DisplayName("Is Directory"), Description("Specifies whether the book's data is in the files in a directory, or a single file.")]
        public bool IsDirectory
        {
            get { return _isDirectory; }
            set { _isDirectory = value; }
        }
        
        private List<string> _files = new List<string>();        
        [Description("Specifies the files which contain the book data. Assumes that \"Is Directory\" is true, and the \"Directory Name\" is specified."), 
        Editor(typeof(testmicrolibsearch.FilesEditor), typeof(System.Windows.Forms.Design.FileNameEditor))]
        public string[] Files
        {
            get { return _files.ToArray(); }
            set { _files = new List<string>(value); }
        }

        public void AddFile(string filename)
        {
            _files.Add(filename);
            
        }

        public void RemoveFile(string filename)
        {
            _files.Remove(filename);

        }

        public override string ToString()
        {
            
            StringBuilder sb = new StringBuilder();
            
            sb.Append("Title: ").Append(this.Title).Append(Environment.NewLine);
            sb.Append("Author: ").Append(this.Author).Append(Environment.NewLine);
            sb.Append("Published Date: ").Append(this.PublishedDate).Append(Environment.NewLine);
            sb.Append("Pages: ").Append(this.Pages).Append(Environment.NewLine);
            sb.Append("Category: ").Append(this.Category).Append(Environment.NewLine);
            sb.Append("FileName: ").Append(this.FileName).Append(Environment.NewLine);
            sb.Append("DirectoryName: ").Append(this.DirectoryName).Append(Environment.NewLine);
            sb.Append("IsDirectory: ").Append(this.IsDirectory).Append(Environment.NewLine);


            return sb.ToString();

        }
    }
}
