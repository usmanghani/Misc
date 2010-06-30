using System;
using System.Collections.Generic;
using System.Text;

namespace AdSearch
{
    class AdFormatter
    {
        public string Format(Advert ad)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            if (ad.GetDictionary().ContainsKey("Title"))
            {
                sb.Append("<h1>");
                sb.Append(ad["Title"]);
                sb.Append("</h1>");
                sb.Append("<br/>");

            }
            sb.Append("<font size = 4>");
            foreach (string k in ad.GetDictionary().Keys)
            {
                if (k.CompareTo("Title") == 0) continue;
                if (ad[k].Trim() == string.Empty) continue;
                sb.Append("<b>");
                sb.Append(k);
                sb.Append(": ");
                sb.Append("</b>");
                sb.Append(ad[k]);
                sb.Append("<br/>");

            }

            sb.Append("</font></body></html>");

            return sb.ToString();

        }

    }
}
