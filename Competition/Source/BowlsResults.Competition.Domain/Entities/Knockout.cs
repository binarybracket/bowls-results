using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class Knockout : CompetitionEvent 
	{
		public Knockout()
		{
			this.CompetitionEventTypeID = CompetitionEventTypes.Knockout;
		}
	
		public virtual KnockoutCalculationEngine KnockoutCalculationEngine { get; set; }

		//public virtual ISet<KnockoutDate> KnockoutDates { get; set; }
		
		public static Knockout Create(CompetitionStage stage, KnockoutCalculationEngine knockoutCalculationEngine)
		{
			var data = new Knockout
			{
				Competition = stage.Competition,
				Season = stage.Competition.Season,
				CompetitionStage = stage,
				KnockoutCalculationEngine = knockoutCalculationEngine
			};

			return data;
		}

		public override MatchFormat GetMatchFormat()
		{
			return this.KnockoutCalculationEngine.MatchFormat;
		}
		
		public override FixtureCalculationEngines GetFixtureCalculationEngine()
		{
			return this.KnockoutCalculationEngine.FixtureCalculationEngineID;
		}

		public override MatchCalculationEngines GetMatchCalculationEngine()
		{
			return this.KnockoutCalculationEngine.MatchCalculationEngineID;
		}
	}
}