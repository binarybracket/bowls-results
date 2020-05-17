using System;
using System.Xml.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public sealed class CreateCompetitionCommandValidator : CommandValidator<CreateCompetitionCommand, DefaultCommandResponse>
	{
		public CreateCompetitionCommandValidator(SingleStageValidator singleStageValidator, MultiStageValidator multiStageValidator)
		{
			this.RuleFor(command => command.CompetitionHeaderID).NotEmpty();
			this.RuleFor(command => command.SeasonID).NotEmpty();
			this.RuleFor(command => command.Name).NotEmpty();
			this.RuleFor(command => command.Organiser).NotEmpty();
			this.RuleFor(command => command.AgeGroup).NotEmpty();
			this.RuleFor(command => command.Gender).NotEmpty();			
			this.RuleFor(command => command.Format).NotEmpty();
			this.RuleFor(command => command.Scope).NotEmpty();
			this.RuleFor(command => command.StartDate).NotEmpty();
			this.RuleFor(command => command.EndDate)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.GreaterThanOrEqualTo(x => x.StartDate)
				.When(x => x.EndDate.HasValue && x.StartDate != default(DateTime));
			this.RuleFor(command => command.PlayerMeritCalculationEngine).Null();
		}
	}
}