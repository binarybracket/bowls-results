using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistrationConfiguration : IdentityEntity<int>
	{
		public virtual int ID { get; set; }
		public virtual Competition Competition { get; set; }
		public virtual CompetitionRegistrationModes CompetitionRegistrationModeID { get; set; }
		public virtual DateTime? OpenDate { get; set; }
		public virtual DateTime? CloseDate { get; set; }
		public virtual decimal Amount { get; set; }
	}
}