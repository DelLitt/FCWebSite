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
            options.UseSqlServer(MainCfg.CoreConfig.Current["Data:DefaultConnection:ConnectionString"]);
        }
    }
}
