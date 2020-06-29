using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration
{
	public sealed class CreateSinglesRegistrationCommand : ICommand<DefaultCommandResponse>
	{
		public CompetitionRegistrationModel Registration { get; set; }
	}
}