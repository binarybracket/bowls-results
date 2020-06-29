using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators
{
	public sealed class CompetitionRegistrationModelValidator : AbstractValidator<CompetitionRegistrationModel>
	{
		public CompetitionRegistrationModelValidator()
		{
			this.RuleFor(x => x.VerificationToken).NotEmpty();
			this.RuleFor(x => x.CompetitionID).NotEmpty();
			this.RuleFor(x => x.Contact).NotNull();
			this.RuleFor(x => x.Players).NotEmpty();

			this.When(x => x.Contact != null, () =>
			{
				this.RuleFor(x => x.Contact.Forename).NotEmpty().MaximumLength(50);
				this.RuleFor(x => x.Contact.Surname).NotEmpty().MaximumLength(50);
				this.RuleFor(x => x.Contact.Telephone).MaximumLength(50);
				this.RuleFor(x => x.Contact.EmailAddress).NotEmpty().MaximumLength(100).EmailAddress();
			});
		}
	}
}