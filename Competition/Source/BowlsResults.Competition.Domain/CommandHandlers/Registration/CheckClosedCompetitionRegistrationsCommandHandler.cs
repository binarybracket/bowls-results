using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CheckClosedCompetitionRegistrations;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using Com.BinaryBracket.Core.Domain2.reCAPTCHA;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration
{
	public sealed class CheckClosedCompetitionRegistrationsCommandHandler : ICommandHandler<CheckClosedCompetitionRegistrationsCommand, DefaultCommandResponse>
	{
		private readonly ILogger<CheckClosedCompetitionRegistrationsCommandHandler> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRegistrationUnitOfWork _registrationUnitOfWork;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ICompetitionRegistrationSummaryRepository _competitionRegistrationSummaryRepository;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;
		private readonly IRegistrationEmailManager _registrationEmailManager;

		public CheckClosedCompetitionRegistrationsCommandHandler(ILogger<CheckClosedCompetitionRegistrationsCommandHandler> logger, IUnitOfWork unitOfWork, IRegistrationUnitOfWork registrationUnitOfWork, 
			ICompetitionRepository competitionRepository, ICompetitionRegistrationSummaryRepository competitionRegistrationSummaryRepository, ICompetitionRegistrationRepository competitionRegistrationRepository,
			IRegistrationEmailManager registrationEmailManager)
		{
			this._logger = logger;
			this._unitOfWork = unitOfWork;
			this._registrationUnitOfWork = registrationUnitOfWork;
			this._competitionRepository = competitionRepository;
			this._competitionRegistrationSummaryRepository = competitionRegistrationSummaryRepository;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
			this._registrationEmailManager = registrationEmailManager;
		}
		
		public async Task<DefaultCommandResponse> Handle(CheckClosedCompetitionRegistrationsCommand command)
		{
			
			this._unitOfWork.Begin();
			this._registrationUnitOfWork.Begin();

			RecaptchaResponse recaptchaResponse = null;
			try
			{
				DateTime end = DateTime.UtcNow;
				DateTime start = end.AddDays(-2);
				var closedCompetitions = await this._competitionRepository.GetClosedOnlineCompetitions(start, end);

				foreach (var competition in closedCompetitions)
				{
					var summary = await this._competitionRegistrationSummaryRepository.GetByCompetition(competition.ID);

					if (summary == null)
					{
						List<CompetitionRegistration> registrations = await this._competitionRegistrationRepository.GetAll(competition.ID);
						await this._registrationEmailManager.SendSummaryEmail(registrations, competition);
						
						summary = new CompetitionRegistrationSummary();
						summary.CompetitionID = competition.ID;
						summary.SummarySent = true;
						summary.SummarySentDate = DateTime.UtcNow;

						await this._competitionRegistrationSummaryRepository.Save(summary);
					}
				}
				
				this._unitOfWork.SoftCommit();
				this._registrationUnitOfWork.SoftCommit();
					
				//await this._registrationEmailManager.SendConfirmationEmails(registration, this._competition);
				return DefaultCommandResponse.Create(new ValidationResult());
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				this._registrationUnitOfWork.Rollback();
				this._logger.LogError("Error", e);
				throw;
			}
		}
	}
}