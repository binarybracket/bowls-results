using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player
{
	public abstract class BasePlayerMatchCalculationEngine
	{
		protected void CalculateChalksFromGames(PlayerMatch match)
		{
			match.HomeChalkScore = 0;
			match.AwayChalkScore = 0;

			foreach (PlayerMatchXGame playerGame in match.ValidGamesForCalculation)
			{
				match.HomeChalkScore += playerGame.Game.HomeScore;
				match.AwayChalkScore += playerGame.Game.AwayScore;
			}

			if (match.HomeChalkHandicap.HasValue)
			{
				match.HomeChalkScore += match.HomeChalkHandicap;
			}
			if (match.AwayChalkHandicap.HasValue)
			{
				match.AwayChalkScore += match.AwayChalkHandicap;
			}
		}

		protected void CalculateGamesScoreFromGames(PlayerMatch match)
		{
			match.HomeGameScore = (short)match.ValidGamesForCalculation.Count(x => x.Game.HomeResultTypeID == ResultType.Win);
			match.AwayGameScore = (short)match.ValidGamesForCalculation.Count(x => x.Game.AwayResultTypeID == ResultType.Win);
		}
	}
}