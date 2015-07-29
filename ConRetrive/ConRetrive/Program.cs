using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConRetrive
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.tripadvisor.in/Hotels-g294226-Bali-Hotels.html");
            HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//abc//tag");
        }
    }
}
