using Com.BinaryBracket.Core.Domain2.Entities;

namespace BowlsResults.Competition.Domain.Models
{
	public class CompetitionHeader : IdentityEntity<int>
	{
		public virtual string Name { get; set; }
		public virtual int AssociationID { get; set; }
		public virtual string ShortName { get; set; }
		public virtual int Priority { get; set; }
	}
}