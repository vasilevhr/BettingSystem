namespace BettingSystem.Api
{
    using Data.Migrations;
    using Data;
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BettingSystemDbContext, Configuration>());
            //**BettingSystemDbContext.Create().Database.Initialize(true);
        }
    }
}