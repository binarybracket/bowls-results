using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Exceptions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game
{
	public sealed class DoublesGameService : BaseGameService<DoublesGame>, IGameService<DoublesGame>
	{
		private readonly ILogger<DoublesGameService> _logger;
		private readonly IPitchRepository _pitchRepository;
		private readonly IPlayerRepository _playerRepository;

		public DoublesGameService(ILogger<DoublesGameService> logger, IPitchRepository pitchRepository, IPlayerRepository playerRepository)
		{
			this._logger = logger;
			this._pitchRepository = pitchRepository;
			this._playerRepository = playerRepository;
		}

		protected override GameFormats GameFormat => GameFormats.Doubles;
		
		protected override async Task<Entities.Game.Game> InnerCreate(IPlayerMatchModel matchModel, GameResult gameResult)
		{
			var matchConfiguration = matchModel.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			var gameData = new DoublesGame();
			gameData.AssociationID = matchModel.Context.Competition.AssociationID;
			gameData.SeasonID = matchModel.Context.Competition.Season.ID;
			gameData.Date = matchModel.Data.Date;
			gameData.VenueTypeID = matchModel.Data.VenueTypeID;
			gameData.Pitch = matchModel.Data.Pitch;

			if (gameResult.VoidGame)
			{
				Player voidPlayer = await this._playerRepository.Get(-7);

				this.SetVoidGameValues(gameData, gameResult, matchConfiguration);

				gameData.AddHomePlayer(voidPlayer);
				gameData.AddHomePlayer(voidPlayer);
				gameData.AddAwayPlayer(voidPlayer);
				gameData.AddAwayPlayer(voidPlayer);
			}
			else
			{
				this.GuardCheckPlayerCounts(gameResult);

				IList<Player> homePlayers = await this.GetPlayers(gameResult.HomePlayers);
				IList<Player> awayPlayers = await this.GetPlayers(gameResult.AwayPlayers);
				var loadedPlayers = homePlayers.Concat(awayPlayers);

				this.GuardCheckLoadedPlayerCounts(loadedPlayers);
				this.GuardCheckLoadedPlayerGenders(loadedPlayers, matchConfiguration);

				this.SetCommonGameValues(gameData, gameResult, matchConfiguration);

				foreach (Player player in homePlayers)
				{
					gameData.AddHomePlayer(player);
				}
				foreach (Player player in awayPlayers)
				{
					gameData.AddAwayPlayer(player);
				}
			}

			gameData.SetAuditFields();

			return gameData;
		}

		protected override async Task<Entities.Game.Game> InnerUpdate(IPlayerMatchModel matchModel, GameResult gameResult)
		{
			var matchConfiguration = matchModel.Data.MatchFormat.GetVariationByID(gameResult.MatchFormatXGameVariationID);
			var playerGame = matchModel.GetGame(gameResult.MatchFormatXGameVariationID);
			var gameData = playerGame.Game as DoublesGame;
			if (gameData == null) throw new ArgumentNullException(nameof(gameData));

			if (gameResult.VoidGame)
			{
				Player voidPlayer = await this._playerRepository.Get(-7);

				this.SetVoidGameValues(gameData, gameResult, matchConfiguration);

				gameData.AddHomePlayer(voidPlayer);
				gameData.AddHomePlayer(voidPlayer);
				gameData.AddAwayPlayer(voidPlayer);
				gameData.AddAwayPlayer(voidPlayer);
			}
			else
			{
				this.GuardCheckPlayerCounts(gameResult);
				IList<Player> homePlayers = await this.GetPlayers(gameResult.HomePlayers);
				IList<Player> awayPlayers = await this.GetPlayers(gameResult.AwayPlayers);
				IEnumerable<Player> loadedPlayers = homePlayers.Concat(awayPlayers).ToList();

				this.GuardCheckLoadedPlayerCounts(loadedPlayers);
				this.GuardCheckLoadedPlayerGenders(loadedPlayers, matchConfiguration);

				this.SetCommonGameValues(gameData, gameResult, matchConfiguration);

				foreach (Player player in homePlayers)
				{
					gameData.AddHomePlayer(player);
				}
				foreach (Player player in awayPlayers)
				{
					gameData.AddAwayPlayer(player);
				}
			}


			gameData.SetAuditFields();

			return gameData;
		}

		private Task<List<Player>> GetPlayers(List<int> playerIDs)
		{
			return this._playerRepository.Get(playerIDs.ToArray());
		}

		private void GuardCheckPlayerCounts(GameResult gameResult)
		{
			this.GuardCheckPlayersCount(gameResult.HomePlayers, 2, "HomePlayers");
			this.GuardCheckPlayersCount(gameResult.AwayPlayers, 2, "AwayPlayers");
		}

		private void GuardCheckLoadedPlayerCounts(IEnumerable<Player> players)
		{
			this.GuardCheckPlayersCount(players, 4, "LoadedPlayers");
		}

		private void GuardCheckLoadedPlayerGenders(IEnumerable<Player> players, MatchFormatXGameVariation matchConfiguration)
		{
			// Only check when not mixed as mixed games will allow different sexes!!!
			Genders genderToCheck = matchConfiguration.GameVariation.GenderID;
			if (genderToCheck != Genders.Mixed)
			{
				if (players.Any(x => !x.IsInternalPlayer() && x.GenderID != genderToCheck))
				{
					throw new InvalidResultsEngineOperationException($"All players should be [{genderToCheck}]");
				}
			}
		}
	}
}