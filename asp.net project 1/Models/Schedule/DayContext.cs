using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp.net_project_1.Models.Schedule {
    public class DayContext : DbContext {
        public DbSet<Day> Days { get; set; }
    }
}