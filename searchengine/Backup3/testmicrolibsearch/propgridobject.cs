using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace testmicrolibsearch
{
    public enum proptype
    {
        Ten,
        Twenty

    }

    [DefaultProperty("Name")]
    public class propgridobject
    {
        string name;
        string title;
        string prop1;
        string prop2;
        proptype prop3;

        [Description("Name of the object"), Category("Action"), DefaultValue("Usman")]
        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        [Description("Title of the object"), Category("Action")]
        public string Title
        {
            get { return title; }
            set { title = value; }

        }
        public string Property1
        {
            get { return prop1; }
            set { prop1 = value; }

        }

        
        public string Property2
        {
            get { return prop2; }
            set { prop2 = value; }
        }

        
                        
        public proptype Property3
        {
            get { return prop3; }
            set { prop3 = value; }
        }



    }
}
