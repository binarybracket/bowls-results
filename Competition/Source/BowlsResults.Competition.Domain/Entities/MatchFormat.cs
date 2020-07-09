using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class MatchFormat : IdentityEntity<short>
	{
		public virtual string Name { get; set; }
		public virtual IList<MatchFormatXGameVariation> GameVariations { get; set; }

		public virtual MatchFormatXGameVariation GetVariationByID(int id)
		{
			return this.GameVariations.Single(x => x.ID == id);
		}
	}
}