using System;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public sealed class CreateCompetitionCommandValidator : CommandValidator<CreateCompetitionCommand, DefaultCommandResponse>
	{
		public CreateCompetitionCommandValidator()
		{
			RuleFor(command => command.CompetitionHeaderID).NotEmpty();
			RuleFor(command => command.SeasonID).NotEmpty();
			RuleFor(command => command.Name).NotEmpty();
			RuleFor(command => command.Organiser).NotEmpty();
			RuleFor(command => command.AgeGroup).NotEmpty();
			RuleFor(command => command.Gender).NotEmpty();
			RuleFor(command => command.Scope).NotEmpty();
			RuleFor(command => command.StartDate).NotEmpty();
			RuleFor(command => command.EndDate).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().LessThan(x=>x.StartDate).When(x=>x.StartDate > DateTime.MinValue);
			RuleFor(command => command.PlayerMeritCalculationEngine).Null();
//			RuleFor(command => command.Stages).SetValidator();
		}
	}
}