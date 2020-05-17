using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators
{
	public class SingleStageValidator : CompetitionStagesValidator
	{
		public SingleStageValidator(CompetitionStageValidator stageValidator) : base(stageValidator)
		{
			this.RuleFor(x => x.Count).Equal(1);
		}

		public override ValidationResult Validate(ValidationContext<List<CompetitionStageTemplate>> context)
		{
			var result = base.Validate(context);

			if (context.InstanceToValidate.Any(x => x.CompetitionStageFormatID == CompetitionStageFormats.Groups))
			{
				result.Errors.Add(new ValidationFailure("Stages", "Groups can only be used on Multi Stage Competitions"));
			}

			return result;
		}
	}
}