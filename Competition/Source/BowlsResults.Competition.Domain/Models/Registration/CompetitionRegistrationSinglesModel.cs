using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration.Players;
using Com.BinaryBracket.Core.Domain2.reCAPTCHA;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration
{
	public abstract class CompetitionRegistrationModel : IRecaptchaTokenModel
	{
		public int CompetitionID { get; set; }
		public ContactModel Contact { get; set; }
		public PlayersRegistrationModel[] Players { get; set; }
		public string VerificationToken { get; set; }
	}

	public class SinglesCompetitionRegistrationModel : CompetitionRegistrationModel
	{
	}

	public class DoublesCompetitionRegistrationModel : CompetitionRegistrationModel
	{
	}

	public class TriplesCompetitionRegistrationModel : CompetitionRegistrationModel
	{
	}

	public class TeamCompetitionRegistrationModel : CompetitionRegistrationModel
	{
	}
}