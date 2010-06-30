using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.Xml.XPath;



namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode booksnode = doc.AppendChild(doc.CreateElement("Books"));
            XmlNode booknode = booksnode.AppendChild(doc.CreateElement("Book"));
            //booksnode.RemoveChild(booknode);
            XmlAttribute bookname = booknode.Attributes.Append(doc.CreateAttribute("Name"));
            bookname.Value = "test";
            
            XmlWriter writer = XmlWriter.Create("booklist.xml");
            doc.WriteTo(writer);
            writer.Close();


            XslCompiledTransform transform = new XslCompiledTransform(true);
            transform.Load("template.xsl");
            transform.Transform("testdata.xml", "testdata.html");

            
            string[] data = { "a", "b", "c" };
            List<string> strings = new List<string>(data);
            string temp = string.Empty;

            foreach (string s in strings)
            {
                temp += s;
            }

            MessageBox.Show(temp);

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
    }
}
