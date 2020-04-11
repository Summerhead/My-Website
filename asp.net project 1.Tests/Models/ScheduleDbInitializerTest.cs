using asp.net_project_1;
using asp.net_project_1.Models.Schedule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace asp.net_project_1.Tests.Models {
    [TestClass]
    public class ScheduleDbInitializerTest {
        public readonly string URL = "https://ruz.narfu.ru/";
        public static HtmlDocument htmlDoc = ScheduleDbInitializer.htmlDoc;

        [TestMethod]
        public void URLResponseNotNull() {
            var res = ScheduleDbInitializer.URLRequest(URL);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void URLResponseException() {
            try {
                var res = ScheduleDbInitializer.URLRequest("qwerty");
                Assert.Fail();
            }
            catch (UriFormatException) { }
        }

        [TestMethod]
        public void GetGroupNotNull() {
            ScheduleDbInitializer.PageLoad(URL + "?timetable&group=11892");
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'list') and contains(@class, 'col-md-2')]");
            List<Pair> pairs = new List<Pair>();
            foreach (var node in nodes) {
                pairs.AddRange(ScheduleDbInitializer.GetPairs(node));
            }
            Assert.IsNotNull(pairs);
        }
    }
}
