using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class PlayerMatch : Match
	{
		public PlayerMatch()
		{
			this.ScopeID = CompetitionScopes.Player;
			this.Games = new HashSet<PlayerMatchXGame>();
		}
				
		public virtual ISet<PlayerMatchXGame> Games { get; set; }
		
		public virtual PlayerFixture PlayerFixture { get; set; }
		
		public virtual CompetitionEntrant Home { get; set; }
		public virtual CompetitionEntrant Away { get; set; }

		public virtual IEnumerable<PlayerMatchXGame> ValidGamesForCalculation
		{
			get 
			{ 
				return this.Games.Where(x => x.Game.GameStatusID == GameStatuses.Standard); 
			}
		}
		
		public virtual PlayerMatchXGame AddGame(MatchFormatXGameVariation matchFormatGameVariation, Game.Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException(nameof(game));
			}

			var existing = this.Games.SingleOrDefault(x => x.MatchFormatXGameVariation.ID == matchFormatGameVariation.ID);
			if (existing != null)
			{
				throw new InvalidOperationException($"Game for Variation ID {matchFormatGameVariation.ID} has already been added");
			}

			var matchGame = new PlayerMatchXGame();
			matchGame.Match = this;
			matchGame.MatchFormatXGameVariation = matchFormatGameVariation;
			matchGame.Game = game;

			this.Games.Add(matchGame);
			//if (!this._games.Add(matchGame))
			{
				//throw new InvalidOperationException("Game already added to Match.");
			}

			return matchGame;
		}
	}
}