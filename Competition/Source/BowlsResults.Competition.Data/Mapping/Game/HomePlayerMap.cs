using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public class HomePlayerMap : IdentityEntitySubclassMap<HomePlayer, int>
	{
		public HomePlayerMap()
		{
			this.DiscriminatorValue((int)Sides.Home);
		}
	}
}