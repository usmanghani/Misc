using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace testmicrolibsearch
{
    public class BookManager
    {
        Dictionary<string, Book> _bookMap = new Dictionary<string, Book>();
        List<Book> _bookList = new List<Book>();
        string _filename = string.Empty;
        BookCollection coll = new BookCollection();

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
            catch (KeyNotFoundException)
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

        public void LoadBookListFromFile2(string filename)
        {
            if (!BookListExists(filename))
            {
                CreateNewBookListFile2(filename);

            }
            FileStream stream = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));
            coll = serializer.Deserialize(stream) as BookCollection;
            stream.Close();
            foreach (Book b in coll.Books)
            {
                _bookList.Add(b);

            }
            _populateBookMap();


        }
        public void CommitChanges(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));
            FileStream stream = new FileStream(filename, FileMode.Open);
            serializer.Serialize(stream, coll);
            stream.Close();


        }
        public void AddBook(string title, string author, DateTime publishedDate, int pages, string category, string filename, string dirname, bool isdirectory, string[] filelist)
        {
            Book b = new Book();
            b.Title = title;
            b.Author = author;
            b.Category = category;
            b.Pages = pages;
            b.PublishedDate = publishedDate;
            b.FileName = filename;
            b.DirectoryName = dirname;
            b.IsDirectory = isdirectory;
            b.Files = filelist;
            coll.AddBook(b);

        }

        public void AddBook(Book b)
        {
            coll.AddBook(b);

        }
        public void RemoveBook(string id)
        {
            coll.RemoveBook(id);

        }

        private void CreateNewBookListFile2(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));
            FileStream stream = new FileStream(filename, FileMode.Create);
            BookCollection coll = new BookCollection();
            serializer.Serialize(stream, coll);
            stream.Close();
        }

        private void _populateBookMap()
        {
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
                    if (temp != null) b.PublishedDate = DateTime.Parse(temp.InnerText);

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
            _populateBookMap();

            
        }
        private bool BookListExists(string filename)
        {
            return File.Exists(filename);

        }
        private void CreateNewBookListFile(string filename)
        {

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("books"));
            doc.Save(filename);

        }
        

    }
}
