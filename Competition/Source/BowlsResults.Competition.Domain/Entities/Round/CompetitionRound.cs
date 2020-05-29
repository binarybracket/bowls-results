using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round
{
	public class CompetitionRound : IdentityEntity<short>
	{
		public virtual CompetitionEvent CompetitionEvent { get; set; }
		public virtual Competition Competition { get; set; }
		public virtual Season Season { get; set; }
		public virtual CompetitionRoundTypes CompetitionRoundTypeID { get; set; }
		public virtual CompetitionScopes CompetitionScopeID { get; set; }
		public virtual short GameNumber { get; set; }
		public virtual string Notes { get; set; }
	}
}