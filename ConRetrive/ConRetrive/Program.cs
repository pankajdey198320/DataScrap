using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConRetrive
{
    class Program
    {
        const string base_url = "http://www.tripadvisor.in";
        static void Main(string[] args)
        {
            var listHotels = new List<Data>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(base_url + "/Restaurants-g294226-Bali.html");///Hotels-g294226-Bali-Hotels.html");
            //var nodes = doc.DocumentNode.Descendants().Where(p =>x p.Name == "div").Where(o => o.Id.Contains("hotel_")).Where(i => i.Attributes.FirstOrDefault(n => n.Value == "listing_title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            var nodes = doc.DocumentNode.Descendants().Where(p => p.Name == "h3" && p.Attributes.FirstOrDefault(n => n.Value == "title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            foreach(var item in nodes)
            {
                var test = item.ChildNodes.Where(p => p.NodeType == HtmlNodeType.Element && p.Name == "a").FirstOrDefault();
                if(test != null)
                {
                    var d = new Data();
                    d.Name = test.InnerText;
                    var url = test.Attributes.FirstOrDefault(p => p.Name == "href");
                    if(url!= null)
                    {
                        d.url = url.Value;
                        
                    }
                    listHotels.Add(d);
                    GetReview(d);
                }
            }
           // HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//abc//tag");
        }
        static void GetReview(Data d)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(base_url + d.url);

            var nodes = doc.DocumentNode.Descendants().FirstOrDefault(p=>p.Name == "address");//.Where(p => p.Name == "div" && p.Attributes.FirstOrDefault(n => n.Value == "listing_title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            var tab = doc.DocumentNode.Descendants().FirstOrDefault(p=>p.Id == "TABS_REVIEWS");
            if(tab != null){
                var counts = tab.ChildNodes.FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "tabs_pers_counts") != null);
                if (counts != null)
                {
                    d.Counts = counts.InnerText.Substring(1);
                }
            }
            //review rating
            var frm = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Value == "histogramCommon simpleHistogram wrap")!= null);
            if (frm != null)
            {
                var ratings = frm.Descendants().Where(o=>o.Name == "li");//.Where(p => p.Attributes.FirstOrDefault(o => o.Value == "wrap row" && o.Name == "class") != null);
                foreach (var rating in ratings)
                {
                    var FirstChild = rating.ChildNodes.FirstOrDefault(p => p.NodeType == HtmlNodeType.Element && p.Name == "div" && p.Attributes.FirstOrDefault(s => s.Value == "label fl part clickable") != null);
                    var LastChild = rating.ChildNodes.FirstOrDefault(p => p.NodeType == HtmlNodeType.Element && p.Name == "div" && p.Attributes.FirstOrDefault(s => s.Value == "valueCount fr part") != null);
                    if (FirstChild != null && LastChild != null)
                    {
                        if (FirstChild.InnerText.Trim() == "Excellent")
                        {
                            d.Excellent = LastChild.InnerText;
                        }
                        else if (FirstChild.InnerText.Trim() == "VeryGood")
                        {
                            d.VeryGood = LastChild.InnerText;
                        }
                        else if (FirstChild.InnerText.Trim() == "Average")
                        {
                            d.Average = LastChild.InnerText;
                        }
                        else if (FirstChild.InnerText.Trim() == "Poor")
                        {
                            d.Poor = LastChild.InnerText;
                        }
                        else if (FirstChild.InnerText.Trim() == "Terrible")
                        {
                            d.Terrible = LastChild.InnerText;
                        }
                    }
                }
            }
            if (nodes != null)
            {
                var add = nodes.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "property" && o.Value == "address") != null);
                if(add != null){
                    d.Address = add.InnerText;
                }
            }
        }
    }

    class Data {
        public string Name { get; set; }
        public string url {get; set; }
        public string Address { get; set; }
        public string Counts { get; set; }
        public string Excellent { get; set; }
        public string VeryGood { get; set; }
        public string Average { get; set; }
        public string Poor { get; set; }
        public string Terrible { get; set; }
    
    }
}
