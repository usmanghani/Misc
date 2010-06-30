using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace testmicrolibsearch
{
    public class BookCollection
    {
        public class CollectionChangeEventArgs:System.EventArgs
        {
            public CollectionChangeEventArgs(Book b)
            {
                _book = b;
            }

            private Book _book;
            public Book Book
            {
                get
                {
                    return _book;

                }

            }

        }

        public delegate void OnBookAddedDelegate(object sender, CollectionChangeEventArgs e);
        public event OnBookAddedDelegate BookAdded;

        public delegate void OnBookRemovedDelegate(object sender, CollectionChangeEventArgs e);
        public event OnBookRemovedDelegate BookRemoved;

        
        List<Book> _books = new List<Book>();

        [XmlArray]
        public Book[] Books
        {
            get
            {
                return _books.ToArray();
            }
            set
            {
                _books = new List<Book>(value);

            }
        }

        public Book AddBook(Book b)
        {
            _books.Add(b);
            this.BookAdded(this, new CollectionChangeEventArgs(b));
            return b;

        }

        public Book RemoveBook(string id)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].BookID == id)
                {
                    Book b = _books[i];
                    _books.RemoveAt(i);
                    this.BookRemoved(this, new CollectionChangeEventArgs(_books[i]));
                    return b;
                }
                

            }

            return null;
        }
    }
}
