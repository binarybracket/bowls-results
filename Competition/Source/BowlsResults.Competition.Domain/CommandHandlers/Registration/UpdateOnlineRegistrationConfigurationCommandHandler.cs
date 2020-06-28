using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
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
		
		private ValidationResult _validationResult;
		private Entities.Competition _competition;
		private IContactRepository _contactRepository;


		public UpdateOnlineRegistrationConfigurationCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, ICompetitionRepository competitionRepository, ICompetitionRegistrationConfigurationRepository configurationRepository, IContactRepository contactRepository)
		{
			this._logger = loggerFactory.CreateLogger<UpdateOnlineRegistrationConfigurationCommandHandler>();
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._configurationRepository = configurationRepository;
			this._contactRepository = contactRepository;
		}
		
		public async Task<DefaultCommandResponse> Handle(UpdateOnlineRegistrationConfigurationCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				await this.Load(command);

				if (this._competition.RegistrationConfiguration == null)
				{
				//	this._competition.CreateRegistrationConfiguration(CompetitionRegistrationModes.Online, contact);
				}
				
			}
			catch (Exception exception)
			{
				this._unitOfWork.Rollback();
				this._logger.LogError(exception, "Exception In Command Handler");
				throw;
			}

			return null;
		}

		private async Task Load(UpdateOnlineRegistrationConfigurationCommand command)
		{
			this._competition = await this._competitionRepository.GetWithRegistrationConfiguration(command.CompetitionID);
			//await this.LoadContact();
		}
		
		
	}
}