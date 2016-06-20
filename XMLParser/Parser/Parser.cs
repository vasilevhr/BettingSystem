namespace Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Xml;
    using BettingSystem.Data;
    using BettingSystem.Models;
    
    public class Parser
    {
        private const string inputUri = @"http://vitalbet.net/sportxml";

        public void Run()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<Match> parsedData = Parse();

            BettingSystemDbContext dbContext = new BettingSystemDbContext();

            foreach (var match in parsedData)
            {
                if (dbContext.Matches != null && dbContext.Matches.Count() > 0 && !dbContext.Matches.Any(x => x.Name == match.Name))
                {
                    dbContext.Matches.Add(match);
                }
                else
                {
                    //remove
                }               
            }

            dbContext.SaveChanges();
        }

        private List<Match> Parse()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inputUri);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/XmlSports/Sport/Event/Match");

            List<Match> matches = new List<Match>();

            foreach (XmlNode node in nodes)
            {
                Match match = new Match();

                match.Name = node.Attributes["Name"].Value;
                match.MatchType = node.Attributes["MatchType"].Value;
                match.StartDate = (node.Attributes["StartDate"] != null) ? Convert.ToDateTime(node.Attributes["StartDate"].Value) : new DateTime();
                match.Id = (node.Attributes["ID"] != null) ? Convert.ToInt32(node.Attributes["ID"].Value) : -1;

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    List<Bet> bets = new List<Bet>();

                    foreach (XmlNode bet in node.ChildNodes)
                    {
                        Bet betToAdd = new Bet()
                        {
                            IsLive = (bet.Attributes["IsLive"] != null) ? Convert.ToBoolean(bet.Attributes["IsLive"].Value) : false,
                            Id = (bet.Attributes["ID"] != null) ? Convert.ToInt32(bet.Attributes["ID"].Value) : 0,
                            Name = bet.Attributes["Name"].Value
                        };

                        if (bet.ChildNodes != null && bet.ChildNodes.Count > 0)
                        {
                            List<Odd> odds = new List<Odd>();

                            foreach (XmlNode odd in bet.ChildNodes)
                            {
                                Odd oddToAdd = new Odd()
                                {
                                    Id = (odd.Attributes["ID"] != null) ? Convert.ToInt32(bet.Attributes["ID"].Value) : 0,
                                    Name = odd.Attributes["Name"].Value,
                                    Value = (odd.Attributes["Value"] != null) ? Convert.ToDouble(odd.Attributes["Value"].Value) : 0.0
                                };

                                // If we have SpecialBetValue attribute
                                if (odd.Attributes.Count > 3)
                                {
                                    oddToAdd.SpecialBetValue = (odd.Attributes["SpecialBetValue"] != null) ? Convert.ToDecimal(odd.Attributes["Value"].Value) : -1;
                                }

                                odds.Add(oddToAdd);
                            }

                            betToAdd.Odds = odds;
                        }

                        bets.Add(betToAdd);
                    }

                    match.Bets = bets;

                    matches.Add(match);
                }
            }

            return matches;
        }
    }
}
