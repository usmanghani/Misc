using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace testmicrolibsearch
{
    public class BookManager
    {
        Dictionary<string, Book> _bookMap = new Dictionary<string, Book>();
        List<Book> _bookList = new List<Book>();
        string _filename = string.Empty;

        private enum _pathType
        {
            File,
            Directory
        }

        private string _canonicalizeFileName(string filename)
        {
            return Path.GetFullPath(filename).ToLower();

        }
        private string _canonicalizeDirName(string dirname)
        {
            
            dirname = dirname.Replace('/', '\\');
            if (dirname.EndsWith("\\"))
            {
                return Path.GetDirectoryName(dirname).ToLower();

            }
            else
            {
                return Path.GetDirectoryName(dirname + "\\").ToLower();

            }


        }

        private string _canonicalizePath(string path, _pathType pathType)
        {
            switch (pathType)
            {
                case _pathType.File:
                    return _canonicalizeFileName(path);
                    
                case _pathType.Directory:
                    return _canonicalizeDirName(path);
                    
                default:
                    return null;

            }

            return null;

        }

        public BookManager()
        {
        }

        public BookManager(string filename)
        {
            this._filename = filename;
        }

        public List<Book> Books
        {
            get { return _bookList; }
        }

        public Book this[string filename]
        {
            get { return GetBookFromFileName(filename); }
        }

        public Book GetBookFromFileName(string filename)
        {

            Book result = null;

            try
            {
                result = _bookMap[_canonicalizePath(filename, _pathType.File)];
            }
            catch (KeyNotFoundException ex)
            {
                //string dirname = Path.GetDirectoryName(filename).ToLower();
                result = _bookMap[_canonicalizePath(filename, _pathType.Directory)];
            }

            return result;

        }

        public string GetBookTitleFromFileName(string filename)
        {
            return GetBookFromFileName(filename).Title;

        }

        
        public void LoadBookListFromFile(string filename)
        {
            if (!BookListExists(filename))
            {
                CreateNewBookListFile(filename);

            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNodeList nodelist = doc.GetElementsByTagName("Books");
            foreach (XmlNode node in nodelist)
            {
                XmlNodeList books = node.ChildNodes;
                foreach (XmlNode book in books)
                {
                    
                    Book b = new Book();

                    XmlNode temp;

                    temp = book.SelectSingleNode("Title");
                    if (temp != null) b.Title = temp.InnerText;
                    
                    temp = book.SelectSingleNode("Author");
                    if (temp != null) b.Author = temp.InnerText;
                    
                    temp = book.SelectSingleNode("Category");
                    if (temp != null) b.Category = temp.InnerText;

                    temp = book.SelectSingleNode("Pages");
                    if (temp != null) b.Pages = Convert.ToInt32(temp.InnerText);

                    temp = book.SelectSingleNode("PublishedDate");
                    if (temp != null) b.PublishedDate = temp.InnerText;

                    temp = book.SelectSingleNode("FileName");
                    if (temp != null) b.FileName = _canonicalizePath(temp.InnerText, _pathType.File);

                    temp = book.SelectSingleNode("Directory");
                    if (temp != null) 
                    {
                        b.DirectoryName = _canonicalizePath(temp.InnerText, _pathType.Directory);
                        b.IsDirectory = true;
                    }


                    _bookList.Add(b);


                }

            }

            foreach (Book b in _bookList)
            {
                if (b.IsDirectory == true)
                {
                    //_bookMap.Add(Path.GetDirectoryName(b.DirectoryName.ToLower()), b);
                    _bookMap.Add(_canonicalizePath(b.DirectoryName, _pathType.Directory), b);


                }
                else
                {
                    //_bookMap.Add(b.FileName.ToLower(), b);]
                    _bookMap.Add(_canonicalizePath(b.FileName, _pathType.File), b);


                }

            }

        }
        private bool BookListExists(string filename)
        {
            return File.Exists(filename);

        }
        private void CreateNewBookListFile(string filename)
        {

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("Books"));
            doc.Save(filename);

        }


    }
}
