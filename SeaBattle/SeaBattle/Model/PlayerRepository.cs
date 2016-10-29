using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Reposit for DB
    /// Client doesn't khows about DB
    /// If we will choose DB, should be rewrite
    /// </summary>
    class PlayerRepository : IPlayerRepository, IDisposable
    {
        PlayerContext _dbContext;

        public PlayerRepository(PlayerContext context)
        { 
            _dbContext = context;
        }

        public void AddPlayer(DbPlayer player)
        {
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
        }

        public BindingList<DbPlayer> GetPlayers()
        {
            _dbContext.Players.Load();
            return _dbContext.Players.Local.ToBindingList();
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
