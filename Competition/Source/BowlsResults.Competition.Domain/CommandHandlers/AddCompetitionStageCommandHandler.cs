using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage.Validators;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers
{
	public sealed class AddCompetitionStageCommandHandler : ICommandHandler<AddCompetitionStageCommand, DefaultCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly AddCompetitionStageCommandValidator _validator;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ICompetitionStageRepository _competitionStageRepository;
		private readonly ICompetitionEventRepository _competitionEventRepository;

		private Entities.Competition _competition;
		private ValidationResult _validationResult;

		public AddCompetitionStageCommandHandler(
			IUnitOfWork unitOfWork,
			ICompetitionRepository competitionRepository,
			ICompetitionStageRepository competitionStageRepository,
			ICompetitionEventRepository competitionEventRepository,
			AddCompetitionStageCommandValidator validator)
		{
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._competitionStageRepository = competitionStageRepository;
			this._competitionEventRepository = competitionEventRepository;
			this._validator = validator;
		}

		public async Task<DefaultCommandResponse> Handle(AddCompetitionStageCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command.CompetitionID);

					var stage = this._competition.CreateStage(command.CompetitionStageFormatID, command.Name, command.Sequence);
					await this._competitionStageRepository.Save(stage);

					foreach (var eventTemplate in command.Events)
					{
						if (eventTemplate is LeagueEventTemplate)
						{
							throw new NotImplementedException("League not yet supported");
						}

						if (eventTemplate is KnockoutEventTemplate)
						{
							var knockoutTemplate = eventTemplate as KnockoutEventTemplate;
							await this.CreateKnockout(stage, knockoutTemplate);
						}
					}
				}

				this._unitOfWork.HardCommit();
				return DefaultCommandResponse.Create(this._validationResult);
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task CreateKnockout(CompetitionStage stage, KnockoutEventTemplate knockoutEventTemplate)
		{
			var knockout = Knockout.Create(stage, knockoutEventTemplate.KnockoutCalculationEngine);
			await this._competitionEventRepository.Save(knockout);
		}

		private async Task Load(int competitionID)
		{
			this._competition = await this._competitionRepository.GetForUpdate(competitionID);
		}
	}
}