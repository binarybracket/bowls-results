using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public sealed class SinglesGameMap : IdentityEntitySubclassMap<SinglesGame, int>
	{
		public SinglesGameMap()
		{
			this.DiscriminatorValue((int)GameFormats.Singles);
		}
	}
}