using System.Collections.Generic;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class MatchFormat : IdentityEntity<short>
	{
		public virtual string Name { get; set; }
		public virtual IList<MatchFormatXGameVariation> GameVariations { get; set; }
	}
}