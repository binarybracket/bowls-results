using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Messages;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Email;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration
{
	public interface IRegistrationEmailManager
	{
		Task SendConfirmationEmails(CompetitionRegistration registration, Entities.Competition competition);
	}

	public sealed class RegistrationEmailManager : IRegistrationEmailManager
	{
		private readonly IEmailSender _emailSender;

		public RegistrationEmailManager(IEmailSender emailSender)
		{
			this._emailSender = emailSender;
		}

		public async Task SendConfirmationEmails(CompetitionRegistration registration, Entities.Competition competition)
		{
			var message1 = new CompetitionRegistrationPlayerConfirmationEmailMessage(competition, registration);
			await this._emailSender.Send(message1);
			
			var message2 = new CompetitionRegistrationOrganiserConfirmationEmailMessage(competition, registration);
			await this._emailSender.Send(message2);
		}
	}
}