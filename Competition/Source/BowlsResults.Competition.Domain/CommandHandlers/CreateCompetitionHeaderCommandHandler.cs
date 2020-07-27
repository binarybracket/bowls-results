using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetitionHeader;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers
{
	public class CreateCompetitionHeaderCommandHandler : ICommandHandler<CreateCompetitionHeaderCommand, DefaultIdentityCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<CreateCompetitionHeaderCommandHandler> _logger;
		private readonly CreateCompetitionHeaderCommandValidator _validator;
		private readonly ICompetitionHeaderRepository _competitionHeaderRepository;

		private ValidationResult _validationResult;
		
		public CreateCompetitionHeaderCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateCompetitionHeaderCommandHandler> logger, CreateCompetitionHeaderCommandValidator validator, ICompetitionHeaderRepository competitionHeaderRepository)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
			this._validator = validator;
			this._competitionHeaderRepository = competitionHeaderRepository;
		}

		public async Task<DefaultIdentityCommandResponse> Handle(CreateCompetitionHeaderCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = await this._validator.ValidateAsync(command);

				if (this._validationResult.IsValid)
				{
					var data = new CompetitionHeader
					{
						Name = command.Name,
						ShortName = command.ShortName,
						Priority = command.Priority,
						AssociationID = command.AssociationID
					};

					await this._competitionHeaderRepository.Save(data);
					
					this._unitOfWork.SoftCommit();
					return DefaultIdentityCommandResponse.Create(this._validationResult, data.ID);
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
	}
}