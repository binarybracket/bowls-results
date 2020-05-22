using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public class AwayPlayerMap : IdentityEntitySubclassMap<AwayPlayer, int>
	{
		public AwayPlayerMap()
		{
			this.DiscriminatorValue((int)Sides.Away);
		}
	}
}