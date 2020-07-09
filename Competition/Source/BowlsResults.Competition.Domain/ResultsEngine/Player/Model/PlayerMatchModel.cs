using System;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public sealed class PlayerMatchModel : BaseModel<PlayerMatch, IPlayerResultEngineContext>, IPlayerMatchModel
	{
		private readonly IGameServiceFactory _gameServiceFactory;
		private readonly IGameCalculationEngineFactory _gameCalculationEngineFactory;

		public PlayerMatchModel(IGameServiceFactory gameServiceFactory, IGameCalculationEngineFactory gameCalculationEngineFactory)
		{
			this._gameServiceFactory = gameServiceFactory;
			this._gameCalculationEngineFactory = gameCalculationEngineFactory;
		}
		
		public bool GameExists(short matchFormatXGameVariationID)
		{
			return this.Data.Games.Any(x => x.MatchFormatXGameVariation.ID == matchFormatXGameVariationID);
		}

		public async Task<Game> AddGame(GameResult gameResult)
		{
			var matchConfiguration = this.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			IGameService service = this._gameServiceFactory.Create(matchConfiguration);
			var gameData = await service.Create(this, gameResult);

			var calculationEngine = this._gameCalculationEngineFactory.Create(matchConfiguration.GameCalculationEngineID);
			calculationEngine.Calculate(gameData);

			this.Data.AddGame(matchConfiguration, gameData);
			return gameData;
		}

		public async Task<Game> UpdateGame(GameResult gameResult)
		{
			var matchConfiguration = this.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			IGameService service = this._gameServiceFactory.Create(matchConfiguration);
			var gameData = await service.Update(this, gameResult);
			
			var calculationEngine = this._gameCalculationEngineFactory.Create(matchConfiguration.GameCalculationEngineID);
			calculationEngine.Calculate(gameData);

			return gameData;
		}

		public PlayerMatchXGame GetGame(short matchFormatXGameVariationID)
		{
			return this.Data.Games.Single(x => x.MatchFormatXGameVariation.ID == matchFormatXGameVariationID);
		}
	}
}