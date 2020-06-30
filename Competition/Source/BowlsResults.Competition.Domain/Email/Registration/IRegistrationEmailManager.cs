using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Messages;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Email;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration
{
	public interface IRegistrationEmailManager
	{
		Task SendConfirmationEmails(CompetitionRegistration registration);
	}

	public sealed class RegistrationEmailManager : IRegistrationEmailManager
	{
		private readonly IEmailSender _emailSender;

		public RegistrationEmailManager(IEmailSender emailSender)
		{
			this._emailSender = emailSender;
		}

		public async Task SendConfirmationEmails(CompetitionRegistration registration)
		{
			var message1 = new CompetitionRegistrationPlayerConfirmationEmailMessage(registration);
			await this._emailSender.Send(message1);
			
			var message2 = new CompetitionRegistrationOrganiserConfirmationEmailMessage(registration);
			await this._emailSender.Send(message2);
		}
	}
}