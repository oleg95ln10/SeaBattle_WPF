using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public interface IPlayerRepository
    {
        void AddPlayer(DbPlayer player);
        BindingList<DbPlayer> GetPlayers();
    }
}
