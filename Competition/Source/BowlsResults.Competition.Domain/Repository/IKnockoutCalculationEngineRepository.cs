using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface IKnockoutCalculationEngineRepository : IIdentityRepository<KnockoutCalculationEngine, byte>
	{
		Task<KnockoutCalculationEngine> GetFull(KnockoutCalculationEngines engineID);
	}
}