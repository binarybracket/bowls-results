using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common
{
	public class ResultsEngineResponse : IResultsEngineResponse
	{
		private readonly ValidationResult _validationResult;

		public ResultsEngineResponse()
		{
			this._validationResult = new ValidationResult();
			this.Status = ResultsEngineStatuses.UnknownError;
		}

		public ResultsEngineStatuses Status { get; set; }

		public ValidationResult ValidationResult
		{
			get { return this._validationResult; }
		}
	}
}