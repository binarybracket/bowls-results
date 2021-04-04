using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public class IomMixedPlayerFixtureCalculationEngine : BasePlayerFixtureCalculationEngine
	{
		protected override void InnerCalculate(PlayerFixture fixture)
		{
			if (fixture.Matches.Count == fixture.Legs && fixture.Matches.All(x => MatchStatus.IsProcessedWithResult(x.MatchStatusID)))
			{
				fixture.Entrant1GameScore = 0;
				fixture.Entrant1ChalkScore = 0;
				fixture.Entrant1Walkover = true;
				fixture.Entrant2GameScore = 0;
				fixture.Entrant2ChalkScore = 0;
				fixture.Entrant2Walkover = true;

				foreach (PlayerMatch playerMatch in fixture.Matches)
				{
					short gameScore;
					short chalkScore;
					short? bonusScore;
					bool? walkover;
					playerMatch.GetScoresByEntrantID(fixture.Entrant1.ID, out gameScore, out chalkScore, out bonusScore, out walkover);

					fixture.Entrant1GameScore += gameScore;
					fixture.Entrant1ChalkScore += chalkScore;
					if (bonusScore.HasValue)
					{
						if (!fixture.Entrant1BonusScore.HasValue)
						{
							fixture.Entrant1BonusScore = 0;
						}

						fixture.Entrant1BonusScore += bonusScore.Value;
					}

					if (walkover.HasValue)
					{
						fixture.Entrant1Walkover &= walkover.Value;
					}

					playerMatch.GetScoresByEntrantID(fixture.Entrant2.ID, out gameScore, out chalkScore, out bonusScore, out walkover);

					fixture.Entrant2GameScore += gameScore;
					fixture.Entrant2ChalkScore += chalkScore;
					if (bonusScore.HasValue)
					{
						if (!fixture.Entrant2BonusScore.HasValue)
						{
							fixture.Entrant2BonusScore = 0;
						}

						fixture.Entrant2BonusScore += bonusScore.Value;
					}

					if (walkover.HasValue)
					{
						fixture.Entrant2Walkover &= walkover.Value;
					}
				}

				this.CalculateResultType(fixture);

				fixture.SetComplete();
			}
			else
			{
				fixture.SetIncomplete();
			}
		}

		private void CalculateResultType(PlayerFixture fixture)
		{
			if (fixture.Entrant1ChalkScore == fixture.Entrant2ChalkScore)
			{
				if (fixture.Entrant1GameScore > fixture.Entrant2GameScore)
				{
					fixture.Entrant1ResultTypeID = ResultType.Win;
					fixture.Entrant2ResultTypeID = ResultType.Lose;
				}
				else
				{
					fixture.Entrant1ResultTypeID = ResultType.Lose;
					fixture.Entrant2ResultTypeID = ResultType.Win;
				}
			}
			else if (fixture.Entrant1ChalkScore > fixture.Entrant2ChalkScore)
			{
				fixture.Entrant1ResultTypeID = ResultType.Win;
				fixture.Entrant2ResultTypeID = ResultType.Lose;
			}
			else
			{
				fixture.Entrant1ResultTypeID = ResultType.Lose;
				fixture.Entrant2ResultTypeID = ResultType.Win;
			}
		}
	}
}