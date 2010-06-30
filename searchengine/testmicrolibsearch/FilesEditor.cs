using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    
    public class FilesEditor : System.Windows.Forms.Design.FileNameEditor
    {
        //public override bool IsDropDownResizable
        //{
        //    get
        //    {
        //        //return base.IsDropDownResizable;
        //        return true;

        //    }

        //}
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
            //return base.GetEditStyle(context);
        }
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            //base.InitializeDialog(openFileDialog);
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select the files...";
            
        }
        
        //public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //{
        //    return base.EditValue(context, provider, value);
        //}
        //protected override object CreateInstance(Type itemType)
        //{
        //    MessageBox.Show("we are here!!");
        //    //return base.CreateInstance(itemType);
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.Title = "Select a file";
        //    dlg.Multiselect = false;
        //    dlg.CheckFileExists = true;
        //    dlg.CheckPathExists = true;
        //    dlg.SupportMultiDottedExtensions = true;
        //    DialogResult result = dlg.ShowDialog();
        //    if (result == DialogResult.OK && dlg.FileName != null && dlg.FileName != string.Empty)
        //    {
                
        //        return dlg.FileName;
                        
        //    }
        //    else
        //    {
        //        return null;
        //    }
            
        //}
    }
}