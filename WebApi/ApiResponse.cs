using System;
using FluentValidation.Results;

namespace BowlsResults.WebApi
{
	[Serializable]
	public sealed class ApiResponse
	{
		public ApiResponse()
		{
			this.Timestamp = DateTime.UtcNow;
		}
		
		public ApiResponseStatuses Status { get; set; }

		public ValidationResult ValidationResult { get; set; }

		public object Result { get; set; }

		public DateTime Timestamp { get; set; }

		public static ApiResponse CreateSuccess(object result)
		{
			var response = new ApiResponse();
			response.Status = ApiResponseStatuses.Success;
			response.Result = result;
			return response;
		}
	}

	public enum ApiResponseStatuses
	{
		Success,
		Error
	}
}