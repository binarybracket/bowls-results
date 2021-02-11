using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Messages;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Domain2.Email;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration
{
	public sealed class RegistrationEmailManager : IRegistrationEmailManager
	{
		private readonly IEmailSender _emailSender;
		private readonly ICompetitionDateRepository _competitionDateRepository;

		public RegistrationEmailManager(IEmailSender emailSender, ICompetitionDateRepository competitionDateRepository)
		{
			this._emailSender = emailSender;
			this._competitionDateRepository = competitionDateRepository;
		}

		public async Task SendConfirmationEmails(CompetitionRegistration registration, Entities.Competition competition)
		{
			var dates = await this._competitionDateRepository.GetByCompetition(competition.ID);
			
			var message1 = new CompetitionRegistrationPlayerConfirmationEmailMessage(competition, registration, dates);
			await this._emailSender.Send(message1);
			
			var message2 = new CompetitionRegistrationOrganiserConfirmationEmailMessage(competition, registration, dates);
			await this._emailSender.Send(message2);
		}

		public async Task SendSummaryEmail(List<CompetitionRegistration> registrations, Entities.Competition competition)
		{
			var dates = await this._competitionDateRepository.GetByCompetition(competition.ID);
			
			var message1 = new CompetitionRegistrationOrganiserSummaryEmailMessage(competition, registrations, dates);
			await this._emailSender.Send(message1);
		}
	}
}