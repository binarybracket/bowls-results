using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class Fixture : AuditableEntity<short>
	{		
		public virtual CompetitionScopes CompetitionScopeID { get; set; }
		public virtual Season Season { get; set; }
		public virtual FixtureStatuses FixtureStatusID { get; set; }
		public virtual FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual byte Legs { get; set; }
		public virtual DateTime? PendingDate { get; set; }
		public virtual short? Pending1FixtureID { get; set; }
		public virtual ResultType? Pending1ResultTypeID { get; set; }
		public virtual short? Pending2FixtureID { get; set; }
		public virtual ResultType? Pending2ResultTypeID { get; set; }
		
		public virtual short? Entrant1GameScore { get; set; }
		public virtual short? Entrant2GameScore { get; set; }
		public virtual short? Entrant1ChalkScore { get; set; }
		public virtual short? Entrant2ChalkScore { get; set; }
		public virtual short? Entrant1BonusScore { get; set; }
		public virtual short? Entrant2BonusScore { get; set; }
		public virtual bool? Entrant1Walkover { get; set; }
		public virtual bool? Entrant2Walkover { get; set; }
		public virtual ResultType? Entrant1ResultTypeID { get; set; }
		public virtual ResultType? Entrant2ResultTypeID { get; set; }
		
		public virtual bool? Completed { get; set; }
		public virtual string Reference { get; set; }
		
		public virtual void SetIncomplete()
		{
			this.FixtureStatusID = FixtureStatuses.Incomplete;
			this.SetAuditFields();
		}

		public virtual void SetComplete()
		{
			this.FixtureStatusID = FixtureStatuses.Complete;
			this.SetAuditFields();
		}
	}
}