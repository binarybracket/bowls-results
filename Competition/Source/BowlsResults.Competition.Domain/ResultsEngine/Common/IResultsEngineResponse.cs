using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common
{
	public interface IResultsEngineResponse
	{
		ResultsEngineStatuses Status { get; set; }
		ValidationResult ValidationResult { get; }
	}
}