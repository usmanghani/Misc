using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace tests
{
    public class Book
    {

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _author = string.Empty;
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private DateTime _publishedDate = DateTime.MinValue;
        public DateTime PublishedDate
        {
            get { return _publishedDate; }
            set { _publishedDate = value; }
        }

        private int _pages = 0;
        public int Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        private string _category = string.Empty;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _filename = string.Empty;
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private string _directoryName = string.Empty;
        public string DirectoryName
        {
            get { return _directoryName; }
            set { _directoryName = value; }
        }

        private bool _isDirectory = false;
        public bool IsDirectory
        {
            get { return _isDirectory; }
            set { _isDirectory = value; }
        }

        private List<string> _files = new List<string>();        
        public string[] Files
        {
            get { return _files.ToArray(); }
            set { _files = new List<string>(value); }
        }

        public void AddFile(string filename)
        {
            _files.Add(filename);

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
