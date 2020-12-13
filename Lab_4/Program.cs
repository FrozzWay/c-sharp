using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using CsvHelper;
// Install-Package CsvHelper -Version 18.0.0

namespace Lab_4
{
    class Program
    {
        const string fileName = @"c:\Temp\file.csv";
        public class WebScanner : IDisposable
        {
            private readonly HashSet<Uri> _procLinks = new HashSet<Uri>();
            private readonly WebClient _webClient = new WebClient();

            private readonly HashSet<string> _ignoreFiles = new HashSet<string> { ".ico", ".xml" };

            public event Action<Link, Link[]> TargetFound;
            private void OnTargetFound(Link page, Link[] links)
            {
                TargetFound?.Invoke(page, links);
            }

            public void Process(string domain, Uri page, int count, int limit, int level) {
                if (count <= 0) return;

                if (_procLinks.Contains(page)) return;
                _procLinks.Add(page);

                string html = _webClient.DownloadString(page);

                var strings = (from cutted_string in Regex.Matches(html, @"<a .*?<\/a>").Cast<Match>()
                               let url = Regex.Match(cutted_string.Value, @"href=""[\/\w-\.:]+""").Value.Replace("href=", "").Trim('"')
                               let name = Regex.Match(cutted_string.Value, @">.+?<").Value.Trim('>').Trim('<')
                               select new
                               {
                                   url = url,
                                   name = name,
                               }
                               ).ToList();

                var hrefs = (from href in strings where href.url != ""
                             let loc = href.url.StartsWith("/")
                             select new
                             {
                                 Ref = new Uri(loc ? $"{domain}{href.url}" : href.url),
                                 name = href.name,
                                 IsLocal = loc || href.url.StartsWith(domain)

                             }).ToList();

                var externals = (from href in hrefs
                                 where !href.IsLocal
                                 select new Link(href.Ref, href.name, level + 1)
                                 ).ToArray();

                if (limit < externals.Length) Array.Resize<Link>(ref externals, limit);

                if (externals.Length > 0) OnTargetFound(new Link(page, "Default Page", level), externals);

                var locals = (from href in hrefs
                              where href.IsLocal
                              select new Link(href.Ref, href.name, level + 1)).ToArray();

                foreach(var link in locals)
                {
                    string fileEx = System.IO.Path.GetExtension(link.url.LocalPath).ToLower();
                    if (_ignoreFiles.Contains(fileEx)) continue;

                    Process(domain, link.url, --count, limit, ++level);
                }
            }


            public void Scan(Uri startPage, int pageCount, int limit, int level)
            {
                _procLinks.Clear();

                string domain = $"{startPage.Scheme}://{startPage.Host}";
                Process(domain, startPage, pageCount, limit, level);
            }

            public void Dispose()
            {
                _webClient.Dispose();
            }
        }

        public class Link
        {
            public Uri url;
            public string name;
            public int level;

            public Link(Uri _url, string _name, int _level)
            {
                url = _url;
                name = _name;
                level = _level;
            }
        }
        public class Link2
        {
            public string url { get; set; }
            public string name { get; set; }
            public int level { get; set; }

            public Link2(string _url, string _name, int _level)
            {
                url = _url;
                name = _name;
                level = _level;
            }
        }

        static void Main(string[] args)
        {
            using (WebScanner scanner = new WebScanner())
            {
                List<Link2> list = new List<Link2>();
                scanner.TargetFound += (page, links) =>
                {
                    Console.WriteLine($"\nL: {page.level} Page:\n\t{page.url}\nLinks:");
                    foreach (var link in links)
                        Console.WriteLine($"\tL: {link.level} Url: {link.url}\n\tName: {link.name}");
                };

                scanner.TargetFound += (page, links) =>
                {
                    list.Add(new Link2(page.url.ToString(), page.name, page.level));
                    foreach (var link in links)
                    {
                        list.Add(new Link2(link.url.ToString(), link.name, link.level));
                    }
                };

                Console.WriteLine("Url:"); string Url = Console.ReadLine();
                Console.WriteLine("Count of pages to analize:"); int count = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Limit of pages to show"); int limit = Convert.ToInt32(Console.ReadLine());
                scanner.Scan(new Uri(Url), count, limit, 0);
                using (var stream = new StreamWriter(fileName))
                using (var csvReader = new CsvWriter(stream, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.WriteRecords(list);
                    stream.Flush();
                }
            }


        }
    }
}
