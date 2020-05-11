using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Models
{
	public class Club : AuditableEntity<int>
	{
		public Club()
		{
		}

		public virtual int AssociationID { get; set; }
		public virtual string Name { get; set; }
		public virtual int? PitchID { get; set; }
	}
}