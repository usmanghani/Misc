using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotFermion;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Searcher searcher = new Searcher("c:\\indexthings3");
        string query = txtSearch.Text;
        SearchResult[] results = searcher.Search(query);
        foreach (SearchResult result in results)
        {
            
            
        }


    }
}