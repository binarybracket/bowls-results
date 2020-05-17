using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public abstract class CompetitionStagesValidator : AbstractValidator<List<CompetitionStageTemplate>>
	{
		protected CompetitionStagesValidator(CompetitionStageValidator stageValidator)
		{
			this.RuleForEach(x => x).SetValidator(stageValidator);
		}
	}
}