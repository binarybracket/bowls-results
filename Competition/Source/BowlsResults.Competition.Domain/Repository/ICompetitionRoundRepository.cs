using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionRoundRepository: IIdentityRepository<CompetitionRound, short>
	{
		Task<List<CompetitionRound>> GetAll(short competitionEventID);
	}
}