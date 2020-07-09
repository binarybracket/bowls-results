using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Extensions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class PlayerFixture : Fixture
	{
		private Guid _guid = Guid.NewGuid();
		public virtual PlayerCompetitionRound CompetitionRound { get; set; }
		private ISet<PlayerMatch> _matches;

		public PlayerFixture()
		{
			this.CompetitionScopeID = CompetitionScopes.Player;
			this._matches = new HashSet<PlayerMatch>();
		}

		public virtual PlayerFixture PendingPlayer1Fixture { get; set; }
		public virtual PlayerFixture PendingPlayer2Fixture { get; set; }

		public virtual CompetitionEntrant Entrant1 { get; set; }
		public virtual CompetitionEntrant Entrant2 { get; set; }

		public override bool Equals(object obj)
		{
			if (this.ID == 0)
			{
				var other = (PlayerFixture) obj;
				return this._guid.Equals(other._guid);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (this.ID == 0)
			{
				return this._guid.GetHashCode();
			}
			return base.GetHashCode();
		}

		public virtual ReadOnlyCollection<PlayerMatch> Matches
		{
			get { return this._matches.ToReadOnlyCollection(); }
		}

		public virtual PlayerMatch CreateMatchTemplate(bool entrant1Home, DateTime date)
		{
			if (this._matches.Count == this.Legs)
			{
				throw new InvalidOperationException("Too many Matches added for this fixture.  Fixture already has enough matches for the configured number of legs.");
			}

			var matchFormat = this.CompetitionRound.CompetitionEvent.GetMatchFormat();
			if (matchFormat.GameVariations.Count > 1)
			{
				throw new InvalidOperationException("Too many game variations to auto-generate match");
			}

			var homePlayers = entrant1Home ? this.Entrant1.GetPlayers() : this.Entrant2.GetPlayers();
			var awayPlayers = entrant1Home ? this.Entrant2.GetPlayers() : this.Entrant1.GetPlayers();

			var leg = (byte) (this._matches.Count + 1);
			var match = PlayerMatch.Create(this, DateTime.Now, leg, matchFormat, entrant1Home);
			match.VenueTypeID = VenueTypes.HomeAway;
			match.PitchID = 1;
			
			this._matches.Add(match);

			var gameVariationSettings = matchFormat.GameVariations.First();
			var gameVariation = gameVariationSettings.GameVariation;
			var game = Game.Game.CreateGame(gameVariation.GameFormatID);
			game.SetAuditFields();
			game.Date = match.Date;
			game.Pitch = new Pitch {ID = match.PitchID};
			game.VenueTypeID = match.VenueTypeID;
			game.GameFormatID = gameVariation.GameFormatID;
			game.GameVariationID = gameVariation.ID;
			game.SeasonID = this.Season.ID;
			game.AssociationID = this.CompetitionRound.CompetitionEvent.Competition.AssociationID;
			game.GameStatusID = GameStatuses.Standard;
			game.GameCalculationEngineID = gameVariationSettings.GameCalculationEngineID;
			foreach (var homePlayer in homePlayers)
			{
				game.AddHomePlayer(homePlayer);
			}
			foreach (var awayPlayer in awayPlayers)
			{
				game.AddAwayPlayer(awayPlayer);
			}

			match.AddGame(gameVariationSettings, game);
			
			return match;
		}

		/// <summary>
		/// Get Match By ID
		/// </summary>
		/// <param name="id">Match ID</param>
		/// <returns><see cref="PlayerMatch"/></returns>
		public virtual PlayerMatch GetMatchByID(int id)
		{
			return this.Matches.Single(x => x.ID == id);
		}
		
		public virtual CompetitionEntrant GetEntrantByResultType(ResultType pendingTeam1ResultType)
		{
			switch (pendingTeam1ResultType)
			{
				case ResultType.Win:
					return this.WinningEntrantID;
				case ResultType.Lose:
					return this.LosingEntrantID;
				default:
					throw new ArgumentOutOfRangeException(nameof(pendingTeam1ResultType));
			}
		}
		
		public virtual CompetitionEntrant WinningEntrantID
		{
			get
			{
				if (this.Entrant1ResultTypeID.Value == ResultType.Win)
				{
					return this.Entrant1;
				}
				if (this.Entrant2ResultTypeID.Value == ResultType.Win)
				{
					return this.Entrant2;
				}
				throw new InvalidOperationException("No winning team");
			}
		}

		public virtual CompetitionEntrant LosingEntrantID
		{
			get
			{
				if (this.Entrant1ResultTypeID.Value == ResultType.Lose)
				{
					return this.Entrant1;
				}
				if (this.Entrant2ResultTypeID.Value == ResultType.Lose)
				{
					return this.Entrant2;
				}
				throw new InvalidOperationException("No winning team");
			}
		}
	}
}