using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Player : AuditableEntity<int>
	{
		public virtual string Forename { get; set; }
		public virtual string Surname { get; set; }
		public virtual byte GenderID { get; set; }
		public virtual string RegistrationID { get; set; }
		public virtual string AlternativeRegistrationID { get; set; }
		public virtual bool Internal { get; set; }
	}
}