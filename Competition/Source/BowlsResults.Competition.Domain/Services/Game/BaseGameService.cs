using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game.Helper;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game
{
	public abstract class BaseGameService<TData>
		where TData : Entities.Game.Game
	{
		protected abstract GameFormats GameFormat { get; }
		
		public Task<Entities.Game.Game> Create(IPlayerMatchModel matchModel, GameResult gameResult)
		{
			if (matchModel == null) throw new ArgumentNullException(nameof(matchModel));
			if (gameResult == null) throw new ArgumentNullException(nameof(gameResult));

			var matchConfiguration = matchModel.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			if (matchConfiguration == null) throw new ArgumentNullException(nameof(matchConfiguration));

			if (matchConfiguration.GameVariation.GameFormatID != this.GameFormat)
			{
				throw new InvalidOperationException("Wrong Game Format for Service");
			}
			
			return this.InnerCreate(matchModel, gameResult);
		}
		
		public Task<Entities.Game.Game> Update(IPlayerMatchModel matchModel, GameResult gameResult)
		{
			if (matchModel == null) throw new ArgumentNullException(nameof(matchModel));
			if (gameResult == null) throw new ArgumentNullException(nameof(gameResult));
			
			var matchConfiguration = matchModel.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			if (matchConfiguration == null) throw new ArgumentNullException(nameof(matchConfiguration));
			
			if (matchConfiguration.GameVariation.GameFormatID != GameFormats.Singles)
			{
				throw new InvalidOperationException("Wrong Game Format for Service");
			}

			return this.InnerUpdate(matchModel, gameResult);
		}

		protected abstract Task<Entities.Game.Game> InnerCreate(IPlayerMatchModel matchModel, GameResult gameResult);
		protected abstract Task<Entities.Game.Game> InnerUpdate(IPlayerMatchModel matchModel, GameResult gameResult);

		protected void SetCommonGameValues(TData gameData, GameResult gameResult, MatchFormatXGameVariation matchConfiguration)
		{
			gameData.GameStatusID = GameStatuses.Standard;

			gameData.HomeWalkover = WalkoverHelper.MapHomeWalkoverValue(gameResult.Walkover);
			gameData.AwayWalkover = WalkoverHelper.MapAwayWalkoverValue(gameResult.Walkover);

			gameData.HomeHandicap = gameResult.HomeHandicap;
			gameData.AwayHandicap = gameResult.AwayHandicap;

			gameData.HomeScore = gameResult.HomeScore;
			gameData.AwayScore = gameResult.AwayScore;

			gameData.GameVariation = matchConfiguration.GameVariation;

			gameData.GameCalculationEngineID = matchConfiguration.GameCalculationEngineID;
		}
		
		protected void SetVoidGameValues(TData gameData, GameResult gameResult, MatchFormatXGameVariation matchConfiguration)
		{
			gameData.GameStatusID = GameStatuses.Void;

			gameData.HomeWalkover = false;
			gameData.AwayWalkover = false;

			gameData.HomeHandicap = gameResult.HomeHandicap;
			gameData.AwayHandicap = gameResult.AwayHandicap;

			gameData.HomeScore = gameResult.HomeScore;
			gameData.AwayScore = gameResult.AwayScore;

			gameData.GameVariation = matchConfiguration.GameVariation;
			gameData.GameCalculationEngineID = matchConfiguration.GameCalculationEngineID;
		}

		protected void GuardCheckPlayersCount<T>(IEnumerable<T> playerIDs, int expectedCount, string descriptionForException)
		{
			if (playerIDs.Count() != expectedCount)
			{
				throw new ArgumentOutOfRangeException(nameof(playerIDs), $"Expected {expectedCount} players and received {playerIDs.Count()}.  Error raised for {descriptionForException}.");
			}
		}
	}
}