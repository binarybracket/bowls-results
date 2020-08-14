using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration
{
	public interface IRegistrationEmailManager
	{
		Task SendConfirmationEmails(CompetitionRegistration registration, Entities.Competition competition);

		Task SendSummaryEmail(List<CompetitionRegistration> registration, Entities.Competition competition);
	}
}