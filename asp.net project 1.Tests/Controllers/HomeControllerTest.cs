using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using asp.net_project_1;
using asp.net_project_1.Controllers;

namespace asp.net_project_1.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Schedule() {
            HomeController controller = new HomeController();
            ViewResult result = controller.Schedule() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Music() {
            HomeController controller = new HomeController();
            ViewResult result = controller.Music() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
