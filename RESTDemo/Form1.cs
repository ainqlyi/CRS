using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace RESTDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string requestUri =
                "http://tasks.arcgisonline.com/ArcGIS/rest/services/Locators/TA_Address_NA/GeocodeServer/findAddressCandidates";
            
            StringBuilder data = new StringBuilder();
            data.AppendFormat("?f={0}", "json");

            // HttpUtility in System.Web.dll
            data.AppendFormat("&Address={0}&Zip={1}", System.Web.HttpUtility.UrlEncode(StreetTextBox.Text), 
                System.Web.HttpUtility.UrlEncode(ZoneTextBox.Text));
            
            HttpWebRequest request = WebRequest.Create(requestUri + data) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string responseString = reader.ReadToEnd();

                // JavaScriptSerializer in System.Web.Extensions.dll
                System.Web.Script.Serialization.JavaScriptSerializer jss =
                    new System.Web.Script.Serialization.JavaScriptSerializer();

                IDictionary<string, object> results = 
                    jss.DeserializeObject(responseString) as IDictionary<string, object>;

                if (results != null && results.ContainsKey("candidates"))
                {
                    IEnumerable<object> candidates = results["candidates"] as IEnumerable<object>;
                    foreach (IDictionary<string, object> candidate in candidates)
                    {
                        string address = candidate["address"] as string;
                        IDictionary<string, object> location = candidate["location"] as IDictionary<string, object>;

                        double x = decimal.ToDouble((decimal)location["x"]);
                        double y = decimal.ToDouble((decimal)location["y"]);
                        linkLabel1.Enabled = true;
                        linkLabel1.Text = x + "," + y;
                        
                        // Push pin 
                        string linkUrl = string.Format("http://www.bing.com/maps/default.aspx?cp={0}~{1}&lvl=15&style=h&sp=Point.{0}_{1}_My Location", y, x);
                        linkLabel1.Links.Add(0, linkLabel1.Text.Length, linkUrl);
                        
                        break;
                    }
                }            
            } 
        }

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}

// ArcGIS Online geocode service for Europe: http://www.arcgisonline.com/home/item.html?id=dff63d09c79a44b8b3c63f2d90222e0e
// Bing Maps online API: http://help.live.com/Help.aspx?market=en-US&project=WL_Local&querytype=topic&query=WL_LOCAL_PROC_BuildURL.htm