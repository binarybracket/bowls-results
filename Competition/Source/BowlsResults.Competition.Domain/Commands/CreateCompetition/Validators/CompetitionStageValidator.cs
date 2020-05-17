using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public sealed class CompetitionStageValidator : AbstractValidator<CompetitionStageTemplate>
	{
		public CompetitionStageValidator()
		{
			this.RuleFor(x => x.CompetitionStageFormatID).NotEmpty();
			this.RuleFor(x => x.Name).NotEmpty();
			this.RuleFor(x => x.Sequence).NotEmpty();
		}
	}
}