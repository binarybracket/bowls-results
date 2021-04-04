using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture
{
	public interface IPlayerFixtureRepository : IIdentityRepository<PlayerFixture, short>
	{
		Task<PlayerFixture> GetFull(short id);
		Task<List<PlayerFixture>> GetAll(short competitionRoundID);
		Task<List<PlayerFixture>> GetAllFullByCompetition(int competitionID);
		Task<List<PlayerFixture>> GetPendingFixtures(short relatedFixtureID);
	}
}