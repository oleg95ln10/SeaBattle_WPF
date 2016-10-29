using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SeaBattle.Model
{
    public class AutofacConfig
    {
        private static IPlayerRepository _repository;

        public static IPlayerRepository Repository
        {
            get  { return _repository;  }
            set  { _repository = value; }
        }

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PlayerRepository>()
                .As<IPlayerRepository>()
                .WithParameter("context", new PlayerContext());

            var container = builder.Build(); 

            _repository = container.Resolve<IPlayerRepository>();

        }
    }
}