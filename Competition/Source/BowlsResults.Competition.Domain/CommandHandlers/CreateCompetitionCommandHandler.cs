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

		private CompetitionHeader _header;
		private Season _season;
		private ValidationResult _validationResult;
		private Entities.Competition _competition;

		public CreateCompetitionCommandHandler(IUnitOfWork unitOfWork, ISeasonRepository seasonRepository, ICompetitionHeaderRepository competitionHeaderRepository, ICompetitionRepository competitionRepository,
			CreateCompetitionCommandValidator validator)
		{
			this._unitOfWork = unitOfWork;
			this._competitionHeaderRepository = competitionHeaderRepository;
			this._competitionRepository = competitionRepository;
			this._seasonRepository = seasonRepository;
			this._validator = validator;
		}

		public async Task<DefaultIdentityCommandResponse> Handle(CreateCompetitionCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command.CompetitionHeaderID, command.SeasonID);
					this.ValidateAssociation(command.AssociationID);
				}

				if (this._validationResult.IsValid)
				{
					await this.SaveCompetition(command);
				}

				this._unitOfWork.HardCommit();
				return DefaultIdentityCommandResponse.Create(this._validationResult, this._competition.ID);
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

		private async Task Load(int competitionHeaderID, short SeasonID)
		{
			this._header = await this._competitionHeaderRepository.Get(competitionHeaderID);
			this._season = await this._seasonRepository.Get(SeasonID);
		}
	}
}