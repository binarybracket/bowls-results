using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Team : AuditableEntity<int>
	{
		public virtual Club Club { get; set; }
		public virtual Genders GenderID { get; set; }
		public virtual AgeGroups AgeGroupID { get; set; }
		public virtual int? TeamHeaderID { get; set; }
		public virtual int? AssociationID { get; set; }
		public virtual string Name { get; set; }
		public virtual string Suffix { get; set; }
		public virtual int? PitchID { get; set; }
		public virtual Contact Captain { get; set; }
	}
}