using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class KnockoutDate : IdentityEntity<int>
	{
		public virtual short CompetitionID { get; set; }
		public virtual short CompetitionRoundID { get; set; }
		public virtual short KnockoutID { get; set; }
		public virtual DateTime Date { get; set; }
	}
}