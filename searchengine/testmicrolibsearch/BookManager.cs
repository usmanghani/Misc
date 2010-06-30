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
        public class BookManagerChangeEventArgs : System.EventArgs
        {
            public BookManagerChangeEventArgs(Book b)
            {
                _book = b;
            }
            private Book _book;
            public Book Book
            {
                get { return _book; }

            }

        }

        public delegate void OnBookAddedDelegate(object sender, BookManagerChangeEventArgs e);
        public event OnBookAddedDelegate BookAdded;

        public delegate void OnBookRemovedDelegate(object sender, BookManagerChangeEventArgs e);
        public event OnBookRemovedDelegate BookRemoved;

        public delegate void OnRefreshedDelegate(object sender);
        public event OnRefreshedDelegate Refreshed;


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
            coll.BookAdded += new BookCollection.OnBookAddedDelegate(coll_BookAdded);
            coll.BookRemoved += new BookCollection.OnBookRemovedDelegate(coll_BookRemoved);
        }

        void coll_BookRemoved(object sender, BookCollection.CollectionChangeEventArgs e)
        {
            _addBookToList(e.Book);
            _addBookToMap(e.Book);


        }

        private void _addBookToMap(Book book)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void _addBookToList(Book book)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void coll_BookAdded(object sender, BookCollection.CollectionChangeEventArgs e)
        {
            _removeBookFromList(e.Book);
            _removeBookFromMap(e.Book);

        }

        private void _removeBookFromMap(Book book)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void _removeBookFromList(Book book)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public BookManager(string filename)
            : this()
        {

            this._filename = filename;
            coll.BookAdded += new BookCollection.OnBookAddedDelegate(coll_BookAdded);
            coll.BookRemoved += new BookCollection.OnBookRemovedDelegate(coll_BookRemoved);
        }

        public List<Book> Books
        {
            get { return _bookList; }
        }
        public Book[] BookCollection
        {
            get { return coll.Books; }

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
        public void LoadBookList()
        {
            LoadBookListFromFile2(_filename);

        }

        public void LoadBookListFromFile2(string filename)
        {
            _filename = filename;
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
        public void Refresh(bool autoCommit)
        {
            if (autoCommit)
                CommitChanges(_filename);

            LoadBookListFromFile2(_filename);

            Refreshed(this);

            
        }
        public void CommitChanges()
        {
            CommitChanges(_filename);

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
            this.AddBook(b);

        }

        public void AddBook(Book b)
        {
            BookAdded(this, new BookManagerChangeEventArgs(coll.AddBook(b)));
            
        }
        public void RemoveBook(string id)
        {
            
            BookRemoved(this, new BookManagerChangeEventArgs(coll.RemoveBook(id)));

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
