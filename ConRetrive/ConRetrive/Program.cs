using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
namespace ConRetrive
{
    class Program
    {
        const string base_url = "http://www.tripadvisor.in";
        const string base_path = "E:\\Trip\\Bali\\";
        static System.Collections.Concurrent.ConcurrentBag<Data> listHotels = new System.Collections.Concurrent.ConcurrentBag<Data>();
        static void Main(string[] args)
        {
            DoWork();
            //);
            WriteOutput(listHotels.ToList());
            // HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//abc//tag");
            Console.ReadKey();
        }

        static async void DoWork()
        {

            int i = 0;

            /* BAliurl
            var extUrl = "/RestaurantSearch?ajax=0&geo=294226&Action=PAGE&o=a{0}&etags=9910%2C9911%2C9909%2C9901%2C9899%2C9900";
             * */
            var extUrl = "/RestaurantSearch?ajax=0&geo=294265&Action=PAGE&o=a{0}&etags=9909%2C9899%2C9901%2C9900%2C9910%2C9911";
            var nods = Enumerable.Range(1, 7110).Where(p => p % 30 == 0);
            //var nods = Enumerable.Range(1, 40).Where(p => p % 30 == 0);
            //Parallel.ForEach(nods, o =>
            // {


            //});3480
            for (var o = 0; o <= 1; o++)
            {
                try
                {

                    HtmlDocument doc = null;
                    //for (var ic = 0; ic < 10 && doc == null; ic++)
                    {
                        // HtmlWeb web = new HtmlWeb();
                        // web.PreRequest += new HtmlWeb.PreRequestHandler(onPrereq);
                        try
                        {
                            var d1 = await LoadPage(base_url + string.Format(extUrl, o));
                            
                            // var dResult = GetDocAsync(base_url + string.Format(extUrl, o), (d1) =>
                            {
                                var dc = d1 as HtmlDocument;
                                if (dc != null)
                                {
                                    var nodes = dc.DocumentNode.Descendants().Where(p => p.Name == "h3" && p.Attributes.FirstOrDefault(n => n.Value == "title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));
                                    //  Parallel.ForEach(nodes, item =>
                                    foreach (var item in nodes)
                                    {
                                        var test = item.ChildNodes.Where(p => p.NodeType == HtmlNodeType.Element && p.Name == "a").FirstOrDefault();
                                        if (test != null)
                                        {
                                            var d = new Data();

                                            d.Name = test.InnerText;

                                            var url = test.Attributes.FirstOrDefault(p => p.Name == "href");
                                            if (url != null)
                                            {
                                                d.ID = ++i;
                                                d.url = url.Value;

                                            }
                                            d.ListUrl = base_url + d.url;// "/Restaurants-g294226-Bali.html";
                                            listHotels.Add(d);
                                            LoadReview(d);
                                            WriteOutput(d);
                                            Console.WriteLine(d.ToString());
                                        }
                                    }
                                    //);
                                }
                            }
                            //);

                        }
                        catch (Exception e)
                        {
                            // Console.WriteLine("error to load " + base_url + string.Format(extUrl, o) + " " + ic);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    //var nodes = doc.DocumentNode.Descendants().Where(p =>x p.Name == "div").Where(o => o.Id.Contains("hotel_")).Where(i => i.Attributes.FirstOrDefault(n => n.Value == "listing_title") != null);//.Where(p => p.Attributes.Contains("class='listing_info popIndexValidation'"));

                }
                catch { }
                // j += 30;
            }

        }

        static HtmlDocument GetDoc(string url)
        {
            HtmlDocument doc = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000 * 60 * 5;
            request.ReadWriteTimeout = 1000 * 60 * 5;
            using (var wresp = (HttpWebResponse)request.GetResponse())
            {
                if (wresp != null)// (!string.IsNullOrWhiteSpace(str))
                {
                    doc = new HtmlDocument();
                    doc.Load(wresp.GetResponseStream());
                }
            }
            return doc;
        }

        public static async Task<HtmlDocument> LoadPage(string address)
        {
            using (var httpResponse = await new HttpClient().GetAsync(address)
                .ConfigureAwait(continueOnCapturedContext: false)) //IO-bound
            using (var responseContent = httpResponse.Content)
            using (var contentStream = await responseContent.ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false)) //IO-bound
                return LoadHtmlDocument(contentStream); //CPU-bound
        }

        public static HtmlDocument LoadHtmlDocument(Stream stream)
        {
            if (stream != null)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(stream);
                return doc;
            }
            return null;

        }
        static IAsyncResult GetDocAsync(string url, AsyncCallback callBack)
        {
            HtmlDocument doc = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000 * 60 * 5;
            request.ReadWriteTimeout = 1000 * 60 * 5;
            if (callBack != null)
            {
                return request.BeginGetResponse((result) =>
                 {

                     if (result != null)// (!string.IsNullOrWhiteSpace(str))
                     {
                         var res = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
                         if (res != null)
                         {
                             doc = new HtmlDocument();
                             doc.Load(res.GetResponseStream());
                             DocResult rslt = new DocResult();
                             rslt.SetDoc(doc);
                             callBack.Invoke(rslt);
                         }
                     }
                 }, request);


            }
            return null;

        }
        static bool onPrereq(System.Net.HttpWebRequest req)
        {
            req.Timeout = 500000;
            return true;
        }
        static async void LoadReview(Data d)
        {

            try
            {

                HtmlDocument doc = null;
                for (var x = 0; x < 10 && doc == null; x++)
                {
                    try
                    {
                        doc = GetDoc(base_url + d.url);
                    }
                    catch
                    {
                        Console.WriteLine("error to load " + base_url + d.url + " " + x);
                        System.Threading.Thread.Sleep(10000);
                    }
                }
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
                              //  Parallel.ForEach(cn, p =>
                               for (var p = 0; p < count; p++)
                               {
                                   var url = d.url.Replace("Reviews-", "Reviews-or" + p * 10 + "-");
                                   try
                                   {
                                       //HtmlDocument dc = null;
                                       //for (var inl = 0; inl < 10 && dc == null; inl++)
                                       //{
                                       //    // HtmlWeb web = new HtmlWeb();
                                       //    //web.PreRequest += new HtmlWeb.PreRequestHandler(onPrereq);
                                       //    try
                                       //    {
                                       //        dc = GetDoc(base_url + url);
                                       //    }
                                       //    catch
                                       //    {
                                       //        Console.WriteLine("error to load " + base_url + d.url + " " + inl);
                                       //        System.Threading.Thread.Sleep(10000);
                                       //    }
                                       //}
                                       var dc = await LoadPage(base_url + url);
                                       if (dc != null)
                                           GetReview(d, dc, base_url + url);
                                   }
                                   catch (Exception e)
                                   {
                                       Console.WriteLine(e.Message);
                                   }
                               }
                               //);
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
        static async void GetReview(Data d, HtmlDocument doc, string url = "")
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
                var quoteContainer = item.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value.Contains("quote")) != null);
                if (quoteContainer != null)
                {
                    var urlTag = quoteContainer.Descendants().FirstOrDefault(p => p.Name == "a");
                    if (urlTag != null)
                    {
                        var urlAttr = urlTag.Attributes.FirstOrDefault(p => p.Name == "href");
                        if (urlAttr != null)
                        {
                            r.Url = base_url + urlAttr.Value;
                        }

                    }

                }
                var Name = item.Descendants().FirstOrDefault(p => p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "username mo") != null);
                var quote = item.Descendants().FirstOrDefault(p => p.Name == "span" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "noQuotes") != null);
                var avgRate = item.Descendants().FirstOrDefault(p => p.Name == "img" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "sprite-rating_s_fill rating_s_fill s50") != null);
                var loc = item.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "location") != null);
                var dt = item.Descendants().FirstOrDefault(p => p.Name == "span" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "ratingDate") != null);

                var ProfileDive = item.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "memberOverlayLink") != null);

                if (ProfileDive != null)
                {
                    var temp = ProfileDive.Id.Split('-');
                    if (temp.Length >= 2)
                    {
                        string uid = string.Empty, src = string.Empty;
                        var arrUid = temp[0].Split('_');
                        if (arrUid.Length >= 2)
                        {
                            uid = arrUid[1];
                        }
                        var arrSrc = temp[1].Split('_');
                        if (arrSrc.Length >= 2)
                        {
                            src = arrSrc[1];
                        }
                        var s = "/MemberOverlay?uid={0}&c=&src={1}&fus=false&partner=false&LsoId=";
                        if (!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(uid))
                        {
                            //HtmlDocument dc = null;
                            //// HtmlWeb web = new HtmlWeb();
                            /////  web.PreRequest += new HtmlWeb.PreRequestHandler(onPrereq);
                            //for (var inl = 0; inl < 10 && dc == null; inl++)
                            //{
                            //    try
                            //    {
                            //        dc = GetDoc(base_url + string.Format(s, uid, src));
                            //    }
                            //    catch
                            //    {
                            //        System.Threading.Thread.Sleep(10000);
                            //    }
                            //}
                            var dc = await LoadPage(base_url + string.Format(s, uid, src));
                            
                            if (dc != null)
                            {

                                var profLvlDiv = dc.DocumentNode.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "badgeinfo") != null);
                                if (profLvlDiv != null)
                                {
                                    var profLvlSpan = profLvlDiv.Descendants().FirstOrDefault(p => p.Name == "span");
                                    if (profLvlSpan != null)
                                    {
                                        r.ReviererLevel = profLvlSpan.InnerText;
                                    }
                                }
                                var profUrlDiv = dc.DocumentNode.Descendants().FirstOrDefault(p => p.Name == "div" && p.Attributes.FirstOrDefault(o => o.Name == "class" && o.Value == "baseNav") != null);
                                if (profUrlDiv != null)
                                {
                                    var profLvlan = profUrlDiv.Descendants().LastOrDefault(p => p.Name == "a");
                                    if (profLvlan != null)
                                    {
                                        var href = profLvlan.Attributes.FirstOrDefault(x => x.Name == "href");
                                        if (href != null)
                                        {
                                            r.ReviewerProfileUrl = base_url + "/" + href.Value;
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
                if (dt != null)
                {
                    r.Date = dt.InnerText.Replace("Reviewed", "").Replace("NEW", "").Trim();
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
            string header = "Destination Country,City,Restaurant list page,Restaurant Name,Address,cuisine,No of reviewes,Url,Excellent,Very Good,Average,Poor,Terrible,Food,Value,Services,Atmosphere,Topic of commants,Name of Reviewer,Review Date,Reviewer location, Reviewer Country,level,profile";
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
                           c.Url.ToFormatString(),
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
                           c.Country.ToFormatString(),
                           c.ReviererLevel.ToFormatString(),
                           c.ReviewerProfileUrl.ToFormatString()

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

        static void WriteOutput(Data v)
        {
            // string header = "Destination Country,City,Restaurant list page,Restaurant Name,Address,cuisine,No of reviewes,Url,Excellent,Very Good,Average,Poor,Terrible,Food,Value,Services,Atmosphere,Topic of commants,Name of Reviewer,Review Date,Reviewer location, Reviewer Country,level,profile";
            var op = (
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
                           c.Url.ToFormatString(),
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
                           c.Country.ToFormatString(),
                           c.ReviererLevel.ToFormatString(),
                           c.ReviewerProfileUrl.ToFormatString()

                      }).ToList();
            // Build the file content
            var csv = new StringBuilder();
            // csv.AppendLine(header);
            op.ForEach(line =>
            {
                csv.AppendLine(string.Join(",", line));
            });
            try
            {
                File.AppendAllText("c:\\build80_9_8_2015.csv", csv.ToString());
            }
            catch
            {
                Console.WriteLine("write error");
            }
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
        public string Url { get; set; }
        public string ReviererLevel { get; set; }
        public string ReviewerProfileUrl { get; set; }
    }

    public static class StringExtension
    {
        public static string ToFormatString(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                return "NA";
            }
            else
            {
                return obj.Replace(",", " ").Trim();
            }
        }
    }
    class DocResult : IAsyncResult
    {
        HtmlDocument _doc;
        bool _isCompleted;

        public void SetDoc(HtmlDocument doc)
        {
            _doc = doc;
        }

        public object AsyncState
        {
            get { return _doc; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
        }
    }
}
