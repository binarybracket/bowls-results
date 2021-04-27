using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Repository
{
	public interface ITeamRepository : IIdentityRepository<Team, int>
	{
		Task<Team> GetByNameGenderAndAgeGroup(string name, AgeGroups ageGroup, Genders gender);
	}
}