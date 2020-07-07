﻿using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

 namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player
{
	public interface IPlayerMatchCalculationEngine
	{
		void CalculateResultFromGames(PlayerMatch match);
	}
}