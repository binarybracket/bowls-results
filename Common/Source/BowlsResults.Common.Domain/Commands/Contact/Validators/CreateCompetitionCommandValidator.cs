using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Commands.Contact.Validators
{
	public sealed class CreateContactCommandValidator : CommandValidator<CreateContactCommand, DefaultIdentityCommandResponse>
	{
		public CreateContactCommandValidator()
		{
			this.RuleFor(command => command.ContactTypeID).NotEmpty();
			this.RuleFor(command => command.Forename).NotEmpty().MaximumLength(50);
			this.RuleFor(command => command.Surname).NotEmpty().MaximumLength(50);
			this.RuleFor(command => command.EmailAddress).NotEmpty().MaximumLength(100).EmailAddress();
			this.RuleFor(command => command.Telephone).MaximumLength(50);
		}
	}
}