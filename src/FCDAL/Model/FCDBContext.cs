namespace FCDAL.Model
{
    using FCCore.Configuration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Using to save connection string from generation
    /// </summary>
    public class FCDBContext : FCWebContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // TODO : Remove UseRowNumberForPaging() for SQL Server wich supports NEXT FETCH statements. 2008 does not support
            options.UseSqlServer(MainCfg.CoreConfig.Current["Data:DefaultConnection:ConnectionString"], ob => ob.UseRowNumberForPaging());
        }
    }
}
