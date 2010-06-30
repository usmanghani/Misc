using System;
using System.Collections.Generic;
using System.Text;

namespace testmicrolibsearch
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

        private string _publishedDate = string.Empty;

        public string PublishedDate
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

        public void AddFile(string filename)
        {
            _files.Add(filename);

        }

        
        public string[] Files
        {
            get { return _files.ToArray(); }
        }


    }
}
