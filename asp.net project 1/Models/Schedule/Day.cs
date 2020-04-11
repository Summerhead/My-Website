using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp.net_project_1.Models.Schedule {
    public class Day {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public List<Pair> Pairs { get; set; }
    }

    public class Pair {
        public int ID { get; set; }
        public int Num { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Classroom { get; set; }
    }
}