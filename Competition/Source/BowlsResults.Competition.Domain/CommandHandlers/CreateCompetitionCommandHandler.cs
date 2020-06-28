using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition.Validators;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers
{
	public sealed class CreateCompetitionCommandHandler : ICommandHandler<CreateCompetitionCommand, DefaultIdentityCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionHeaderRepository _competitionHeaderRepository;
		private readonly CreateCompetitionCommandValidator _validator;
		private readonly ISeasonRepository _seasonRepository;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly IGameVariationRepository _gameVariationRepository;
		private readonly IClubRepository _clubRepository;

		private CompetitionHeader _header;
		private Season _season;
		private ValidationResult _validationResult;
		private Entities.Competition _competition;
		private GameVariation _gameVariation;
		private Club _venueClub;
		private Club _organiserClub;

		public CreateCompetitionCommandHandler(IUnitOfWork unitOfWork, ISeasonRepository seasonRepository, ICompetitionHeaderRepository competitionHeaderRepository, ICompetitionRepository competitionRepository,
			IGameVariationRepository gameVariationRepository, CreateCompetitionCommandValidator validator, IClubRepository clubRepository)
		{
			this._unitOfWork = unitOfWork;
			this._competitionHeaderRepository = competitionHeaderRepository;
			this._competitionRepository = competitionRepository;
			this._seasonRepository = seasonRepository;
			this._gameVariationRepository = gameVariationRepository;
			this._validator = validator;
			this._clubRepository = clubRepository;
		}

		public async Task<DefaultIdentityCommandResponse> Handle(CreateCompetitionCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command);
					this.ValidateAssociation(command.AssociationID);
				}

				if (this._validationResult.IsValid)
				{
					await this.SaveCompetition(command);
					this._unitOfWork.SoftCommit();
					return DefaultIdentityCommandResponse.Create(this._validationResult, this._competition.ID);
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
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task SaveCompetition(CreateCompetitionCommand command)
		{
			this._competition = Entities.Competition.Create(
				this._header,
				this._season,
				command.Organiser,
				command.Scope,
				command.Format,
				command.AgeGroup,
				command.Gender,
				command.AssociationID,
				command.Name,
				command.StartDate,
				command.EndDate);
			
			this._competition.Sponsor = command.Sponsor;
			this._competition.OrganisingClub = this._organiserClub;
			this._competition.VenueClub = this._venueClub;
			this._competition.GameVariation = this._gameVariation;

			this._competition.SetAuditFields();

			await this._competitionRepository.Save(this._competition);
		}

		private void ValidateAssociation(int associationID)
		{
			if (this._header.AssociationID != associationID)
			{
				this._validationResult.Errors.Add(new ValidationFailure(nameof(associationID), "Association does not match"));
			}
		}

		private async Task Load(CreateCompetitionCommand command)
		{
			this._header = await this._competitionHeaderRepository.Get(command.CompetitionHeaderID);
			this._season = await this._seasonRepository.Get(command.SeasonID);
			if (command.GameVariationID.HasValue)
			{
				this._gameVariation = await this._gameVariationRepository.Get(command.GameVariationID.Value);
			}

			if (command.Organiser == CompetitionOrganisers.Association)
			{
				this._venueClub = await this._clubRepository.Get(command.VenueClubID.Value);
			}

			if (command.Organiser == CompetitionOrganisers.Club)
			{
				this._organiserClub = await this._clubRepository.Get(command.OrganiserClubID.Value);
				this._venueClub = this._organiserClub;
			}
		}
	}
}