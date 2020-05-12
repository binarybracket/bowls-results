using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public class SingleStageValidator : AbstractValidator<List<CompetitionStageTemplate>>
	{
		public SingleStageValidator()
		{
			this.RuleFor(x => x.Count).Equal(1);
		}
	}
}