using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;


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
            //throw new Exception("The method or operation is not implemented.");
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
    }
}