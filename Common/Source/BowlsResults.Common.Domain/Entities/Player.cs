using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Player : AuditableEntity<int>
	{
		public virtual string Forename { get; set; }
		public virtual string Surname { get; set; }
		public virtual Genders GenderID { get; set; }
		public virtual string RegistrationID { get; set; }
		public virtual string AlternativeRegistrationID { get; set; }
		public virtual bool Internal { get; set; }
		
		public virtual bool IsInternalPlayer()
		{
			return this.ID < 0 || this.Internal;
		}

		public virtual bool IsRealPlayer()
		{
			return !this.IsInternalPlayer();
		}

		public virtual bool IsNoNamePlayer()
		{
			return this.ID == -6;
		}

		public virtual bool IsVoidPlayer()
		{
			return this.ID == -7;
		}
	}
}