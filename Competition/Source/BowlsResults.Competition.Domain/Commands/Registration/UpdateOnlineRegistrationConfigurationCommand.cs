using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration
{
	public class UpdateOnlineRegistrationConfigurationCommand : ICommand<DefaultCommandResponse>
	{
		public int CompetitionID { get; set; }
		public DateTime? OpenDate { get; set; }
		public DateTime? CloseDate { get; set; }
		
		public decimal? Amount { get; set; }
		public int? OrganiserContactID { get; set; }
		
		public CompetitionRegistrationModes? RegistrationMode { get; set; }
		public bool ShowQualificationDates { get; set; }
	}
}