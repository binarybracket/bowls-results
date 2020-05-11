using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Models
{
	public class Pitch : IdentityEntity<int>
	{
		public virtual string Name { get; set; }
	}
}