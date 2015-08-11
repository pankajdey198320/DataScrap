using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace ConRetrive
{
    class Program
    {
        const string base_url = "http://www.tripadvisor.in";
        static void Main(string[] args)
        {
            int i = 0;
            var listHotels = new System.Collections.Concurrent.ConcurrentBag<Data>();

            var extUrl = "/RestaurantSearch?ajax=0&geo=294226&Action=PAGE&o=a{0}&etags=9910%2C9911%2C9909%2C9901%2C9899%2C9900";
            var nods = Enumerable.Range(1, 3480).Where(p => p % 30 == 0);
            Parallel.ForEach(nods, o =>
            // {


            //});
            //for (var j = 0; j <= 3480; )
            {
                try
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = web.Load(base_url + string.Format(extUrl, o));//"/Restaurants-g294226-Bali.html");///Hotels-g294226-Bali-Hotels.html");
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
                            LoadReview(d);
                            Console.WriteLine(d.ToString());
                        }
                    }
                }
                catch { }
                // j += 30;
            });
            WriteOutput(listHotels.ToList());
            // HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//abc//tag");
        }
        static void LoadReview(Data d)
        {
            HtmlWeb web = new HtmlWeb();
            try
            {
                HtmlDocument doc = web.Load(base_url + d.url);
                if (doc != null)
                {
                    var pageDiv = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "pageNumbers") != null);
                    if (pageDiv != null)
                    {
                        var rvPagecount = pageDiv.ChildNodes.Where(o => o.NodeType == HtmlNodeType.Element && o.Name == "a").LastOrDefault();
                        GetReview(d, doc);
                        if (rvPagecount != null)
                        {
                            int count = 0;

                            int.TryParse(rvPagecount.InnerText, out count);
                            if (count > 1)
                            {
                                var cn = Enumerable.Range(1, count);
                                Parallel.ForEach(cn, p =>
                                {
                                    var url = d.url.Replace("Reviews-", "Reviews-or" + p * 10 + "-");
                                    try
                                    {
                                        HtmlDocument dc = web.Load(base_url + url);
                                        GetReview(d, dc);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                });
                            }
                            //for (int i = 1; i < count; i++)
                            //{
                            //    var url = d.url.Replace("Reviews-", "Reviews-or" + i * 10 + "-");
                            //    HtmlDocument dc = web.Load(base_url + url);
                            //    GetReview(d, dc);
                            //}

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void GetReview(Data d, HtmlDocument doc)
        {
            var cuisine = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "cuisine") != null);

            if (cuisine != null)
            {
                d.cuisine = cuisine.InnerText.Trim();
            }

            var address = doc.DocumentNode.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "format_address") != null);
            if (address != null)
            {
                d.Address = address.InnerText.Trim();
            }
            var lblratings = doc.DocumentNode.Descendants().Where(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "ratingRow wrap") != null);
            foreach (var item in lblratings)
            {
                var text = item.Descendants().FirstOrDefault(p => p.Name == "span" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "text") != null);
                var value = item.Descendants().FirstOrDefault(p => p.Name == "img" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value.Contains("sprite-rating_s_fill rating_s_fill")) != null);
                if (text != null && value != null)
                {
                    if (text.InnerText.Trim() == "Food")
                    {
                        var valx = value.Attributes.FirstOrDefault(p => p.Name == "alt");
                        if (valx != null)
                        {
                            d.Food = valx.Value;
                        }
                    }
                    else if (text.InnerText.Trim() == "Value")
                    {
                        var valx = value.Attributes.FirstOrDefault(p => p.Name == "alt");
                        if (valx != null)
                        {
                            d.Value = valx.Value;
                        }
                    }
                    else if (text.InnerText.Trim() == "Service")
                    {
                        var valx = value.Attributes.FirstOrDefault(p => p.Name == "alt");
                        if (valx != null)
                        {
                            d.Service = valx.Value;
                        }
                    }
                    else if (text.InnerText.Trim() == "Atmosphere")
                    {
                        var valx = value.Attributes.FirstOrDefault(p => p.Name == "alt");
                        if (valx != null)
                        {
                            d.Atmosphere = valx.Value;
                        }
                    }
                }
            }
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
            //if (nodes != null)
            //{
            //    var add = nodes.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "property" && o.Value == "address") != null);
            //    if (add != null)
            //    {
            //        d.Address = add.InnerText;
            //    }
            //}
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
            string header = "Destination Country,City,Restaurant list page,Restaurant Name,Address,cuisine,No of reviewes,Url,Excellent,Very Good,Average,Poor,Terrible,Food,Value,Services,Atmosphere,Topic of commants,Name of Reviewer,Review Date,Reviewer location, Reviewer Country";
            var op = (from v in data
                      from c in v.Reviewes
                      select new object[]
                      {
                            v.Country.ToFormatString(),
                           v.City.ToFormatString(),
                            v.ListUrl.ToFormatString(),
                            v.Name.ToFormatString(),
                          v.Address.ToFormatString(),
                          v.cuisine.ToFormatString() ,
                           v.Counts.ToFormatString(),
                           v.url.ToFormatString(),
                           v.Excellent.ToFormatString(),
                           v.VeryGood.ToFormatString(),
                           v.Average.ToFormatString(),
                           v.Poor.ToFormatString(),
                           v.Terrible.ToFormatString(),
                           v.Food.ToFormatString(),
                          v.Value.ToFormatString(),
                          v.Service.ToFormatString(),
                           v.Atmosphere.ToFormatString(),
                           c.ReviewQuote.ToFormatString(),
                         
                           c.Name.ToFormatString() ,
                           c.Date.ToFormatString(),
                           c.City.ToFormatString(),
                           c.Country.ToFormatString()

                      }).ToList();
            // Build the file content
            var csv = new StringBuilder();
            csv.AppendLine(header);
            op.ForEach(line =>
            {
                csv.AppendLine(string.Join(",", line));
            });

            File.WriteAllText("c:\\build76_9_8_2015.csv", csv.ToString());
        }

        static void GetIndividualReviews(Data d)
        {

        }
    }

    class Data
    {
        private string _cnt;
        private string _csn;
        private string _addr;
        public Data()
        {
            _cnt = "";
            Reviewes = new System.Collections.Concurrent.ConcurrentBag<Review>();
            Country = "Indonesia";
            City = "Bali";

        }
        public string ListUrl { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public string Address
        {
            get
            {
                if (_addr == null)
                    return "";
                return _addr.Replace(",", "");
            }
            set
            {
                _addr = value;
            }
        }
        public string cuisine
        {
            get
            {
                if (_csn == null)
                    return "";
                return _csn.Replace(",", "");
            }
            set
            {
                _csn = value;
            }
        }
        public string Counts
        {
            get
            {
                if (_cnt == null)
                    return "";
                return _cnt.Replace(",", "");
            }
            set
            {
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
        public System.Collections.Concurrent.ConcurrentBag<Review> Reviewes { get; set; }
        public string Food { get; set; }
        public string Value { get; set; }
        public string Service { get; set; }
        public string Atmosphere { get; set; }
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

    public static class StringExtension
    {
        public static string ToFormatString(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) {
                return "NA";
            }
            else
            {
               return obj.Replace(",", " ").Trim();
            }
        }
    }
}
