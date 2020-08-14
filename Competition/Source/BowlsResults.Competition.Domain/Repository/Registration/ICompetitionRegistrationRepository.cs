using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration
{
	public interface ICompetitionRegistrationRepository : IIdentityRepository<CompetitionRegistration, int>
	{
		Task<List<CompetitionRegistration>> GetAll(int competitionID);
	}
}