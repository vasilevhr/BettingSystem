namespace BettingSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Odd
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public double Value { get; set; }
        
        public decimal? SpecialBetValue { get; set; }

        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
