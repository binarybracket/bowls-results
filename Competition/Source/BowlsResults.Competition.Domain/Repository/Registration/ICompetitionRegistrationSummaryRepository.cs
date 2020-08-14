using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration
{
	public interface ICompetitionRegistrationSummaryRepository : IIdentityRepository<CompetitionRegistrationSummary, int>
	{
		Task<CompetitionRegistrationSummary> GetByCompetition(int competitionID);
	}
}