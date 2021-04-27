using Com.BinaryBracket.BowlsResults.Common.Data.Repository;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Common.Data
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<IAssociationRepository, AssociationRepository>();
			services.AddTransient<IClubRepository, ClubRepository>();
			services.AddTransient<ITeamRepository, TeamRepository>();
			services.AddTransient<IContactRepository, ContactRepository>();
			services.AddTransient<IPitchRepository, PitchRepository>();
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<ISeasonRepository, SeasonRepository>();
		}
	}
}