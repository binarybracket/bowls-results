using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class Knockout : CompetitionEvent 
	{
		public Knockout()
		{
			this.CompetitionEventTypeID = CompetitionEventTypes.Knockout;
		}
	
		public virtual KnockoutCalculationEngines KnockoutCalculationEngineID { get; set; }

		//public virtual ISet<KnockoutDate> KnockoutDates { get; set; }
		
		public static Knockout Create(CompetitionStage stage, KnockoutCalculationEngines knockoutCalculationEngineID)
		{
			var data = new Knockout
			{
				Competition = stage.Competition,
				Season = stage.Competition.Season,
				CompetitionStage = stage,
				KnockoutCalculationEngineID = knockoutCalculationEngineID
			};

			return data;
		}
	}

	
}