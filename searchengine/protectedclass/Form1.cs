using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace protectedclass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyProtectedClass myclass = MyProtectedClass.CreateProtectedInstance("usmanghani2");
            //MyProtectedClass myclass2 = new MyProtectedClass();

            if (myclass != null)
            {
                MessageBox.Show("Class Construction Succeeded.");
            }
            else
            {
                MessageBox.Show("Class Construction Failed.");
            }

        }
    }
    public class MyProtectedClass
    {
        public static MyProtectedClass CreateProtectedInstance(string password)
        {
            if (password == "usmanghani") return new MyProtectedClass();
            else return null;

        }
        protected MyProtectedClass()
        {
        }


    }

}