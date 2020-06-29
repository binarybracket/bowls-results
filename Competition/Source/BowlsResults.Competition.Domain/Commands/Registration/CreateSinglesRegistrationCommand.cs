using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration
{
	public sealed class CreateSinglesRegistrationCommand : ICommand<DefaultCommandResponse>
	{
		public CompetitionRegistrationModel Registration { get; set; }
	}
	public sealed class CreateDoublesRegistrationCommand : ICommand<DefaultCommandResponse>
	{
		public CompetitionRegistrationModel Registration { get; set; }
	}
	public sealed class CreateTriplesRegistrationCommand : ICommand<DefaultCommandResponse>
	{
		public CompetitionRegistrationModel Registration { get; set; }
	}
	public sealed class CreateTeamRegistrationCommand : ICommand<DefaultCommandResponse>
	{
		public CompetitionRegistrationModel Registration { get; set; }
	}
}