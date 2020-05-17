using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators
{
	public sealed class KnockoutEventTemplateValidator : AbstractValidator<KnockoutEventTemplate>
	{
		public KnockoutEventTemplateValidator()
		{
			this.RuleFor(x => x.KnockoutCalculationEngine).NotEmpty();
			this.RuleFor(x => x.Rounds).NotEmpty();
		}

		public override ValidationResult Validate(ValidationContext<KnockoutEventTemplate> context)
		{
			var result = base.Validate(context);

			if (result.IsValid)
			{
				if (!context.InstanceToValidate.Rounds.ContainsValue(CompetitionRoundTypes.Final))
				{
					result.Errors.Add(new ValidationFailure("Rounds", "There must be at least 'Final' round added."));
				}
			}

			return result;
		}
	}
}