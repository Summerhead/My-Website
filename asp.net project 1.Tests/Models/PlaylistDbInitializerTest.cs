using asp.net_project_1.Models.Music;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace asp.net_project_1.Tests.Models {
    [TestClass]
    public class PlaylistDbInitializerTest {
        public readonly string URL = "https://primalmusicblog.com/tag/dream-pop/";
        public static HtmlDocument htmlDoc = PlaylistDbInitializer.htmlDoc;

        [TestMethod]
        public void PageLoadException() {
            try {
                PlaylistDbInitializer.PageLoad(URL + "/page/9");
                Assert.Fail();
            }
            catch (WebException) { }
        }

        [TestMethod]
        public void TitleNotNull() {
            PlaylistDbInitializer.PageLoad(URL);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class = 't-inside style-light-bg']");
            var titles = "";
            foreach (var node in nodes) {
                titles += PlaylistDbInitializer.GetTitle(node);
            }
            Assert.IsNotNull(titles);
        }

        [TestMethod]
        public void AudioNotNull() {
            PlaylistDbInitializer.PageLoad(URL);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class = 't-inside style-light-bg']");
            var audios = "";
            foreach (var node in nodes) {
                audios += PlaylistDbInitializer.GetAudio(node);
            }
            Assert.IsNotNull(audios);
        }
    }
}
