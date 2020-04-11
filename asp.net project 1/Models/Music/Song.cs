using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace asp.net_project_1.Models.Music {
    public class Song {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Audio { get; set; }
    }
}