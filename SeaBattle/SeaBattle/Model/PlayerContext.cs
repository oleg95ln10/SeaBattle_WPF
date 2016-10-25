using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    class PlayerContext : DbContext
    {
        public PlayerContext()
            :base("DbConnection")
        { }

        public DbSet<DbPlayer> Players { get; set; }
    }
}
