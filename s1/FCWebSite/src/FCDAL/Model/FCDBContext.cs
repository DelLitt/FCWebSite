namespace FCDAL.Model
{
    using Microsoft.Data.Entity;
    using FCCore.Configuration;

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
