using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    public partial class testpropgrid : Form
    {
        public testpropgrid()
        {
            InitializeComponent();
        }

        private void testpropgrid_Load(object sender, EventArgs e)
        {
            //propgridobject obj = new propgridobject();
            //obj.Name = "usman";
            //obj.Property1 = "prop1";
            //obj.Title = "title";
            //obj.Property2 = "prop2";
            //obj.Property3 = proptype.Ten;

            //propertyGrid1.SelectedObject = obj;

            Book b = new Book();
            propertyGrid1.SelectedObject = b;


        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //MessageBox.Show(e.ChangedItem.Label + " : " + e.ChangedItem.Value);
            
        }
    }
}