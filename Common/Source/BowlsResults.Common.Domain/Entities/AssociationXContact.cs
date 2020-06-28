using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class AssociationXContact : IdentityEntity<int>
	{
		public virtual Association Association { get; set; }
		public virtual Contact Contact { get; set; }
	}
}