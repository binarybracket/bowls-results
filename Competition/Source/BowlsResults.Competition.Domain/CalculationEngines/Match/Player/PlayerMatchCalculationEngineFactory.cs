using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player
{
	public sealed class PlayerMatchCalculationEngineFactory : IPlayerMatchCalculationEngineFactory
	{
		public IPlayerMatchCalculationEngine Create(MatchCalculationEngines engineID)
		{
			switch (engineID)
			{
				case MatchCalculationEngines.KnockoutMatchByGames:
					return KnockoutPlayerMatchByGamesCalculationEngine.Instance;
				case MatchCalculationEngines.KnockoutMatchByChalksThenGames:
					return KnockoutPlayerMatchByChalksGamesCalculationEngine.Instance;
				default:
					throw new ArgumentOutOfRangeException("engineID");
			}
		}
	}
}