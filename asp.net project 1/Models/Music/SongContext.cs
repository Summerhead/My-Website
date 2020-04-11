using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp.net_project_1.Models.Music {
    public class SongContext : DbContext {
        public DbSet<Song> Playlist { get; set; }
    }
}