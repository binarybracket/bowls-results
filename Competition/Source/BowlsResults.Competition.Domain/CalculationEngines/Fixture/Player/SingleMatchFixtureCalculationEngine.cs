using System.Linq;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public class SingleMatchFixtureCalculationEngine : BasePlayerFixtureCalculationEngine
	{
		protected override void InnerCalculate(PlayerFixture fixture)
		{
			if (fixture.Matches.All(x => x.MatchStatusID.IsProcessedWithResult()))
			{
				var match = fixture.Matches.FirstOrDefault();

				if (match != null)
				{
					this.CalculateMatch(fixture, match);
					fixture.SetComplete();
				}
				else
				{
					fixture.SetIncomplete();
				}
			}
			else
			{
				fixture.SetIncomplete();
			}
		}

		protected virtual void CalculateMatch(PlayerFixture fixture, Entities.Match.Match match)
		{
			this.CalculateChalks(fixture, match);
			this.CalculateGames(fixture, match);
			this.CalculateBonusPoints(fixture, match);
			this.CalculateResultType(fixture, match);

			fixture.Entrant1Walkover = match.HomeWalkover;
			fixture.Entrant2Walkover = match.AwayWalkover;
		}

		protected void CalculateChalks(PlayerFixture fixture, Entities.Match.Match match)
		{
			fixture.Entrant1ChalkScore = match.HomeChalkScore;
			fixture.Entrant2ChalkScore = match.AwayChalkScore;
		}

		protected void CalculateGames(PlayerFixture fixture, Entities.Match.Match match)
		{
			fixture.Entrant1GameScore = match.HomeGameScore;
			fixture.Entrant2GameScore = match.AwayGameScore;
		}

		protected void CalculateResultType(PlayerFixture fixture, Entities.Match.Match match)
		{
			fixture.Entrant1ResultTypeID = match.HomeResultTypeID;
			fixture.Entrant2ResultTypeID = match.AwayResultTypeID;
		}

		protected void CalculateBonusPoints(PlayerFixture fixture, Entities.Match.Match match)
		{
			fixture.Entrant1BonusScore = match.HomeBonusScore;
			fixture.Entrant2BonusScore = match.AwayBonusScore;
		}
	}
}