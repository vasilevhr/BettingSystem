namespace BettingSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class BettingSystemDbContext : IdentityDbContext<User>
    {
        public BettingSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            if (!Database.Exists())
            {
                Database.Create();
            }
        }
        
        public virtual IDbSet<Match> Matches { get; set; }

        public virtual IDbSet<Bet> Bets { get; set; }

        public virtual IDbSet<Odd> Odds { get; set; }

        public static BettingSystemDbContext Create()
        {
            return new BettingSystemDbContext();
        }
    }
}
