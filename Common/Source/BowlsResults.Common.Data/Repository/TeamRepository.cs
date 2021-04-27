using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class TeamRepository : IdentityRepository<Team, int>, ITeamRepository
	{
		public TeamRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<Team> GetByNameGenderAndAgeGroup(string name, AgeGroups ageGroup, Genders gender)
		{
			return this
				.SessionQuery()
				.SingleOrDefaultAsync(x => x.Name == name && x.GenderID == gender && x.AgeGroupID == ageGroup);
		}
	}
}