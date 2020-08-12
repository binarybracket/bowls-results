using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class CompetitionResult : IdentityEntity<int>
	{
		public CompetitionResult()
		{
		}
		public virtual CompetitionScopes CompetitionScopeID { get; set; }
		public virtual int SeasonID { get; set; }
		public virtual Competition Competition { get; set; }
	}
}