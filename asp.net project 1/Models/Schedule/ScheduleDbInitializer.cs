using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace asp.net_project_1.Models.Schedule {
    public class ScheduleDbInitializer : DropCreateDatabaseAlways<DayContext> {
        public readonly string URL = "https://ruz.narfu.ru/";
        public static HtmlDocument htmlDoc = new HtmlDocument();

        protected override void Seed(DayContext context) {
            string nameAndDate, name = "", date = "";
            List<Pair> pairs;
            HtmlNodeCollection nodes;
            try {
                PageLoad(URL + "?groups&institution=3");
                PageLoad(URL + GetGroup());
                nodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'list') and contains(@class, 'col-md-2')]");
                if (nodes != null) {
                    foreach (var node in nodes) {
                        nameAndDate = GetNameAndDate(node);
                        if (nameAndDate.Contains(",")) {
                            name = nameAndDate.Split(',')[0].Trim();
                            name = name.First().ToString().ToUpper() + name.Substring(1);
                            date = nameAndDate.Split(',')[1].Trim();
                        }
                        pairs = GetPairs(node);
                        context.Days.Add(new Day() {
                            Name = name,
                            Date = date,
                            Pairs = pairs
                        });
                        base.Seed(context);
                    }
                }
            }
            catch (WebException) { }
        }

        public static void PageLoad(string URL) {
            string urlResponse = URLRequest(URL);
            htmlDoc.LoadHtml(urlResponse);
        }

        public static string URLRequest(string URL) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "GET";
            request.Timeout = 60000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36";

            string responseContent = null;
            using (WebResponse response = request.GetResponse()) {
                using (Stream stream = response.GetResponseStream()) {
                    using (StreamReader streamreader = new StreamReader(stream)) {
                        responseContent = streamreader.ReadToEnd();
                    }
                }
            }
            return responseContent;
        }

        public static string GetGroup() {
            var nodes = htmlDoc.DocumentNode.SelectNodes("//a[@class = 'visible-xs']");
            foreach (var node in nodes) {
                if (node.SelectSingleNode(".//span").InnerText.Trim().Equals("351718")) {
                    return node.GetAttributeValue("href", "");
                }
            }
            return null;
        }

        public static string GetNameAndDate(HtmlNode node) {
            var div = node.SelectSingleNode(".//div[contains(@class, 'dayofweek')]");
            string content = div.InnerText;
            if (!string.IsNullOrEmpty(content)) {
                return content;
            }
            return null;
        }

        public static List<Pair> GetPairs(HtmlNode node) {
            var divs = node.SelectNodes(".//div[contains(@class, 'timetable_sheet') and contains(@class, 'hidden-xs')]");
            List<Pair> pairs = new List<Pair>();
            int num = 0;
            string name = "", time = "", classroom = "";
            if (divs != null) {
                foreach (var div in divs) {
                    var spanNum = div.SelectSingleNode(".//span[@class = 'num_para']");
                    var spanKindOfWork = div.SelectSingleNode(".//span[@class = 'kindOfWork']");
                    var spanDisipline = div.SelectSingleNode(".//span[@class = 'discipline']");
                    var spanTime = div.SelectSingleNode(".//span[@class = 'time_para']");
                    var spanClassroom = div.SelectSingleNode(".//span[@class = 'auditorium']");
                    if (spanNum != null && spanDisipline != null && spanTime != null && spanClassroom != null && spanKindOfWork != null) {
                        if (!int.TryParse(spanNum.InnerText.Trim(), out num)) {
                            num = 0;
                        }
                        name = spanDisipline.InnerText.Split('(')[0].Trim() + $" ({spanKindOfWork.InnerText.Trim()})";
                        time = spanTime.InnerText.Replace("&ndash;", " — ").Trim();
                        classroom = spanClassroom.InnerText.Replace("&nbsp;", " ").Trim();
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(classroom)) {
                            pairs.Add(new Pair() {
                                Num = num,
                                Name = name,
                                Time = time,
                                Classroom = classroom
                            });
                        }
                    }
                }
            }
            else {
                pairs.Add(new Pair() {
                    Num = num,
                    Name = name,
                    Time = time,
                    Classroom = classroom
                });
            }
            return pairs;
        }
    }
}