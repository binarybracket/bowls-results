using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
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
	public class UpdateOnlineRegistrationConfigurationCommandHandler : ICommandHandler<UpdateOnlineRegistrationConfigurationCommand, DefaultCommandResponse>
	{
		private readonly ILogger<UpdateOnlineRegistrationConfigurationCommandHandler> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionRegistrationConfigurationRepository _configurationRepository;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly IClubRepository _clubRepository;
		private readonly IContactRepository _contactRepository;
		private readonly ValidationResult _validationResult;
		
		private Entities.Competition _competition;
		private Association _association;
		private Club _club;
		private Contact _organiserContact;

		public UpdateOnlineRegistrationConfigurationCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, ICompetitionRepository competitionRepository, ICompetitionRegistrationConfigurationRepository configurationRepository,
			IContactRepository contactRepository, IAssociationRepository associationRepository, IClubRepository clubRepository)
		{
			this._logger = loggerFactory.CreateLogger<UpdateOnlineRegistrationConfigurationCommandHandler>();
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._configurationRepository = configurationRepository;
			this._contactRepository = contactRepository;
			this._associationRepository = associationRepository;
			this._clubRepository = clubRepository;
			this._validationResult = new ValidationResult();
		}

		public async Task<DefaultCommandResponse> Handle(UpdateOnlineRegistrationConfigurationCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				await this.Load(command);
				this.CheckOrganiserContact();

				if (this._competition.RegistrationConfiguration == null)
				{
					this._competition.CreateRegistrationConfiguration(CompetitionRegistrationModes.Online, this._organiserContact);										
				}
				this._competition.RegistrationConfiguration.OpenDate = DateHelper.GenerateOpenDate(this._competition.StartDate, command.OpenDate);
				this._competition.RegistrationConfiguration.CloseDate = DateHelper.GenerateCloseDate(this._competition.StartDate, command.CloseDate);
				this._competition.RegistrationConfiguration.Amount = command.Amount;

				await this._competitionRepository.Save(this._competition);
				
				this._unitOfWork.SoftCommit();
			}
			catch (Exception exception)
			{
				this._unitOfWork.Rollback();
				this._logger.LogError(exception, "Exception In Command Handler");
				throw;
			}

			return DefaultCommandResponse.Create(this._validationResult);
		}

		private async Task Load(UpdateOnlineRegistrationConfigurationCommand command)
		{
			await this._competitionRepository.GetForUpdate(command.CompetitionID);
			this._competition = await this._competitionRepository.GetWithRegistrationConfiguration(command.CompetitionID);
			this._association = await this._associationRepository.GetWithContacts(this._competition.AssociationID);

			if (this._competition.CompetitionOrganiserID == CompetitionOrganisers.Club)
			{
				this._club = await this._clubRepository.GetWithContacts(this._competition.OrganisingClub.ID);
			}

			if (command.OrganiserContactID.HasValue)
			{
				this._organiserContact = await this._contactRepository.Get(command.OrganiserContactID.Value);
			}
		}

		private void CheckOrganiserContact()
		{
			if (this._organiserContact == null)
			{
				switch (this._competition.CompetitionOrganiserID)
				{
					case CompetitionOrganisers.Club:
						this._organiserContact = this._club.GetContactByType(ContactTypes.CompetitionSecretary);
						break;
					case CompetitionOrganisers.Association:
						this._organiserContact = this._association.GetContactByType(ContactTypes.CompetitionSecretary);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}