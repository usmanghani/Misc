using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    public partial class TestBookManager : Form
    {
        public TestBookManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            treeView1.Nodes.Clear();

            BookManager manager = new BookManager("C:\\testdata.xml");
            manager.LoadBookListFromFile("C:\\testdata.xml");
            foreach (Book b in manager.Books)
            {
                TreeNode node = new TreeNode(b.Title);
                node.Nodes.Add("Author: " + b.Author);
                node.Nodes.Add("Pages: " + b.Pages);
                node.Nodes.Add("Publication Date: " + b.PublishedDate);
                node.Nodes.Add("Category: " + b.Category);

                if (!b.IsDirectory) 
                    node.Nodes.Add("FileName: " + b.FileName);

                if (b.IsDirectory)
                    node.Nodes.Add("Directory: " + b.DirectoryName);

                treeView1.Nodes.Add(node);

                
            }

            string b3 = manager["C:/book3/"].Title;
            MessageBox.Show(b3);
            
        }

    }
}