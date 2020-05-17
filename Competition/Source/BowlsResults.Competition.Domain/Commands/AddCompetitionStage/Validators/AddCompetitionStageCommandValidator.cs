using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators
{
	public sealed class AddCompetitionStageCommandValidator : AbstractValidator<AddCompetitionStageCommand>
	{
		public AddCompetitionStageCommandValidator(SingleLeagueCompetitionEventValidator singleLeagueCompetitionEventValidator, SingleKnockoutCompetitionEventValidator singleKnockoutCompetitionEventValidator)
		{
			this.RuleFor(x => x.CompetitionID).NotEmpty();
			this.RuleFor(x => x.CompetitionStageFormatID).NotEmpty();
			this.RuleFor(x => x.Name).NotEmpty();
			this.RuleFor(x => x.Sequence).NotEmpty();
			
			
			this.RuleFor(command => command.CompetitionStageFormatID).NotEmpty().DependentRules(() =>
			{
				this.When(x => x.CompetitionStageFormatID == CompetitionStageFormats.SingleLeague, () =>
					{
						this.RuleFor(x => x.Events).SetValidator(singleLeagueCompetitionEventValidator);
					}
				)
					.Otherwise(() =>
				{
					this.When(x => x.CompetitionStageFormatID == CompetitionStageFormats.SingleKnockout, () =>
						{
							this.RuleFor(x => x.Events).SetValidator(singleKnockoutCompetitionEventValidator);
						});
						//.Otherwise(() => { this.RuleFor(x => x.Stages).NotEmpty(); });
				});
			});
		}
	}
}