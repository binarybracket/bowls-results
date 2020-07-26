using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Helpers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.Email;
using Com.BinaryBracket.Core.Domain2.reCAPTCHA;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration
{
	public sealed class CreateTriplesRegistrationCommandHandler : ICommandHandler<CreateTriplesRegistrationCommand, DefaultCommandResponse>
	{
		private readonly CreateTriplesRegistrationCommandValidator _validator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRegistrationUnitOfWork _registrationUnitOfWork;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ILogger<CreateTriplesRegistrationCommandHandler> _logger;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;
		private readonly IRecaptchaService _recaptchaService;
		private readonly IRegistrationEmailManager _registrationEmailManager;
		private readonly ICompetitionRegistrationAttemptRepository _competitionRegistrationAttemptRepository;

		private ValidationResult _validationResult;
		private Entities.Competition _competition;

		public CreateTriplesRegistrationCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IRegistrationUnitOfWork registrationUnitOfWork, CreateTriplesRegistrationCommandValidator validator, ICompetitionRepository competitionRepository,
			ICompetitionRegistrationRepository competitionRegistrationRepository, IRecaptchaService recaptchaService, IRegistrationEmailManager registrationEmailManager, ICompetitionRegistrationAttemptRepository competitionRegistrationAttemptRepository)
		{
			this._logger = loggerFactory.CreateLogger<CreateTriplesRegistrationCommandHandler>();
			this._validator = validator;
			this._unitOfWork = unitOfWork;
			this._registrationUnitOfWork = registrationUnitOfWork;
			this._competitionRepository = competitionRepository;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
			this._recaptchaService = recaptchaService;
			this._registrationEmailManager = registrationEmailManager;
			this._competitionRegistrationAttemptRepository = competitionRegistrationAttemptRepository;
		}

		public async Task<DefaultCommandResponse> Handle(CreateTriplesRegistrationCommand command)
		{
			await this.StoreAttempt(command);
			
			this._unitOfWork.Begin();
			this._registrationUnitOfWork.Begin();

			try
			{
				CompetitionRegistration registration = null;
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command);

					RegistrationValidatorHelper.Validate(this._validationResult, this._competition);
				}

				if (this._validationResult.IsValid)
				{
					RegistrationValidatorHelper.ValidateGameFormat(this._validationResult, this._competition, GameFormats.Threesomes);
				}

				if (this._validationResult.IsValid)
				{
					await this._recaptchaService.Validate(command.Registration, "opens/registration", this._validationResult);
				}

				if (this._validationResult.IsValid)
				{
					registration = this._competition.CreateRegistration(command.Registration.Contact.Forename, command.Registration.Contact.Surname, command.Registration.Contact.EmailAddress);

					foreach (var player in command.Registration.Players)
					{
						var entrant = registration.CreateEntrant(this._competition);
						entrant.CreatePlayer(player.Player1.Forename, player.Player1.Surname);
						entrant.CreatePlayer(player.Player2.Forename, player.Player2.Surname);
						entrant.CreatePlayer(player.Player3.Forename, player.Player3.Surname);
					}

					await this._competitionRegistrationRepository.Save(registration);
				}

				if (this._validationResult.IsValid)
				{
					this._unitOfWork.SoftCommit();
					this._registrationUnitOfWork.SoftCommit();

					await this._registrationEmailManager.SendConfirmationEmails(registration, this._competition);

					return DefaultCommandResponse.Create(this._validationResult);
				}
				else
				{
					this._unitOfWork.Rollback();
					this._registrationUnitOfWork.Rollback();
					return DefaultCommandResponse.Create(this._validationResult);
				}
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				this._registrationUnitOfWork.Rollback();
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task StoreAttempt(CreateTriplesRegistrationCommand command)
		{
			try
			{
				string x = JsonConvert.SerializeObject(command);
				
				var data = new CompetitionRegistrationAttempt();
				data.Data = x;

				await this._competitionRegistrationAttemptRepository.Save(data);
			}
			catch (Exception e)
			{
				this._logger.LogError(e, "Store Attempt Failed");
			}
		}
		
		private async Task Load(CreateTriplesRegistrationCommand command)
		{
			this._competition = await this._competitionRepository.GetForUpdate(command.Registration.CompetitionID);
			this._competition = await this._competitionRepository.GetWithRegistrationConfiguration(this._competition.ID);
		}
	}
}