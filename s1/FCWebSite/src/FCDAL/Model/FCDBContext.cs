using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using FCCore.Configuration;

namespace FCDAL.Model
{
    /// <summary>
    /// Using to save connection string from generation
    /// </summary>
    public class FCDBContext : FCWebContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(MainCfg.CoreConfig.Current["Data:DefaultConnection:ConnectionString"]);
        }
    }
}
