﻿using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

 namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game
{
	public interface IGameCalculationEngine
	{
		short GameScoreTarget { get; }
		void Calculate(Entities.Game.Game game);
		short CalculateDroppedChalks(GameXPlayer gamePlayer);
		GameResultMargin CalculateResultMargin(Entities.Game.Game game);
	}
}