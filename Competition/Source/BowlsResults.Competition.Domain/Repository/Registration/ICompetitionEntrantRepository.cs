using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration
{
	public interface ICompetitionEntrantRepository : IIdentityRepository<CompetitionEntrant, int>
	{
		
	}
}