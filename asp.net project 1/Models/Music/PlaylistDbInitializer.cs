using HtmlAgilityPack;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace asp.net_project_1.Models.Music {
    public class PlaylistDbInitializer : DropCreateDatabaseAlways<SongContext> {
        public readonly string URL = "https://primalmusicblog.com/tag/dream-pop/";
        public static HtmlDocument htmlDoc = new HtmlDocument();

        protected override void Seed(SongContext context) {
            string nextPage, title, audio;
            int pageNumber = 1;
            HtmlNodeCollection nodes;
            //while (true) {
                nextPage = URL + "page/" + pageNumber++;
                try {
                    PageLoad(nextPage);
                    nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class = 't-inside style-light-bg']");
                    if (nodes != null) {
                        foreach (var node in nodes) {
                            title = GetTitle(node);
                            audio = GetAudio(node);
                            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(audio)) {
                                context.Playlist.Add(new Song() {
                                    Title = title,
                                    Audio = audio
                                });
                                base.Seed(context);
                            }
                        }
                    }
                }
                catch (WebException) {
                    //break;
                }
            //}
        }

        public static void PageLoad(string URL) {
            string urlResponse = URLRequest(URL);
            htmlDoc.LoadHtml(urlResponse);
        }

        private static string URLRequest(string URL) {
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

        public static string GetTitle(HtmlNode node) {
            var a = node.SelectSingleNode(".//h3[@class = 't-entry-title h3']/a");
            string content = a.InnerText;
            if (!string.IsNullOrEmpty(content) && content.Contains("-")) {
                if (content.Contains("|")) {
                    return content.Split(new string[] { "|" }, StringSplitOptions.None)[1].Replace("-", "—").Trim();
                }
                else {
                    return content;
                }
            }
            return null;
        }

        public static string GetAudio(HtmlNode node) {
            var iframe = node.SelectSingleNode(".//iframe");
            if (iframe != null) {
                return iframe.GetAttributeValue("src", "");
            }
            return null;
        }
    }
}