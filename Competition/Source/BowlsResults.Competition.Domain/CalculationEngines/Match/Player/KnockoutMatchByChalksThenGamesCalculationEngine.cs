using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player
{
	public sealed class KnockoutPlayerMatchByChalksGamesCalculationEngine : BaseKnockoutPlayerMatchCalculationEngine
	{
		private KnockoutPlayerMatchByChalksGamesCalculationEngine()
		{
		}

		public static KnockoutPlayerMatchByChalksGamesCalculationEngine Instance = new KnockoutPlayerMatchByChalksGamesCalculationEngine();

		/// <summary>
		/// Calculation Match Result From Games
		/// </summary>
		protected override void InnerCalculateResultFromGames(PlayerMatch match)
		{
			this.CalculateChalksFromGames(match);
			this.CalculateGamesScoreFromGames(match);
			this.CalculateResultType(match);
			match.SetAuditFields();
		}

		private void CalculateResultType(PlayerMatch match)
		{
			if (match.HomeChalkScore == match.AwayChalkScore)
			{
				if (match.HomeGameScore > match.AwayGameScore)
				{
					match.HomeResultTypeID = ResultType.Win;
					match.AwayResultTypeID = ResultType.Lose;
				}
				else if (match.HomeGameScore < match.AwayGameScore)
				{
					match.HomeResultTypeID = ResultType.Lose;
					match.AwayResultTypeID = ResultType.Win;
				}
			}
			else
			{
				if (match.HomeChalkScore > match.AwayChalkScore)
				{
					match.HomeResultTypeID = ResultType.Win;
					match.AwayResultTypeID = ResultType.Lose;
				}
				else
				{
					match.HomeResultTypeID = ResultType.Lose;
					match.AwayResultTypeID = ResultType.Win;
				}
			}
		}
	}
}