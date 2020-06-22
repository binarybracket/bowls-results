using System;
using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Entrant;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class PlayerMatch : Match
	{
		public PlayerMatch()
		{
			this.ScopeID = CompetitionScopes.Player;
			this.Games = new List<PlayerMatchXGame>();
		}
				
		public virtual IList<PlayerMatchXGame> Games { get; set; }
		
		public virtual PlayerFixture PlayerFixture { get; set; }
		
		public virtual CompetitionEntrant Home { get; set; }
		public virtual CompetitionEntrant Away { get; set; }

		public static PlayerMatch Create(PlayerFixture fixture, DateTime date, byte leg, MatchFormat matchFormat, bool entrant1Home)
		{
			var data = new PlayerMatch
			{
				PlayerFixture = fixture,
				Date = date,
				Leg = leg,
				MatchFormat = matchFormat,
				MatchStatusID = MatchStatuses.Incomplete
			};
			data.Home = entrant1Home ? fixture.Entrant1 : fixture.Entrant2;
			data.Away = entrant1Home ? fixture.Entrant2 : fixture.Entrant1;
			
			return data;
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