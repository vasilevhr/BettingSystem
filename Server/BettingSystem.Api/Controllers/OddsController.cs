using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BettingSystem.Data;
using BettingSystem.Models;

namespace BettingSystem.Api.Controllers
{
    public class OddsController : ApiController
    {
        private BettingSystemDbContext db = new BettingSystemDbContext();

        // GET: api/Odds
        public IEnumerable<Odd> GetOdds()
        {
            var odds = new List<Odd>();

            return odds;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OddExists(int id)
        {
            return db.Odds.Count(e => e.Id == id) > 0;
        }
    }
}