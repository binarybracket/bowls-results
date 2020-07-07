using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public class GameXPlayerMap : IdentityEntityMap<GameXPlayer, int>
	{
		public GameXPlayerMap()
		{
			this.Table("GameXPlayer");
			this.LazyLoad();
			this.Id(x => x.ID).GeneratedBy.Identity().Column("ID");
			this.References(x => x.Game).Column("GameID");
			this.References(x => x.Player).Column("PlayerID");
			this.Map(x => x.ResultTypeID).Column("ResultTypeID").CustomType<ResultType>();

			this.DiscriminateSubClassesOnColumn("SideID");
		}
	}
}