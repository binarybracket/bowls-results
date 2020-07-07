using System;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player
{
	public abstract class BaseKnockoutPlayerMatchCalculationEngine : BasePlayerMatchCalculationEngine, IPlayerMatchCalculationEngine
	{
		/// <summary>
		/// Calculation Match Result From Games
		/// </summary>
		public void CalculateResultFromGames(PlayerMatch match)
		{
			if (match.Games.Count > 0 && match.Games.All(x => x.Game.Completed))
			{
				match.HomeResultTypeID = ResultType.Draw;
				match.AwayResultTypeID = ResultType.Draw;

				this.InnerCalculateResultFromGames(match);

				if (match.HomeResultTypeID == ResultType.Draw || match.AwayResultTypeID == ResultType.Draw)
				{
					throw new InvalidOperationException(string.Format("Match [{0}] cannot end in a draw for Knockout Calculation Engines", match.ID));
				}

				match.SetComplete();
			}
			else
			{
				match.SetIncomplete();
			}
		}

		/// <summary>
		/// Calculation Match Result From Games
		/// </summary>
		protected abstract void InnerCalculateResultFromGames(PlayerMatch match);
	}
}