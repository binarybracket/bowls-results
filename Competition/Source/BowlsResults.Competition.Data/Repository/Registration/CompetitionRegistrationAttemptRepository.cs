using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public class CompetitionRegistrationAttemptRepository : IdentityRepository<CompetitionRegistrationAttempt, int>, ICompetitionRegistrationAttemptRepository
	{
		private readonly IRegistrationSessionProvider _provider;

		public CompetitionRegistrationAttemptRepository(IRegistrationSessionProvider provider) : base(provider)
		{
			this._provider = provider;
		}

		public override async Task Save(CompetitionRegistrationAttempt data)
		{
			await base.Save(data);
			await this._provider.Session.FlushAsync();
		}

		public Task<CompetitionRegistrationAttempt> GetTop()
		{
			return this.SessionQuery().FirstOrDefaultAsync();
		}
	}
}