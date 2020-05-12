using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public class MultiStageValidator : AbstractValidator<List<CompetitionStageTemplate>>
	{
		public MultiStageValidator()
		{
			this.RuleFor(x => x.Count).GreaterThan(0);
		}
	}
}