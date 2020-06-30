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

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration
{
	public sealed class CreateSinglesRegistrationCommandHandler : ICommandHandler<CreateSinglesRegistrationCommand, DefaultCommandResponse>
	{
		private readonly CreateSinglesRegistrationCommandValidator _validator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ILogger<CreateSinglesRegistrationCommandHandler> _logger;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;
		private readonly IRecaptchaService _recaptchaService;
		private readonly IRegistrationEmailManager _registrationEmailManager;
		private readonly IEmailSender _emailSender;

		private ValidationResult _validationResult;
		private Entities.Competition _competition;

		public CreateSinglesRegistrationCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, CreateSinglesRegistrationCommandValidator validator, ICompetitionRepository competitionRepository,
			ICompetitionRegistrationRepository competitionRegistrationRepository, IRecaptchaService recaptchaService, IRegistrationEmailManager registrationEmailManager)
		{
			this._logger = loggerFactory.CreateLogger<CreateSinglesRegistrationCommandHandler>();
			this._validator = validator;
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
			this._recaptchaService = recaptchaService;
			this._registrationEmailManager = registrationEmailManager;
		}

		public async Task<DefaultCommandResponse> Handle(CreateSinglesRegistrationCommand command)
		{
			this._unitOfWork.Begin();

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
					RegistrationValidatorHelper.ValidateGameFormat(this._validationResult, this._competition, GameFormats.Singles);
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
						var entrant = registration.CreateEntrant();
						entrant.CreatePlayer(player.Player1.Forename, player.Player1.Surname);
					}

					await this._competitionRegistrationRepository.Save(registration);
				}

				if (this._validationResult.IsValid)
				{
					this._unitOfWork.SoftCommit();

					await this._registrationEmailManager.SendConfirmationEmails(registration);

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

		private async Task Load(CreateSinglesRegistrationCommand command)
		{
			this._competition = await this._competitionRepository.GetForUpdate(command.Registration.CompetitionID);
			this._competition = await this._competitionRepository.GetWithRegistrationConfiguration(this._competition.ID);
		}
	}
}