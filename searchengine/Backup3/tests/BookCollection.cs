using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace tests
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

    }
}
