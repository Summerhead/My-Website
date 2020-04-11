using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp.net_project_1.Models.Music;
using asp.net_project_1.Models.Schedule;
using System.Data.Entity;

namespace asp.net_project_1.Controllers {
    public class HomeController : Controller {
        SongContext songContext = new SongContext();
        DayContext dayContext = new DayContext();

        public ActionResult Index() {
            return View();
        }

        public ActionResult Schedule() {
            var schedule = dayContext.Days.Include(d => d.Pairs).ToList();
            return View(schedule);
        }

        public ActionResult Music() {
            IEnumerable<Song> playlist = songContext.Playlist;
            ViewBag.Playlist = playlist;
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}