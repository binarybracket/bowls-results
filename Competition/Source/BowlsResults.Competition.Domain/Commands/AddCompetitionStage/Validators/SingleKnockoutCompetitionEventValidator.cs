using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators
{
	public class SingleKnockoutCompetitionEventValidator : AbstractValidator<IList<EventTemplate>>
	{
		private readonly KnockoutEventTemplateValidator _knockoutEventTemplateValidator;

		public SingleKnockoutCompetitionEventValidator(KnockoutEventTemplateValidator knockoutEventTemplateValidator)
		{
			this._knockoutEventTemplateValidator = knockoutEventTemplateValidator;
			this.RuleFor(x => x.Count).Equal(1);
			this.RuleFor(x => x.OfType<KnockoutEventTemplate>().Count()).Equal(1).WithMessage("Only Knockout Event Supported");
		}

		public override ValidationResult Validate(ValidationContext<IList<EventTemplate>> context)
		{
			var result = base.Validate(context);

			if (result.IsValid)
			{
				foreach (var item in context.InstanceToValidate.OfType<KnockoutEventTemplate>())
				{
					ValidationResult innerResult = this._knockoutEventTemplateValidator.Validate(item);
					((List<ValidationFailure>) result.Errors).AddRange(innerResult.Errors);
				}
			}

			return result;
		}
	}
}