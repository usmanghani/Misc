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
using Lucene.Net.Documents;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Page.Request.QueryString["contents"] != null)
        //    divResults.InnerHtml = (string)Session[Page.Request.QueryString["contents"]];

        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text == string.Empty) return;

        lstResults.Items.Clear();

        Searcher searcher = null;

        if (optOperator.SelectedValue == "AND")
            searcher = new Searcher(Server.MapPath("~/kanzuliman"), DefaultOperator.AND);
        else
            searcher = new Searcher(Server.MapPath("~/kanzuliman"));

        string query = txtSearch.Text;
        SearchResult[] results = searcher.FastSearch(query, new string[] { "pid", "sid", "ayatno" });
        Session[Page.Session.SessionID + "_results"] = results;

        lblHits.Text = results.Length.ToString() + " hit(s).";

        foreach (SearchResult result in results)
        {
            string temp = string.Empty;
            Document doc = result.Document;
            string pid = doc.Get("pid");
            string sid = doc.Get("sid");
            string ayatno = doc.Get("ayatno");

            temp = "Para: " + pid + ", Surat: " + sid + ", Ayat: " + ayatno;
            lstResults.Items.Add(temp);
                
        }
        
        

    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session.IsNewSession) Label1.Text = "Session Expired!!";

        if (lstResults.SelectedIndex < 0) return;
        SearchResult[] results = (SearchResult[])Session[Session.SessionID + "_results"];

        if (results == null) Label1.Text = "Session Expired!!";
        //SearchResult r = Utils.GetFastSearchResultFragments(ref results[lstResults.SelectedIndex]);
        
        //System.Text.StringBuilder result = new System.Text.StringBuilder(string.Empty);
        //result.Append("<font face=Arial size=5>");
        
        //foreach (string s in r.GetFragments())
        //    result.Append(s).Append("<br/><hr/><br/>");

        
        //result.Append("</font><a href=\"").Append("Default.aspx?contents=").Append(Session.SessionID+"_contents").Append("\">View Original Document...</a>");
        //result.Replace("\n", "<br/>");

        SearchResult sr = (SearchResult)results[lstResults.SelectedIndex];
        string hcontents = Utils.GetHilitedContentsWithoutHeaders(sr);
        //Session[Session.SessionID + "_contents"] = hcontents;

        divResults.InnerHtml = hcontents.ToString();

                
    }
}