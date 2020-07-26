using Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Match;
using Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<IPlayerFixtureRepository, PlayerFixtureRepository>();
			services.AddTransient<IPlayerMatchRepository, PlayerMatchRepository>();
			services.AddTransient<ICompetitionEntrantRepository, CompetitionEntrantRepository>();
			services.AddTransient<ICompetitionRegistrationConfigurationRepository, CompetitionRegistrationConfigurationRepository>();
			services.AddTransient<ICompetitionRegistrationRepository, CompetitionRegistrationRepository>();
			services.AddTransient<ICompetitionEventRepository, CompetitionEventRepository>();
			services.AddTransient<ICompetitionHeaderRepository, CompetitionHeaderRepository>();
			services.AddTransient<ICompetitionRepository, CompetitionRepository>();
			services.AddTransient<ICompetitionRoundRepository, CompetitionRoundRepository>();
			services.AddTransient<ICompetitionStageRepository, CompetitionStageRepository>();
			services.AddTransient<IGameVariationRepository, GameVariationRepository>();
			services.AddTransient<IKnockoutCalculationEngineRepository, KnockoutCalculationEngineRepository>();
			services.AddTransient<IPlayerCompetitionRoundRepository, PlayerCompetitionRoundRepository>();
			services.AddTransient<ICompetitionDateRepository, CompetitionDateRepository>();
			services.AddTransient<ICompetitionRegistrationAttemptRepository, CompetitionRegistrationAttemptRepository>();
		}
	}
}