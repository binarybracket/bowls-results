using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration.Players;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators
{
	public sealed class PlayerRegistrationModelValidator : AbstractValidator<PlayerRegistrationModel>
	{
		public PlayerRegistrationModelValidator()
		{
			this.RuleFor(x => x.Forename).NotEmpty().MaximumLength(50);
			this.RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
		}
	}
}