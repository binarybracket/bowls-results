using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration.Players;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators
{
	public class CreateSinglesRegistrationCommandValidator : CommandValidator<CreateSinglesRegistrationCommand, DefaultCommandResponse>
	{
		public CreateSinglesRegistrationCommandValidator()
		{
			this.RuleFor(x => x.Registration.CompetitionID).NotEmpty();
			
			this.RuleFor(x => x.Registration).NotNull();
			this.RuleFor(x => x.Registration.Contact).NotNull();
			this.RuleFor(x => x.Registration.Players).NotEmpty();


			this.When(x => x.Registration.Contact != null, () =>
			{
				this.RuleFor(x => x.Registration.Contact.Forename).NotEmpty().MaximumLength(50);
				this.RuleFor(x => x.Registration.Contact.Surname).NotEmpty().MaximumLength(50);
				this.RuleFor(x => x.Registration.Contact.Telephone).MaximumLength(50);
				this.RuleFor(x => x.Registration.Contact.EmailAddress).NotEmpty().MaximumLength(100).EmailAddress();
			});

			var validator = new InlineValidator<PlayersRegistrationModel>();
			var playerValidator = new PlayerRegistrationModelValidator();
			validator.RuleFor(x => x.Player1).NotNull();
			validator.RuleFor(x => x.Player1).SetValidator(playerValidator);
			this.RuleForEach(x => x.Registration.Players).SetValidator(validator);
		}
	}

	public sealed class PlayerRegistrationModelValidator : AbstractValidator<PlayerRegistrationModel>
	{
		public PlayerRegistrationModelValidator()
		{
			this.RuleFor(x => x.Forename).NotEmpty().MaximumLength(50);
			this.RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
		}
	}
}