using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class Fixture : AuditableEntity<short>
	{
		public virtual short CompetitionRoundID { get; set; }
		public virtual Season Season { get; set; }
		public virtual FixtureStatuses FixtureStatusID { get; set; }
		public virtual FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual byte Legs { get; set; }
		public virtual DateTime? PendingDate { get; set; }
		public virtual short? Pending1FixtureID { get; set; }
		public virtual short? Pending1ResultTypeID { get; set; }
		public virtual short? Pending2FixtureID { get; set; }
		public virtual short? Pending2ResultTypeID { get; set; }
		public virtual bool? Completed { get; set; }
		public virtual string Reference { get; set; }
	}
}