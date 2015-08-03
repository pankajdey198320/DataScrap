using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ConRetrive
{
    class Program
    {
        const string base_url = "http://www.tripadvisor.in";
        static void Main(string[] args)
        {
            int i = 0;
            var listHotels = new List<Data>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(base_url + "/Restaurants-g294226-Bali.html");///Hotels-g294226-Bali-Hotels.html");
            //var nodes = doc.DocumentNode.Descendants().Where(p =>x p.Name == "div").Where(o => o.Id.Contains("hotel_")).Where(i => i.Attributes.FirstOrDefault(n => n.Value == "listing_title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            var nodes = doc.DocumentNode.Descendants().Where(p => p.Name == "h3" && p.Attributes.FirstOrDefault(n => n.Value == "title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            foreach (var item in nodes)
            {
                var test = item.ChildNodes.Where(p => p.NodeType == HtmlNodeType.Element && p.Name == "a").FirstOrDefault();
                if (test != null)
                {
                    var d = new Data();
                    d.ListUrl = base_url + "/Restaurants-g294226-Bali.html";
                    d.Name = test.InnerText;
                    var url = test.Attributes.FirstOrDefault(p => p.Name == "href");
                    if (url != null)
                    {
                        d.ID = ++i;
                        d.url = url.Value;

                    }
                    listHotels.Add(d);
                    GetReview(d);
                }
            }
            WriteOutput(listHotels);
            // HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//abc//tag");
        }
        static void GetReview(Data d)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(base_url + d.url);

            var nodes = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Name == "address");//.Where(p => p.Name == "div" && p.Attributes.FirstOrDefault(n => n.Value == "listing_title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
            var tab = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Id == "TABS_REVIEWS");
            if (tab != null)
            {
                var counts = tab.ChildNodes.FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "tabs_pers_counts") != null);
                if (counts != null)
                {
                    d.Counts = counts.InnerText.Substring(1);
                }
            }
            //review rating
            var frm = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Value == "histogramCommon simpleHistogram wrap") != null);
            if (frm != null)
            {
                var ratings = frm.Descendants().Where(o => o.Name == "li");//.Where(p => p.Attributes.FirstOrDefault(o => o.Value == "wrap row" && o.Name == "class") != null);
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
                if (add != null)
                {
                    d.Address = add.InnerText;
                }
            }
            var revws = doc.DocumentNode.Descendants().Where(p => p.Id.Contains("review_") && p.Attributes.FirstOrDefault(o => o.Value == "reviewSelector  ") != null);

            foreach (var item in revws)
            {
                var r = new Review();

                var Name = item.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "username mo") != null);
                var quote = item.Descendants().FirstOrDefault(p => p.Name == "span" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "noQuotes") != null);
                var avgRate = item.Descendants().FirstOrDefault(p => p.Name == "img" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "sprite-rating_s_fill rating_s_fill s50") != null);
                var loc = item.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "location") != null);
                var dt = item.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "ratingDate") != null);
                if (dt != null)
                {
                    r.Date = dt.InnerText;
                }
                if (loc != null)
                {
                    var ci = loc.InnerText.Split(',');
                    if (ci.Length > 1)
                    {
                        r.City = ci[0];
                        r.Country = ci[1];
                    }
                }
                if (avgRate != null)
                {
                    var rt = avgRate.Attributes.FirstOrDefault(o => o.Name == "alt");
                    if (rt != null)
                    {
                        r.AvgReview = rt.Value;

                    }
                }
                if (Name != null)
                    r.Name = Name.InnerText.Trim();

                if (quote != null)
                {
                    r.ReviewQuote = quote.InnerText;
                }
                d.Reviewes.Add(r);
            }

        }

        static void WriteOutput(List<Data> data)
        {
            string header = "Destination Country,City,Restaurant list page,Restaurant Name,No of reviewes,Url,Excellent,Very Good,Average,Poor,Terrible,Topic of commants,Name of Reviewer,Review Date,Reviewer location, Reviewer Country";
            var op = (from v in data
                      from c in v.Reviewes
                      select new object[]
                      {
                           v.Country==null?"":v.Country.Trim(),
                          v.City==null?"":v.City.Trim(),
                           v.ListUrl==null?"":v.ListUrl.Trim(),
                           v.Name==null?"":v.Name.Trim(),
                         
                          v.Counts==null?"":v.Counts.Trim(),
                          v.url ==null?"":v.url.Trim(),
                          v.Excellent==null?"":v.Excellent.Trim(),
                          v.VeryGood==null?"":v.VeryGood.Trim(),
                          v.Average==null?"":v.Average.Trim(),
                          v.Poor==null?"":v.Poor.Trim(),
                          v.Terrible==null?"":v.Terrible.Trim(),
                          c.ReviewQuote==null?"":c.ReviewQuote.Trim(),
                         
                          c.Name ==null?"":c.Name.Trim(),
                          c.Date==null?"":c.Date.Trim(),
                          c.City==null?"":c.City.Trim(),
                          c.Country==null?"":c.Country.Trim()

                      }).ToList();
            // Build the file content
            var csv = new StringBuilder();
            csv.AppendLine(header);
            op.ForEach(line =>
            {
                csv.AppendLine(string.Join(",", line));
            });

            File.WriteAllText("c:\\p.csv", csv.ToString());
        }

        static void GetIndividualReviews(Data d)
        {

        }
    }

    class Data
    {
        private string _cnt;
        public Data()
        {
            Reviewes = new List<Review>();
            Country = "Indonesia";
            City = "Bali";

        }
        public string ListUrl { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public string Address { get; set; }
        public string Counts { get {
            return _cnt.Replace(",", "");
        }
            set {
                _cnt = value;
            }
        }
        public string Excellent { get; set; }
        public string VeryGood { get; set; }
        public string Average { get; set; }
        public string Poor { get; set; }
        public string Terrible { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ID { get; set; }
        public List<Review> Reviewes { get; set; }
        public override string ToString()
        {
            return Name + "," + url + "," + Address + "," + Counts + "," + Excellent + "," + VeryGood + "," + Average + "," + Poor + "," + Terrible;
        }
        public string Header
        {
            get
            {
                return "Destination Country,City,Restaurant list page,Restaurant Name,Address,Cuisine Type,No of reviewes,Rating avg,Url,Excellent,Very Good,Average,Poor,Terrible,Topic of commants,Url Of Commant,Rating of reviewer,Review Date,Name of Review, Reviewer location, Reviewer Country";
            }
        }
    }

    class Review
    {
        public int ResId { get; set; }
        public string Name { get; set; }
        public string ReviewQuote { get; set; }
        public string AvgReview { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
    }
}
