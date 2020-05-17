using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators
{
//	public class MultiStageValidator : CompetitionStagesValidator
//	{
//		public MultiStageValidator(CompetitionStageValidator stageValidator) : base(stageValidator)
//		{
//			this.RuleFor(x => x.Count).GreaterThan(0);
//		}
//		
//		public override ValidationResult Validate(ValidationContext<List<CompetitionStageTemplate>> context)
//		{
//			var result = base.Validate(context);
//
//			var anyDups = context.InstanceToValidate.GroupBy(x => x.Sequence).Any(g => g.Count() > 1);
//			if (anyDups)
//			{
//				result.Errors.Add(new ValidationFailure("Sequence", "'Sequence' must be unique for all competition stages."));
//			}
//
//			return result;
//		}
//	}
}