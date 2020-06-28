using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class ClubXContact : IdentityEntity<int>
	{
		public virtual Club Club { get; set; }
		public virtual Contact Contact { get; set; }
	}
}