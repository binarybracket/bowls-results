using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
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
		public virtual GameFormats? EntryGameFormatID { get; set; }

		public virtual CompetitionRegistrationStatuses CalculateOnlineStatus()
		{
			if ((DateTime.UtcNow - this.Competition.StartDate).Days > 1)
			{
				return CompetitionRegistrationStatuses.Past;
			}
			
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

				TimeSpan difference = (this.CloseDate.Value - DateTime.UtcNow);
				if (difference.TotalDays <= 3)
				{
					return CompetitionRegistrationStatuses.ClosingSoon;
				}
			}

			return CompetitionRegistrationStatuses.Open;
		}
		
		public virtual CompetitionRegistrationStatuses CalculateInvitationalStatus()
		{
			if ((DateTime.UtcNow - this.Competition.StartDate).Days > 1)
			{
				return CompetitionRegistrationStatuses.Past;
			}

			return CompetitionRegistrationStatuses.Invitational;
		}
		
		public virtual CompetitionRegistrationStatuses CalculateUnavailableStatus()
		{
			if ((DateTime.UtcNow - this.Competition.StartDate).Days > 1)
			{
				return CompetitionRegistrationStatuses.Past;
			}

			return CompetitionRegistrationStatuses.Unavailable;
		}

		public virtual bool IsClosed()
		{
			return this.CloseDate < DateTime.UtcNow;
		}
	}
}