using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class CompetitionStage : IdentityEntity<short>
	{
		public virtual CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public virtual Competition Competition { get; set; }
		public virtual byte Sequence { get; set; }
		public virtual string Name { get; set; }
	}
}