using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public sealed class ThreesomesGameMap : IdentityEntitySubclassMap<ThreesomesGame, int>
	{
		public ThreesomesGameMap()
		{
			this.DiscriminatorValue((int) GameFormats.Threesomes);
		}
	}
}