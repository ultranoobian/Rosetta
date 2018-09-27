using System.Windows.Forms;
using System.Net;
using System;
using System.Net.Http;
using System.Xml;

namespace OCR
{
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
                if (d == null)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">Part Number to search for</param>
        /// <returns>First product description, otherwise empty string.</returns>
        public static string MouserAPI_GetDescription(string keyword)
        {
            try
            {
                if (keyword.Trim().Length > 0)
                {
                    Mouser.AccountInfo ai = new Mouser.AccountInfo
                    {
                        PartnerID = "[REDACTED]"
                    };
                    Mouser.MouserHeader mh = new Mouser.MouserHeader
                    {
                        AccountInfo = ai
                    };
                    var sap = new Mouser.SearchAPISoapClient("SearchAPISoap");

                    Mouser.ResultParts results = sap.SearchByPartNumber(mh, keyword, "2");
                    if (results.Parts.Length > 0)
                    {
                        return results.Parts[0].Description;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return "";
        }

        public static string Element14API_GetDescription(string keyword)
        {
            //Construct our URI with our parameters
            UriBuilder uriBuilder = new UriBuilder("https://api.element14.com/catalog/products");
            string[] apiParams = new string[] {
                "callInfo.responseDataFormat=XML",
                "callInfo.omitXmlSchema",
                "term=manuPartNum:"+keyword+"",
                "storeInfo.id=au.element14.com",
                "callInfo.apiKey=[REDACTED]",
            };

            uriBuilder.Query = String.Join("&", apiParams);

            // Use HttpClient to access our prebuilt URI
            HttpClient hc = new HttpClient();
            hc.BaseAddress = uriBuilder.Uri;
            string v = hc.GetStringAsync("").Result;

            // Load HTTP Response into XML Document Object
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(v);

            // Test number of products in XML Document, return the first item description, otherwise nothing.
            int count = int.Parse(xDoc.GetElementsByTagName("ns1:numberOfResults")[0].InnerText);
            if(count > 0)
            {
                return xDoc.GetElementsByTagName("ns1:displayName")[0].InnerText;
            } else
            {
                return "";
            }         
        }
    }
}
