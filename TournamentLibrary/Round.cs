using System;
using System.Collections.Generic;

namespace TournamentLib
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        
        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            foreach (Match m in matches)
            {
                Match result = null;
                {
                if (m.FirstOpponent.Name == teamName1 && m.SecondOpponent.Name == teamName2)
                {
                    result = m;
                }

                }

            }
            return null;
        }

        public bool IsMatchesFinished()
        {
            /* TODO: Implement this method
            bool
            */
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Winner == null)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Team> GetWinningTeams()
        {
            /* TODO: Implement this method
            GetWinningTeams()
            */
            List<Team> winners = new List<Team>();

            for (int i = 0; i < matches.Count; i++)
            {
                winners.Add(matches[i].Winner);
            }
            return winners;
        }

        public List<Team> GetLosingTeams()
        {
            // TODO: Implement this method
            List<Team> loosers = new List<Team>();

            foreach (Match m in matches)
            {
                if(m.Winner == m.FirstOpponent)
                {
                    loosers.Add(m.SecondOpponent);
                }
                else
                {
                    loosers.Add(m.FirstOpponent);
                }
            }
            return loosers;
        }

        private Team freeRider;
        public Team FreeRider
        {
            get => freeRider;
            set { freeRider = value; }
        }

        public Match GetMatch(string team)
        {
            Match result = null;

            foreach (Match match in matches)
            {
                if (match.FirstOpponent.Name.Equals(team) || match.SecondOpponent.Name.Equals(team))
                {
                    result = match;
                }
            }
            return result;
        }
    }
}
