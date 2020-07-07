using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Business.CalculationEngines.TeamMatch
{
	public static class TeamMatchCalculationEngineFactory
	{
		public static IPlayerMatchCalculationEngine CreateTeamMatchCalculationEngine(MatchCalculationEngines engineID)
		{
			switch (engineID)
			{
				case MatchCalculationEngines.KnockoutMatchByGames:
					return KnockoutPlayerMatchByGamesCalculationEngine.Instance;
				default:
					throw new ArgumentOutOfRangeException("engineID");
			}
		}
	}
}