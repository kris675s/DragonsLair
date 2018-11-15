using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;

namespace DragonsLair
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();
        private List<Team> Scramble(List<Team> teams)
        {
            return teams;
        }
        
       
        public void ShowScore(string tournamentName)
        {
            Console.WriteLine("Implement this method!");
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds();

            Round lastRound;
            bool isRoundFinished;
            List<Team> teams = new List<Team>();
            Team oldFreeRider;
            Team newFreeRider = null;
            List<Team> scramble;

            if (numberOfRounds == 0)
            {
                lastRound = null;
                isRoundFinished = true;
            }
            else
            {
                lastRound = t.GetRound(numberOfRounds - 1);
                isRoundFinished = lastRound.IsMatchesFinished();
            }
            if (isRoundFinished == true)
            {
                if (lastRound == null)
                {
                    teams = t.GetTeams();
                }
                else
                {
                    teams = lastRound.GetWinningTeams();
                    if(lastRound.FreeRider != null )
                    {
                        teams.Add(lastRound.FreeRider);
                    }
                }
                if (teams.Count > 1)
                {
                    scramble = teams.ToList();
                    Round newRound = new Round();

                    if (scramble.Count % 2 != 0)
                    {
                        if (numberOfRounds > 0)
                        {
                            oldFreeRider = lastRound.FreeRider;
                        }
                        else
                        {
                            oldFreeRider = null;
                        }

                        int x = 0;
                        /*do
                        {
                            newFreeRider = scramble[x++];
                        }
                        */

                        while (newFreeRider == oldFreeRider)
                        {
                            newFreeRider = scramble[x];
                            x++;
                        }

                        newRound.FreeRider =newFreeRider;
                        scramble.Remove(newFreeRider);
                    }
                    for (int i = 0; i < scramble.Count; i = i + 2)
                    {
                        Match match = new Match();
                        match.FirstOpponent = scramble[i];
                        match.SecondOpponent = scramble[i + 1]; //test virker ikke fix bounds out
                        newRound.AddMatch(match);
                    }
                    t.AddRound(newRound);
                    // Vis kampe i ny runde (se wireframe) Sprint 2 dag 1
                }
                else
                {
                    throw new Exception("Tournament is finished");
                }
            }
            else
            {
                throw new Exception("Round not finished");
            }
        }
        public void SaveMatch(string tournamentName, int round, string winner)
        {
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            Round r = t.GetRound(round);
            Match m = r.GetMatch(winner);

            if(m!= null && m.Winner == null)
            {
                Team w = t.GetTeam(winner);
                Console.WriteLine("Kampen mellem "+ m.FirstOpponent +" og "+ m.SecondOpponent +" i runde 2 i turneringen "+ tournamentName +" er nu afviklet. Vinderen blev "+ winner +".");
                m.Winner = w;
            }
            else
            {
                Console.WriteLine("indtastede Hold, kan ikke være vinder i denne runde, da holdet enten ikke deltager i runden eller kampen allerede er registreret med en vinder.");
            }
        }

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
        }
    }
}
