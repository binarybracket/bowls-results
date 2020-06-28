using System.Collections.Generic;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Association : AuditableEntity<int>
	{
		public virtual string Name { get; set; }
		public virtual string ShortName { get; set; }
		
		public virtual IList<AssociationXContact> Contacts { get; set; }
	}
}