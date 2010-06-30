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

        private ChildClass _child = new ChildClass();
        public ChildClass Child
        {
            get { return _child; }
            set { _child = value; }
        }
        private System.Drawing.Point _pt;
        public System.Drawing.Point Point
        {
            get { return _pt; }
            set { _pt = value; }
        }



    }
    [TypeConverter(typeof(MyConverter))]
    public class ChildClass
    {
        private int _x = 0;
        [NotifyParentProperty(true)]
        public int X
        {
            get { return _x; }
            set { _x = value; }

        }

        private int _y = 0;
        [NotifyParentProperty(true)]
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public override string ToString()
        {
            return _x.ToString() + ", " + _y.ToString();
        }

    }
    public class MyConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String)) && (value is ChildClass))
            {
                
                ChildClass c = value as ChildClass;
                return c.X.ToString() + ", " + c.Y.ToString();

            }
            return base.ConvertTo(context, culture, value, destinationType);
            
        }
    }


}
