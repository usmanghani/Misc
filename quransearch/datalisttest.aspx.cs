using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class datalisttest : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 101; i++)
        {
            list.Add(i);

        }
        //DataList1.DataSource = list;
        //DataList1.DataBind();
        //Repeater1.DataSource = list;
        //Repeater1.DataBind();
        GridView1.AllowPaging = true;
        //GridView1.AutoGenerateSelectButton = true;
        GridView1.DataSource = list;
        GridView1.DataBind();
        GridView1.SelectedIndex = -1;
        

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //e.Cancel = false;
        //GridView1.DataBind();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        Label1.Text = (GridView1.PageIndex * GridView1.PageSize + GridView1.SelectedIndex).ToString();
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = (GridView1.PageIndex * GridView1.PageSize + GridView1.SelectedIndex).ToString();   
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Show")
        {
            string temp = string.Empty;
            int index = Convert.ToInt32(e.CommandArgument);
            Label1.Text = (GridView1.PageIndex * GridView1.PageSize + index).ToString();
            GridView1.SelectedIndex = index;
            GridViewRow row = GridView1.Rows[index];
            Label thing = row.FindControl("lblDataItem") as Label;
            //foreach (TableCell cell in row.Cells)
            //{
            //    temp +=  + "<br /><br />";

            //}
            Label2.Text = thing.Text;
            
        }

    }
}
