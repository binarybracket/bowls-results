using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class GameVariation : IdentityEntity<byte>
	{
		public virtual GameFormats GameFormatID { get; set; }
		public virtual Genders GenderID { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
	}
}