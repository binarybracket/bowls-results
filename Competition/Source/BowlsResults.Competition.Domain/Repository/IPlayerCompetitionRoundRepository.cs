using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface IPlayerCompetitionRoundRepository: IIdentityRepository<PlayerCompetitionRound, short>
	{
		Task<List<PlayerCompetitionRound>> GetAll(short competitionEventID);
	}
}