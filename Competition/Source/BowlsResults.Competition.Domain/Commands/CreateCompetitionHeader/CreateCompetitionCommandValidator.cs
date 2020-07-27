using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetitionHeader
{
	public sealed class CreateCompetitionHeaderCommandValidator : CommandValidator<CreateCompetitionHeaderCommand, DefaultIdentityCommandResponse>
	{
		public CreateCompetitionHeaderCommandValidator()
		{
			this.RuleFor(command => command.Name).NotEmpty().MaximumLength(100);
			this.RuleFor(command => command.ShortName).NotEmpty().MaximumLength(5);
			this.RuleFor(command => command.Priority).NotEmpty().GreaterThan(0);
			this.RuleFor(command => command.AssociationID).NotEmpty();
		}
	}
}