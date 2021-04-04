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
		
		protected override bool SupportTransient => true;
		
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

		public virtual void GetScoresByEntrantID(int entrantID, out short gameScore, out short chalkScore, out short? bonusScore, out bool? walkover)
		{
			if(entrantID == this.Home.ID)
			{
				gameScore = this.HomeGameScore.Value;
				chalkScore = this.HomeChalkScore.Value;
				bonusScore = this.HomeBonusScore;
				walkover = this.HomeWalkover;
				return;
			}
			else if (entrantID == this.Away.ID)
			{
				gameScore = this.AwayGameScore.Value;
				chalkScore = this.AwayChalkScore.Value;
				bonusScore = this.AwayBonusScore;
				walkover = this.AwayWalkover;
				return;
			}
			throw new ArgumentOutOfRangeException($"Entrant {entrantID} is not part of this match");
		}
	}
}