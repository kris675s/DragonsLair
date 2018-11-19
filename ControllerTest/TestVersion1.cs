using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DragonsLair;
using TournamentLib;

namespace ControllerTest
{
    [TestClass]
    public class TestVersion1
    {
        Controller controller;
        TournamentRepo currentRepo;
        Tournament currentTournament;

        [TestInitialize]
        public void SetupForTest()
        {
            controller = new Controller();
            currentRepo = controller.GetTournamentRepository();
            currentTournament = currentRepo.GetTournament("Vinter Turnering");
            currentTournament.SetupTestTeams();  // Setup 8 teams
        }

        [TestMethod]
        public void TestScheduleNewRoundWithEvenNumberOfTeams()
        {
            Assert.AreEqual(8, currentTournament.GetTeams().Count);

            controller.ScheduleNewRound("Vinter Turnering", false);

            Assert.AreEqual(1, currentTournament.GetNumberOfRounds());
            Assert.AreEqual(8, currentTournament.GetTeams().Count);
        }

        [TestMethod]
        public void TestScheduleNewRoundWithOddNumbersOfTeams()
        {
            currentTournament.AddTeam(new Team("The Andals")); // Add the nine'th team
            Assert.AreEqual(9, currentTournament.GetTeams().Count);

            controller.ScheduleNewRound("Vinter Turnering", false);

            Assert.AreEqual(1, currentTournament.GetNumberOfRounds());
            Assert.AreEqual(9, currentTournament.GetTeams().Count);
        }
        [TestMethod]
        public void TestOddNumberOfTeamsGivesFreeRider()
        {
            currentTournament.AddTeam(new Team("The Andals")); // Add the nine'th team
            controller.ScheduleNewRound("Vinter Turnering", false);
            Assert.AreNotEqual(null, currentTournament.GetRound(0).FreeRider);
        }

        [TestMethod]
        public void TestWinningTeamInRound0IsRegistered()
        {
            String winnerName = "The Spartans";
            controller.ScheduleNewRound("Vinter Turnering", false);
            controller.SaveMatch("Vinter Turnering", 0, winnerName);
            Match m = currentTournament.GetRound(0).GetMatch(winnerName);
            Assert.AreEqual(winnerName, m.Winner.Name);
        }

        [TestMethod]
        public void TestWinningTeamInRound1IsRegistered()
        {
            String winnerName = "The Coans";

            controller.ScheduleNewRound("Vinter Turnering", false);
            controller.SaveMatch("Vinter Turnering", 0, "The Spartans");
            controller.SaveMatch("Vinter Turnering", 0, "The Cretans");
            controller.SaveMatch("Vinter Turnering", 0, "The Coans");
            controller.SaveMatch("Vinter Turnering", 0, "The Megareans");

            controller.ScheduleNewRound("Vinter Turnering", false);
            controller.SaveMatch("Vinter Turnering", 1, winnerName);

            Match m = currentTournament.GetRound(1).GetMatch(winnerName);
            Assert.AreEqual(winnerName, m.Winner.Name);
        }
        [TestMethod]
        public void TournamentHasEvenNumberOfTeams()
        {
            int numberOfTeams = currentTournament.GetTeams().Count;
            Assert.AreEqual(0, numberOfTeams % 2);
        }

        [TestMethod]
        public void EqualNumberOfWinnersAndLosersPerRound()
        {
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            for (int round = 0; round < numberOfRounds; round++)
            {
                Round currentRound = currentTournament.GetRound(round);
                int numberOfWinningTeams = currentRound.GetWinningTeams().Count;
                int numberOfLosingTeams = currentRound.GetLosingTeams().Count;
                Assert.AreEqual(numberOfWinningTeams, numberOfLosingTeams);
            }
        }

        [TestMethod]
        public void OneWinnerInLastRound()
        {
            // Verifies there is exactly one winner in last round
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            Round currentRound = currentTournament.GetRound(numberOfRounds - 1);
            int numberOfWinningTeams = currentRound.GetWinningTeams().Count;
            Assert.AreEqual(1, numberOfWinningTeams);
        }

        [TestMethod]
        public void AllMatchesInPreviousRoundsFinished()
        {
            bool matchesFinished = true;
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            for (int round = 0; round < numberOfRounds; round++)
            {
                Round currentRound = currentTournament.GetRound(round);
                if (currentRound.IsMatchesFinished() == false)
                    matchesFinished = false;
            }
            Assert.AreEqual(true, matchesFinished);
        }
    }
}
