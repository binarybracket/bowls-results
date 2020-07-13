using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Fixture.Player;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Fixture.Player
{
	public class AddPlayerFixtureCommandHandler : ICommandHandler<AddPlayerFixtureCommand, DefaultIdentityCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<AddPlayerFixtureCommandHandler> _logger;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ICompetitionStageRepository _competitionStageRepository;
		private readonly ICompetitionEventRepository _competitionEventRepository;
		private readonly IPlayerCompetitionRoundRepository _playerCompetitionRoundRepository;
		private readonly IPlayerFixtureRepository _playerFixtureRepository;
		private readonly ICompetitionEntrantRepository _competitionEntrantRepository;

		private Entities.Competition _competition;
		private ValidationResult _validationResult;
		private CompetitionStage _competitionStage;
		private CompetitionEvent _competitionEvent;
		private PlayerCompetitionRound _round;
		private CompetitionEntrant _entrant1;
		private CompetitionEntrant _entrant2;
		private PlayerFixture _pendingFixture1;
		private PlayerFixture _pendingFixture2;

		public AddPlayerFixtureCommandHandler(
			IUnitOfWork unitOfWork,
			ILogger<AddPlayerFixtureCommandHandler> logger,
			ICompetitionRepository competitionRepository,
			ICompetitionStageRepository competitionStageRepository,
			ICompetitionEventRepository competitionEventRepository,
			IPlayerCompetitionRoundRepository playerCompetitionRoundRepository,
			IPlayerFixtureRepository playerFixtureRepository,
			ICompetitionEntrantRepository competitionEntrantRepository)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
			this._competitionRepository = competitionRepository;
			this._competitionStageRepository = competitionStageRepository;
			this._competitionEventRepository = competitionEventRepository;
			this._playerCompetitionRoundRepository = playerCompetitionRoundRepository;
			this._playerFixtureRepository = playerFixtureRepository;
			this._competitionEntrantRepository = competitionEntrantRepository;
			this._validationResult = new ValidationResult();
		}

		public async Task<DefaultIdentityCommandResponse> Handle(AddPlayerFixtureCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				PlayerFixture fixture = null;
				
				if (this._validationResult.IsValid)
				{
					await this.Load(command);
					this.Validate(command);
				}

				if (this._validationResult.IsValid)
				{
					fixture = this._round.CreateFixture(command.TotalLegs, command.Date);
					this.SetupFixture(fixture, command);

					await this._playerCompetitionRoundRepository.Save(this._round);
				}

				if (this._validationResult.IsValid)
				{
					this._unitOfWork.HardCommit();
					return DefaultIdentityCommandResponse.Create(this._validationResult, fixture.ID);
				}
				else
				{
					this._unitOfWork.Rollback();
					return DefaultIdentityCommandResponse.Create(this._validationResult);
				}
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				this._logger.LogCritical(e, nameof(AddPlayerFixtureCommandHandler));
				throw;
			}
		}

		private void Validate(AddPlayerFixtureCommand command)
		{
			if (this._round.Fixtures.Any(x => x.Reference == command.Reference))
			{
				this._validationResult.Errors.Add(new ValidationFailure("reference", "Fixture already exists with this reference."));
			}
			// TODO - may use this yet
			// if (this._entrant1 != null && this._entrant1.CompetitionEntrantStatusID != CompetitionEntrantStatuses.Confirmed)
			// {
			// 	this._validationResult.Errors.Add(new ValidationFailure("entrant1", "Entrant 1 has not been confirmed."));
			// }
			// if (this._entrant2 != null && this._entrant2.CompetitionEntrantStatusID != CompetitionEntrantStatuses.Confirmed)
			// {
			// 	this._validationResult.Errors.Add(new ValidationFailure("entrant2", "Entrant 2 has not been confirmed."));
			// }
		}

		private async Task Load(AddPlayerFixtureCommand command)
		{
			await this._competitionRepository.GetForUpdate(command.Competition.CompetitionID);
			this._competition = await this._competitionRepository.GetWithStages(command.Competition.CompetitionID);
			this._competitionStage = this._competition.GetStage(command.Competition.CompetitionStageLookupMode, command.Competition.CompetitionStageValue);
			this._competitionEvent = await this._competitionEventRepository.Get(command.CompetitionEventID);
			var rounds = await this._playerCompetitionRoundRepository.GetAll(this._competitionEvent.ID);
			await this.LoadRound(command, rounds);
			await this.LoadEntrants(command);
			await this.PendingFixtures(command);
		}

		private async Task PendingFixtures(AddPlayerFixtureCommand command)
		{
			if (command.Entrant1.Mode == PlayerFixtureEntrantConfigurationModel.PendingEntrantModes.Fixture)
			{
				if (command.Entrant1.FixtureID == null) throw new ArgumentNullException(nameof(command.Entrant1.FixtureID));
				this._pendingFixture1 = await this._playerFixtureRepository.Get(command.Entrant1.FixtureID.Value);
			}
			if (command.Entrant2.Mode == PlayerFixtureEntrantConfigurationModel.PendingEntrantModes.Fixture)
			{
				if (command.Entrant2.FixtureID == null) throw new ArgumentNullException(nameof(command.Entrant2.FixtureID));
				this._pendingFixture2 = await this._playerFixtureRepository.Get(command.Entrant2.FixtureID.Value);
			}
		}

		private async Task LoadEntrants(AddPlayerFixtureCommand command)
		{
			if (command.Entrant1.Mode == PlayerFixtureEntrantConfigurationModel.PendingEntrantModes.Entrant)
			{
				if (command.Entrant1.EntrantID == null) throw new ArgumentNullException(nameof(command.Entrant1.EntrantID));
				this._entrant1 = await this._competitionEntrantRepository.Get(command.Entrant1.EntrantID.Value);
			}
			if (command.Entrant2.Mode == PlayerFixtureEntrantConfigurationModel.PendingEntrantModes.Entrant)
			{
				if (command.Entrant2.EntrantID == null) throw new ArgumentNullException(nameof(command.Entrant2.EntrantID));
				this._entrant2 = await this._competitionEntrantRepository.Get(command.Entrant2.EntrantID.Value);
			}
		}

		private async Task LoadRound(AddPlayerFixtureCommand command, List<PlayerCompetitionRound> rounds)
		{
			if (this._competitionEvent is Knockout)
			{
				if (command.Round.GameNumber.HasValue)
				{
					this._round = rounds.Single(x => x.CompetitionRoundTypeID == command.Round.RoundType && x.GameNumber == command.Round.GameNumber.Value);
				}
				else
				{
					this._round = rounds.Single(x => x.CompetitionRoundTypeID == command.Round.RoundType);
				}
			}
			else
			{
				throw new NotImplementedException();
			}
		}
		
		private void SetupFixture(PlayerFixture fixture, AddPlayerFixtureCommand command)
		{
			if (this._pendingFixture1 != null)
			{
				fixture.SetPendingFixture1(this._pendingFixture1, command.Entrant1.FixtureResultType.Value);
			}
			if (this._pendingFixture2 != null)
			{
				fixture.SetPendingFixture2(this._pendingFixture2, command.Entrant2.FixtureResultType.Value);
			}
			if (this._entrant1 != null)
			{
				fixture.SetEntrant1(this._entrant1);
			}
			if (this._entrant2 != null)
			{
				fixture.SetEntrant2(this._entrant2);
			}
		}
	}
}