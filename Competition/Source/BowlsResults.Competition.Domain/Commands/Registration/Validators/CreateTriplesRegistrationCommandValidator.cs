using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration.Players;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators
{
	public class CreateTriplesRegistrationCommandValidator : CommandValidator<CreateTriplesRegistrationCommand, DefaultCommandResponse>
	{
		public CreateTriplesRegistrationCommandValidator()
		{
			this.RuleFor(x => x.Registration).NotNull();
			this.RuleFor(x => x.Registration).SetValidator(new CompetitionRegistrationModelValidator());

			var validator = new InlineValidator<PlayersRegistrationModel>();
			var playerValidator = new PlayerRegistrationModelValidator();
			validator.RuleFor(x => x.Player1).NotNull();
			validator.RuleFor(x => x.Player1).SetValidator(playerValidator);
			validator.RuleFor(x => x.Player2).NotNull();
			validator.RuleFor(x => x.Player2).SetValidator(playerValidator);
			validator.RuleFor(x => x.Player3).NotNull();
			validator.RuleFor(x => x.Player3).SetValidator(playerValidator);
			validator.RuleFor(x => x.Player4).Null();
			this.RuleForEach(x => x.Registration.Players).SetValidator(validator);
		}
	}
}