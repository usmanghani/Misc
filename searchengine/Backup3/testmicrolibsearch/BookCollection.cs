using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace testmicrolibsearch
{
    public class BookCollection
    {

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

        public void AddBook(Book b)
        {
            _books.Add(b);

        }

        public void RemoveBook(string id)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].BookID == id) _books.RemoveAt(i);
            }

        }
    }
}
