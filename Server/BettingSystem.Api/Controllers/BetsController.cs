namespace BettingSystem.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.Http;
    using BettingSystem.Data;
    using BettingSystem.Models;

    public class BetsController : ApiController
    {
        private BettingSystemDbContext db = new BettingSystemDbContext();

        // GET: api/Bets
        public IEnumerable<Bet> GetBets()
        {
            DateTime datePlus24hours = DateTime.Now.AddHours(24);
            DateTime dateTimeNow = DateTime.Now;

            var matches = db.Matches
                        .Where(m => m.StartDate.CompareTo(datePlus24hours) == -1 &&
                                    m.StartDate.CompareTo(dateTimeNow) == 1)
                        .OrderBy(st => st.StartDate)
                        .ToList();

            var matchBets = new List<Bet>();
            var allBets = new List<Bet>();

            foreach (var match in matches)
            {
                matchBets = match.Bets
                    .Where(b => b.MatchId == match.Id)
                    .ToList();

                foreach (var bet in matchBets)
                {
                    allBets.Add(bet);
                }
            }
            
            return allBets;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BetExists(int id)
        {
            return db.Bets.Count(e => e.Id == id) > 0;
        }
    }
}