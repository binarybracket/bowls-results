using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition.Registration
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition/registration")]
	public class CompetitionRegistrationController
	{
		private CreateSinglesRegistrationCommandHandler _commandHandler;

		public CompetitionRegistrationController(CreateSinglesRegistrationCommandHandler commandHandler)
		{
			this._commandHandler = commandHandler;
		}
		
		[Route("singles")]
		[HttpPost]
		public async Task<ValidationResult> Post([FromBody] SinglesCompetitionRegistrationModel data)
		{

			var command = new CreateSinglesRegistrationCommand();
			command.Registration = data;

			var response = await this._commandHandler.Handle(command);

			return response.ValidationResult;
		}
		
		[Route("doubles")]
		[HttpPost]
		public async void Post([FromBody] DoublesCompetitionRegistrationModel data)
		{
		}
		
		[Route("triples")]
		[HttpPost]
		public async void Post([FromBody] TriplesCompetitionRegistrationModel data)
		{
		}
		
		[Route("team")]
		[HttpPost]
		public async void Post([FromBody] TeamCompetitionRegistrationModel data)
		{
		}
	}
}