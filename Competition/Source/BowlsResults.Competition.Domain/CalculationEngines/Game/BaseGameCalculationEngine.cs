using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game
{
	public enum GameResultMargin
	{
		FiftyFifty,
		Tight,
		Good,
		Strong,
		Big
	}

	public abstract class BaseGameCalculationEngine : IGameCalculationEngine
	{
		public abstract short GameScoreTarget { get; }

		public void Calculate(Entities.Game.Game game)
		{
			if (game.GameStatusID == GameStatuses.Void)
			{
				game.HomeResultTypeID = ResultType.Void;
				game.AwayResultTypeID = ResultType.Void;
				foreach (var gameXPlayer in game.Players)
				{
					gameXPlayer.ResultTypeID = ResultType.Void;
				}
				game.Completed = true;
			}
			else
			{
				if (game.HomeScore == 0 && game.AwayScore == 0)
				{
					game.HomeResultTypeID = null;
					game.AwayResultTypeID = null;
					game.Completed = false;
				}
				else
				{
					this.CalculateGameResult(game);
					this.CalculatePlayerResults(game);
					game.Completed = true;
				}
			}
			game.SetAuditFields();
		}

		public short CalculateDroppedChalks(GameXPlayer gamePlayer)
		{
			switch (gamePlayer.SideID)
			{
				case Sides.Home:
					return (byte)(this.GameScoreTarget - gamePlayer.Game.HomeScore);
				case Sides.Away:
					return (byte)(this.GameScoreTarget - gamePlayer.Game.AwayScore);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public GameResultMargin CalculateResultMargin(Entities.Game.Game game)
		{
			var diff = 0;
			if (game.HomeResultTypeID.Value == ResultType.Win)
			{
				diff = game.HomeScore - game.AwayScore;
			}
			else
			{
				diff = game.AwayScore - game.HomeScore;
			}
			return this.CalculateMargin(diff);
		}

		private void CalculateGameResult(Entities.Game.Game game)
		{
			if (game.HomeScore > game.AwayScore)
			{
				game.HomeResultTypeID = ResultType.Win;
				game.AwayResultTypeID = ResultType.Lose;
			}
			else
			{
				game.HomeResultTypeID = ResultType.Lose;
				game.AwayResultTypeID = ResultType.Win;
			}
		}

		private void CalculatePlayerResults(Entities.Game.Game game)
		{
			foreach (var playerResult in game.Players)
			{
				if (playerResult.SideID == Sides.Home)
				{
					playerResult.ResultTypeID = game.HomeResultTypeID;
				}
				else
				{
					playerResult.ResultTypeID = game.AwayResultTypeID;
				}
			}
		}

		protected abstract GameResultMargin CalculateMargin(int diff);
	}
}