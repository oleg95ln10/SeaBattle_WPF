using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    class PlayerRepository : IPlayerRepository
    {
        public void AddPlayer(DbPlayer player)
        {
            using (var dbContext = new PlayerContext())
            {
                dbContext.Players.Add(player);
                dbContext.SaveChanges();
            }
        }

        public BindingList<DbPlayer> GetPlayers()
        {
            using (var dbContext = new PlayerContext())
            {
                dbContext.Players.Load();

                return dbContext.Players.Local.ToBindingList();
            }
        }
    }
}
