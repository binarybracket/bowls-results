using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Models
{
	public class Season : IdentityEntity<short>
	{
		public Season()
		{
		}

		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual string DisplayName { get; set; }
	}
}