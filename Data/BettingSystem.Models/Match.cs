namespace BettingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Match
    {
        private ICollection<Bet> bets;

        public Match()
        {
            this.bets = new HashSet<Bet>();
        }

        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Name length must be less than 100 symbols.")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        [MaxLength(25)]
        public string MatchType { get; set; }

        public virtual ICollection<Bet> Bets
        {
            get { return this.bets; }
            set { this.bets = value; }
        }
    }
}
