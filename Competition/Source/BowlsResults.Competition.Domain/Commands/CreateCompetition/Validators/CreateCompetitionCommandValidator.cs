using System;
using System.Xml.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Validators;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public interface ICreateCompetitionCommandValidator : IValidator<CreateCompetitionCommand>
	{
	}

	public sealed class CreateCompetitionCommandValidator : CommandValidator<CreateCompetitionCommand, DefaultCommandResponse>, ICreateCompetitionCommandValidator
	{
		private SingleStageValidator _singleStageValidator;
		private MultiStageValidator _multiStageValidator;

		public CreateCompetitionCommandValidator(SingleStageValidator singleStageValidator, MultiStageValidator multiStageValidator)
		{
			this._singleStageValidator = singleStageValidator;
			this._multiStageValidator = multiStageValidator;

			this.RuleFor(command => command.CompetitionHeaderID).NotEmpty();
			this.RuleFor(command => command.SeasonID).NotEmpty();
			this.RuleFor(command => command.Name).NotEmpty();
			this.RuleFor(command => command.Organiser).NotEmpty();
			this.RuleFor(command => command.AgeGroup).NotEmpty();
			this.RuleFor(command => command.Gender).NotEmpty();			
			this.RuleFor(command => command.Scope).NotEmpty();
			this.RuleFor(command => command.StartDate).NotEmpty();
			this.RuleFor(command => command.EndDate).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().LessThan(x => x.StartDate).When(x => x.StartDate > DateTime.MinValue);
			this.RuleFor(command => command.PlayerMeritCalculationEngine).Null();

			this.RuleFor(command => command.Format).NotEmpty().DependentRules(() =>
			{
				this.When(x => x.Format == CompetitionFormats.SingleStage, () =>
					{
						this.RuleFor(x => x.Stages).SetValidator(this._singleStageValidator);
					}
				).Otherwise(() =>
				{
					this.When(x => x.Format == CompetitionFormats.MultipleStages, () =>
						{
							this.RuleFor(x => x.Stages).SetValidator(this._multiStageValidator);
						})
						.Otherwise(() => { this.RuleFor(x => x.Stages).NotEmpty(); });
				});
			});
		}
	}
}