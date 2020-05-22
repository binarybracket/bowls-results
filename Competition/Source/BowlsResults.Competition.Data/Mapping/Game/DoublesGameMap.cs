using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public sealed class DoublesGameMap : IdentityEntitySubclassMap<DoublesGame, int>
	{
		public DoublesGameMap()
		{
			this.DiscriminatorValue((int) GameFormats.Doubles);
		}
	}
}