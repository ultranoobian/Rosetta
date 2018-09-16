using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCR
{
    public static class WebScrapper
    {
        public static  string NaiveDigikey(string keyword)
        {
            // Important to safely dispose of resources
            // WebBrowser is memory intensive
            using (WebBrowser wb = new WebBrowser())
            using (WebClient wc = new WebClient())
            {
                // Add User-Agent data to access data with 403 errors
                wc.Headers.Add("User-Agent: Other");

                // Search string with keyword
                System.Uri searchUrl = new System.Uri("https://www.digikey.com.au/products/en?keywords=" + keyword);
                var downloadedHTML = wc.DownloadString(searchUrl);

                // Startup paremeters to WebBrowser
                wb.ScriptErrorsSuppressed = true;
                wb.DocumentText = downloadedHTML;
                wb.Document.OpenNew(true);
                wb.Document.Write(downloadedHTML);
                wb.Refresh();

                // Digikey-Specific DOM Navigation
                HtmlElement a = wb.Document.GetElementById("lnkPart");
                HtmlElementCollection data_row_results = a.Children;
                HtmlElementCollection td_tags = a.GetElementsByTagName("TD");

                // Iterate through valid data, return first description
                foreach (HtmlElement e in td_tags)
                {
                    if (e.GetAttribute("className") == "tr-description")
                    {
                        return e.InnerText;
                    }
                }
                // Else if none found, return blank description
                return "";
            }
        }
    }
}
