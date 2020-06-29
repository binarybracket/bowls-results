using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Helpers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration
{
	public sealed class CreateTriplesRegistrationCommandHandler : ICommandHandler<CreateTriplesRegistrationCommand, DefaultCommandResponse>
	{
		private readonly CreateTriplesRegistrationCommandValidator _validator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ILogger<CreateTriplesRegistrationCommandHandler> _logger;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;

		private ValidationResult _validationResult;
		private Entities.Competition _competition;


		public CreateTriplesRegistrationCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, CreateTriplesRegistrationCommandValidator validator, ICompetitionRepository competitionRepository,
			ICompetitionRegistrationRepository competitionRegistrationRepository)
		{
			this._logger = loggerFactory.CreateLogger<CreateTriplesRegistrationCommandHandler>();
			this._validator = validator;
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
		}

		public async Task<DefaultCommandResponse> Handle(CreateTriplesRegistrationCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command);

					RegistrationValidatorHelper.Validate(this._validationResult, this._competition);
				}

				if (this._validationResult.IsValid)
				{
					var registration = this._competition.CreateRegistration(command.Registration.Contact.Forename, command.Registration.Contact.Surname, command.Registration.Contact.EmailAddress);

					foreach (var player in command.Registration.Players)
					{
						var entrant = registration.CreateEntrant();
						entrant.CreatePlayer(player.Player1.Forename, player.Player1.Surname);
						entrant.CreatePlayer(player.Player2.Forename, player.Player2.Surname);
						entrant.CreatePlayer(player.Player3.Forename, player.Player3.Surname);
					}

					await this._competitionRegistrationRepository.Save(registration);
				}

				if (this._validationResult.IsValid)
				{
					this._unitOfWork.SoftCommit();
					return DefaultCommandResponse.Create(this._validationResult);
				}
				else
				{
					this._unitOfWork.Rollback();
					return DefaultCommandResponse.Create(this._validationResult);
				}
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task Load(CreateTriplesRegistrationCommand command)
		{
			this._competition = await this._competitionRepository.GetForUpdate(command.Registration.CompetitionID);
			this._competition = await this._competitionRepository.GetWithRegistrationConfiguration(this._competition.ID);
		}
	}
}