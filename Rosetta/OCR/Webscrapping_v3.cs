using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    public static class WebScrapper
    {
        public static string NaiveDigikey(string keyword)
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

        public static string NaiveRS(string keyword)
        {
            // Important to safely dispose of resources
            // WebBrowser is memory intensive
            using (WebBrowser wb = new WebBrowser())
            using (WebClient wc = new WebClient())
            {
                // Add User-Agent data to access data with 403 errors
                wc.Headers.Add("User-Agent: Other");

                // Search string with keyword
                System.Uri searchUrl = new System.Uri("https://au.rs-online.com/web/c/?sra=oss&r=t&searchTerm=" + keyword);
                var downloadedHTML = wc.DownloadString(searchUrl);

                // Startup paremeters to WebBrowser
                wb.ScriptErrorsSuppressed = true;
                wb.DocumentText = downloadedHTML;
                wb.Document.OpenNew(true);
                wb.Document.Write(downloadedHTML);
                wb.Refresh();

                // RS component-Specific DOM Navigation

                //Unique id only exists in Scenrio 2
                HtmlElement d = wb.Document.GetElementById("facetFiltersResultsContainer"); 
                if (d ==null)
                {
                    
                    //Scenario 1
                    HtmlElement c = wb.Document.GetElementById("search-message-container");
                    c = c.NextSibling;
                    
                    return c.InnerText;
                    
                }
                
                
                //Scenario 2
                HtmlElement a = wb.Document.GetElementById("results-table");
                HtmlElementCollection data_row_results = a.Children;
                HtmlElementCollection td_tags = a.GetElementsByTagName("TD");
                HtmlElement b;

                
                // Iterate through valid data, return first description
                foreach (HtmlElement e in td_tags)
                {
                    if (e.GetAttribute("className") == "descriptionCol compareContainer")
                    {
                        b = e.FirstChild;
                        return b.InnerText;
                    }
                }              
                

                // Else if none found, return blank description
                return "";
            }

        }

        public static string NaiveElement14(string keyword)
        {
            // Important to safely dispose of resources
            // WebBrowser is memory intensive
            using (WebBrowser wb = new WebBrowser())
            using (WebClient wc = new WebClient())
            {
                // Add User-Agent data to access data with 403 errors
                wc.Headers.Add("User-Agent: Other");

                // Search string with keyword
                System.Uri searchUrl = new System.Uri("https://au.element14.com/search?st=" + keyword);
                var downloadedHTML = wc.DownloadString(searchUrl);

                // Startup paremeters to WebBrowser
                wb.ScriptErrorsSuppressed = true;
                wb.DocumentText = downloadedHTML;
                wb.Document.OpenNew(true);
                wb.Document.Write(downloadedHTML);
                wb.Refresh();
                
                
                // Element14-Specific DOM Navigation
                HtmlElement a = wb.Document.GetElementById("sProdList");
                HtmlElementCollection data_row_results = a.Children;
                HtmlElementCollection td_tags = a.GetElementsByTagName("TD");
                HtmlElement b;

                // Iterate through valid data, return first description
                foreach (HtmlElement e in td_tags)
                {
                    if (e.GetAttribute("className") == "description")
                    {
                        b = e.FirstChild;                     
                        return b.InnerText;
                    }
                }
                

                // Else if none found, return blank description
                return "";


            }

        }

        public static string NaiveMouser(string keyword)
        {
            // Important to safely dispose of resources
            // WebBrowser is memory intensive
            using (WebBrowser wb = new WebBrowser())
            using (WebClient wc = new WebClient())
            {
                // Add User-Agent data to access data with 403 errors
                wc.Headers.Add("User-Agent: Other");
                
                // Search string with keyword
                System.Threading.Thread.Sleep(1000);
                System.Uri searchUrl = new System.Uri("https://au.mouser.com/Search/Refine.aspx?Keyword=" + keyword);
                System.Threading.Thread.Sleep(1000);
                var downloadedHTML = wc.DownloadString(searchUrl);

                // Startup paremeters to WebBrowser
                wb.ScriptErrorsSuppressed = true;
                wb.DocumentText = downloadedHTML; 
                wb.Document.OpenNew(true);
                wb.Document.Write(downloadedHTML);
                wb.Refresh();


                // Mouser-Specific DOM Navigation
                System.Threading.Thread.Sleep(1000);

                //Unique id only exists in Scenrio 1
                HtmlElement d = wb.Document.GetElementById("spnDescription");
                
                if (d != null)
                {
                    //Scenario 1
                    // return d.InnerText;
                    return "";
                }
                
                System.Threading.Thread.Sleep(1000);
                //Scenrio 2
                HtmlElement a = wb.Document.GetElementById("ctl00_ContentMain_SearchResultsGrid_grid_ctl03_ctl04_pnlLarnMore");
                if (a == null)
                {
                    //if none found, return blank description
                    return "";
                }
                a = a.Parent;
                return a.InnerText;

                
                
                

            }

        }

    }


}
