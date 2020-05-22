using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	public class GameXPlayer : IdentityEntity<int>
	{
		public virtual Game Game { get; set; }
		public virtual Player Player { get; set; }
		public virtual Sides SideID { get; set; }
		public virtual ResultType? ResultTypeID { get; set; }
	}
}