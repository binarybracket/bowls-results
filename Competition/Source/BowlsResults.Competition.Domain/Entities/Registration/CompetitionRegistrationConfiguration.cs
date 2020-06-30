using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistrationConfiguration : IdentityEntity<int>
	{
		public virtual Competition Competition { get; set; }
		public virtual CompetitionRegistrationModes CompetitionRegistrationModeID { get; set; }
		public virtual DateTime? OpenDate { get; set; }
		public virtual DateTime? CloseDate { get; set; }
		public virtual decimal? Amount { get; set; }
		public virtual Contact OrganiserContact { get; set; }

		public virtual CompetitionRegistrationStatuses CalculateStatus()
		{
			if (this.OpenDate.HasValue && this.OpenDate.Value > DateTime.UtcNow)
			{
				return CompetitionRegistrationStatuses.NotOpenYet;
			}
			
			if (this.CloseDate.HasValue)
			{
				if (DateTime.UtcNow > this.CloseDate)
				{
					return CompetitionRegistrationStatuses.Closed;
				}

				TimeSpan difference = (DateTime.UtcNow - this.CloseDate.Value);
				if (difference.TotalDays <= 3)
				{
					return CompetitionRegistrationStatuses.ClosingSoon;
				}
			}

			return CompetitionRegistrationStatuses.Open;
		}
	}
}