using System;
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
		private readonly CreateSinglesRegistrationCommandHandler _singlesRegistrationCommandHandler;
		private readonly CreateDoublesRegistrationCommandHandler _doublesRegistrationCommandHandler;
		private readonly CreateTriplesRegistrationCommandHandler _triplesRegistrationCommandHandler;

		public CompetitionRegistrationController(
			CreateSinglesRegistrationCommandHandler singlesRegistrationCommandHandler,
			CreateDoublesRegistrationCommandHandler doublesRegistrationCommandHandler,
			CreateTriplesRegistrationCommandHandler triplesRegistrationCommandHandler)
		{
			this._singlesRegistrationCommandHandler = singlesRegistrationCommandHandler;
			this._doublesRegistrationCommandHandler = doublesRegistrationCommandHandler;
			this._triplesRegistrationCommandHandler = triplesRegistrationCommandHandler;
		}

		[Route("singles")]
		[HttpPost]
		public async Task<ValidationResult> Post([FromBody] SinglesCompetitionRegistrationModel data)
		{
			var command = new CreateSinglesRegistrationCommand();
			command.Registration = data;

			var response = await this._singlesRegistrationCommandHandler.Handle(command);

			return response.ValidationResult;
		}

		[Route("doubles")]
		[HttpPost]
		public async Task<ValidationResult> Post([FromBody] DoublesCompetitionRegistrationModel data)
		{
			var command = new CreateDoublesRegistrationCommand();
			command.Registration = data;

			var response = await this._doublesRegistrationCommandHandler.Handle(command);

			return response.ValidationResult;
		}

		[Route("triples")]
		[HttpPost]
		public async Task<ValidationResult> Post([FromBody] TriplesCompetitionRegistrationModel data)
		{
			var command = new CreateTriplesRegistrationCommand();
			command.Registration = data;

			var response = await this._triplesRegistrationCommandHandler.Handle(command);

			return response.ValidationResult;
		}

		[Route("team")]
		[HttpPost]
		public async Task<ValidationResult> Post([FromBody] TeamCompetitionRegistrationModel data)
		{
			throw new NotImplementedException();
		}
	}
}