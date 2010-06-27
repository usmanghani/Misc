using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using DotFermion;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            _doSearch();
        }

        //if (Page.Request.QueryString["contents"] != null)
        //    divResults.InnerHtml = (string)Session[Page.Request.QueryString["contents"]];

        
    }
    private void _doSearch()
    {
        if (txtSearch.Text == string.Empty) return;

        //ClearResultsList();
        //lstResults.Items.Clear();

        Searcher searcher = CreateSearcher();

        string query = txtSearch.Text;
        DateTime starttime = DateTime.Now;
        SearchResult[] results = searcher.FastSearch(query, new string[] { "pid", "sid", "ayatno" });
        DateTime endtime = DateTime.Now;
        TimeSpan time = endtime - starttime;
        string duration = time.TotalSeconds.ToString();
        //Session[Page.Session.SessionID + "_results"] = results;

        lblHits.Text = "A search for " + query + " returned " + results.Length.ToString() + " result(s) in " + duration + " seconds.";
        PopulateSearchList(results);
    }

    private Searcher CreateSearcher()
    {
        Searcher searcher = null;

        if (optOperator.SelectedValue == "AND")
            searcher = new Searcher(Server.MapPath("~/kanzuliman"), DefaultOperator.AND);
        else
            searcher = new Searcher(Server.MapPath("~/kanzuliman"));

        return searcher;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        _doSearch();
    }

    private void PopulateSearchList(SearchResult[] results)
    {
        List<BinderSearchResult> bsrs = new List<BinderSearchResult>();
        foreach (SearchResult result in results)
        {
            BinderSearchResult bsr = new BinderSearchResult();
            bsr.Para = result.GetDocProperty("pid");
            bsr.Surah = result.GetDocProperty("sid");
            bsr.Ayah = result.GetDocProperty("ayatno");
            bsrs.Add(bsr);
        }

        grdResults.AllowPaging = true;
        grdResults.DataSource = bsrs;
        grdResults.DataBind();


        //foreach (SearchResult result in results)
        //{
        //    string temp = string.Empty;

        //    string pid = result.GetDocProperty("pid");
        //    string sid = result.GetDocProperty("sid");
        //    string ayatno = result.GetDocProperty("ayatno");

        //    temp = "Para: " + pid + ", Surat: " + sid + ", Ayat: " + ayatno;
        //    //lstResults.Items.Add(temp);

        //}

    }

    private void ClearResultsList()
    {

        grdResults.DataSource = null;
        grdResults.DataBind();

        //throw new Exception("The method or operation is not implemented.");
        
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (Session.IsNewSession) Label1.Text = "Session Expired!!";

        //if (lstResults.SelectedIndex < 0) return;
        //SearchResult[] results = (SearchResult[])Session[Session.SessionID + "_results"];

        //if (results == null) Label1.Text = "Session Expired!!";
        ////SearchResult r = Utils.GetFastSearchResultFragments(ref results[lstResults.SelectedIndex]);
        
        //System.Text.StringBuilder result = new System.Text.StringBuilder(string.Empty);
        //result.Append("<font face=Arial size=5>");
        
        //foreach (string s in r.GetFragments())
        //    result.Append(s).Append("<br/><hr/><br/>");

        
        //result.Append("</font><a href=\"").Append("Default.aspx?contents=").Append(Session.SessionID+"_contents").Append("\">View Original Document...</a>");
        //result.Replace("\n", "<br/>");

        //SearchResult sr = (SearchResult)results[lstResults.SelectedIndex];
        //string hcontents = Utils.GetHilitedContentsWithoutHeaders(sr);
        ////Session[Session.SessionID + "_contents"] = hcontents;

        //divResults.InnerHtml = hcontents.ToString();

                
    }
    protected void grdResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = false;
        grdResults.PageIndex = e.NewPageIndex;
        grdResults.DataBind();
    }
    protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Show")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            
            string para = grdResults.Rows[index].Cells[1].Text;
            string sura = grdResults.Rows[index].Cells[2].Text;
            string ayah = grdResults.Rows[index].Cells[3].Text;

            string query = "pid:" + para + " sid:" + sura + " ayatno:" + ayah;
            
            SearchResult[] results = CreateSearcher().FastSearch(query, new string[] { "pid", "sid", "ayatno" });
            //SearchResult[] results = CreateSearcher().FastSearch(query);
            results[0].Query = txtSearch.Text;
            divResults.InnerHtml = Utils.GetHilitedContentsWithoutHeaders(results[0]);

        }
    }
}