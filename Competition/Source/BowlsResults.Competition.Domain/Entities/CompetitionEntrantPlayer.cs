using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class CompetitionEntrantPlayer : IdentityEntity<int>
	{
		public virtual CompetitionEntrant CompetitionEntrant { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual int? PlayerID { get; set; }
	}
}