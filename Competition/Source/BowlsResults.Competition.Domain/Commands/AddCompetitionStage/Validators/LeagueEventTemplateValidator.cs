using FluentValidation;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators
{
	public sealed class LeagueEventTemplateValidator : AbstractValidator<LeagueEventTemplate>
	{
		public LeagueEventTemplateValidator()
		{
			this.RuleFor(x => x.LeagueCalculationEngine).NotEmpty();
			this.RuleFor(x => x.Name).NotEmpty();
			this.RuleFor(x => x.Code).NotEmpty();
		}
	}
}