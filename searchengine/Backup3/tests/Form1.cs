using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;


namespace tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopulateTreeViewFromXml(treeView1, "booklist.xml");

        }

        private void PopulateTreeViewFromXml(TreeView tv, string filename)
        {
            
            if (!System.IO.File.Exists(filename))
            {
                MessageBox.Show("File does not exist!!");
                return;

            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode booksnode = doc.GetElementsByTagName("Books")[0];
            foreach (XmlNode booknode in booksnode.ChildNodes)
            {
                TreeNode node = tv.Nodes.Add(booknode.Attributes["Name"].Value);
                node.Nodes.Add(booknode.Attributes["Author"].Value);
                node.Nodes.Add(booknode.Attributes["PublishDate"].Value);
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XPathDocument doc = new XPathDocument("xmldata.xml");
            XPathNavigator nav = doc.CreateNavigator();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("xmldata.xml");
            XmlNodeList catnodes = xdoc.GetElementsByTagName("catalog");
            foreach (XmlNode catnode in catnodes)
            {
                XmlNodeList cdsnode = catnode.ChildNodes;
                foreach (XmlNode cdnode in cdsnode)
                {
                    XmlNode title = cdnode.SelectSingleNode("title");
                    if (title == null) MessageBox.Show("eat rubberband");
                    else
                    {
                        MessageBox.Show(title.InnerText);
                    }

                    
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));
            FileStream stream = new FileStream("c:\\testdata2.xml", FileMode.OpenOrCreate);


            Book b1 = new Book();
            Book b2 = new Book();

            b1.Title = "Book 1";
            b1.Author = "Author of Book 1";
            b1.Category = "Category 1";
            b1.Pages = 100;
            b1.PublishedDate =  DateTime.Parse ( "Jan 15 2005" );
            b1.FileName = "c:/book1.txt";

            b2.Title = "Book 2";
            b2.Author = "Author of Book2";
            b2.Category = "Category 1";
            b2.Pages = 100;
            b1.PublishedDate = DateTime.Parse("Jun 3 1984");
            b2.FileName = "c:/book2.txt";

            BookCollection coll = new BookCollection();
            coll.AddBook(b1);
            coll.AddBook(b2);

            serializer.Serialize(stream, coll);
            stream.Close();


        }
    }
}