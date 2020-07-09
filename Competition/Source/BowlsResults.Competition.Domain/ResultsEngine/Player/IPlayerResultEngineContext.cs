using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;
using Com.BinaryBracket.Core.Domain2;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public interface IPlayerResultEngineContext : IResultsEngineContext
	{
		Entities.Competition Competition { get; }
		CompetitionStage CompetitionStage { get; }
		CompetitionEvent CompetitionEvent { get; }
		CompetitionRound CompetitionRound { get; }
		IPlayerFixtureModel PlayerFixture { get; }

		void Initialise(Entities.Competition competition, CompetitionStage stage, CompetitionEvent competitionEvent, CompetitionRound round, IPlayerFixtureModel playerFixture);

		Task Save();
	}

	public class PlayerResultEngineContext : IPlayerResultEngineContext
	{
		private readonly IPlayerFixtureRepository _playerFixtureRepository;
		private readonly IPlayerMatchRepository _playerMatchRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PlayerResultEngineContext(IPlayerFixtureRepository playerFixtureRepository, IPlayerMatchRepository playerMatchRepository)
		{
			this._playerFixtureRepository = playerFixtureRepository;
			this._playerMatchRepository = playerMatchRepository;
		}
		
		public void Initialise(Entities.Competition competition, CompetitionStage stage, CompetitionEvent competitionEvent, CompetitionRound round,
			IPlayerFixtureModel playerFixture)
		{
			this.Competition = competition;
			this.CompetitionStage = stage;
			this.CompetitionEvent = competitionEvent;
			this.CompetitionRound = round;
			this.PlayerFixture = playerFixture;
		}

		public async Task Save()
		{
			foreach (var match in this.PlayerFixture.Data.Matches)
			{
				await this._playerMatchRepository.Save(match);
			}

			await this._playerFixtureRepository.Save(this.PlayerFixture.Data);
			await this._playerFixtureRepository.Flush();
		}

		public Entities.Competition Competition { get; private set; }
		public CompetitionStage CompetitionStage { get; private set; }
		public CompetitionEvent CompetitionEvent { get; private set; }
		public CompetitionRound CompetitionRound { get; private set; }
		public IPlayerFixtureModel PlayerFixture { get; private set; }
	}
}