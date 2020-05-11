using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers
{
	public class CreateCompetitionCommandHandler : ICommandHandler<CreateCompetitionCommand, DefaultCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionHeaderRepository _competitionHeaderRepository;

		public CreateCompetitionCommandHandler(IUnitOfWork unitOfWork, ICompetitionHeaderRepository competitionHeaderRepository)
		{
			this._unitOfWork = unitOfWork;
			this._competitionHeaderRepository = competitionHeaderRepository;
		}

		public async Task<DefaultCommandResponse> Handle(CreateCompetitionCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				var header = await this._competitionHeaderRepository.Get(command.CompetitionHeaderID);
				if (header.AssociationID != command.AssociationID)
				{
					throw new NotSupportedException("Association does not match");
				}
			}
			catch (Exception e)
			{
				this._unitOfWork.Rollback();
				Console.WriteLine(e);
				throw;
			}
			
			throw new NotImplementedException();
		}
	}
}