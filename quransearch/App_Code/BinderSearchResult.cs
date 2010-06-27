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

/// <summary>
/// Summary description for SearchResults
/// </summary>
public class BinderSearchResult
{
    string _pid;
    public string Para
    {
        get
        {
            return _pid;
        }
        set
        {
            _pid = value;
        }
    }
    string _sid;
    public string Surah
    {
        get
        {
            return _sid;
        }
        set
        {
            _sid = value;
        }

    }
    string _ayah;
    public string Ayah
    {
        get
        {
            return _ayah;
        }
        set
        {
            _ayah = value;
        }

    }

}
