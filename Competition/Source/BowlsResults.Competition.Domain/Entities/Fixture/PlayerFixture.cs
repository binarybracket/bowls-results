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

		public virtual void SetEntrant1(CompetitionEntrant entrant)
		{
			if (this.Entrant1 != null || this.PendingPlayer1Fixture != null)
			{
				throw new InvalidOperationException("Entrant1 or Fixture1 has already been allocated.");
			}

			this.Entrant1 = entrant;

			this.CheckFixtureReady();
		}

		public virtual void SetEntrant2(CompetitionEntrant entrant)
		{
			if (this.Entrant2 != null || this.PendingPlayer2Fixture != null)
			{
				throw new InvalidOperationException("Entrant2 or Fixture2 has already been allocated.");
			}

			this.Entrant2 = entrant;
			
			this.CheckFixtureReady();
		}

		public virtual void SetPendingFixture1(PlayerFixture fixture, ResultType resultType)
		{
			if (this.Entrant1 != null || this.PendingPlayer1Fixture != null)
			{
				throw new InvalidOperationException("Entrant1 or Fixture1 has already been allocated.");
			}

			this.PendingPlayer1Fixture = fixture;
			this.Pending1ResultTypeID = resultType;
		}

		public virtual void SetPendingFixture2(PlayerFixture fixture, ResultType resultType)
		{
			if (this.Entrant2 != null || this.PendingPlayer2Fixture != null)
			{
				throw new InvalidOperationException("Entrant2 or Fixture2 has already been allocated.");
			}

			this.PendingPlayer2Fixture = fixture;
			this.Pending2ResultTypeID = resultType;
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

			var match = this.CreateMatch(entrant1Home);

			var gameVariationSettings = matchFormat.GameVariations.First();
			var gameVariation = gameVariationSettings.GameVariation;
			var game = Game.Game.CreateGame(gameVariation.GameFormatID);
			game.SetAuditFields();
			game.Date = match.Date;
			game.Pitch = new Pitch {ID = match.Pitch.ID};
			game.VenueTypeID = match.VenueTypeID;
			game.GameFormatID = gameVariation.GameFormatID;
			game.GameVariation = gameVariation;
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

		public virtual CompetitionEntrant GetEntrantByResultType(ResultType resultType)
		{
			switch (resultType)
			{
				case ResultType.Win:
					return this.WinningEntrantID;
				case ResultType.Lose:
					return this.LosingEntrantID;
				default:
					throw new ArgumentOutOfRangeException(nameof(resultType));
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

		private void CheckFixtureReady()
		{
			if (this.Entrant1 != null && this.Entrant1.CompetitionEntrantStatusID == CompetitionEntrantStatuses.Confirmed)
			{
				if (this.Entrant2 != null && this.Entrant2.CompetitionEntrantStatusID == CompetitionEntrantStatuses.Confirmed)
				{
					this.SetupPendingMatches();

					this.PendingDate = null;
					this.SetIncomplete();
				}
			}
		}

		private void SetupPendingMatches()
		{
			for (var i = 1; i <= this.Legs; i++)
			{
				CreateMatch(i == 1);
			}
		}

		public virtual PlayerMatch CreateMatch(bool entrant1Home)
		{
			if (this._matches.Count == this.Legs)
			{
				throw new InvalidOperationException("Too many Matches added for this fixture.  Fixture already has enough matches for the configured number of legs.");
			}

			var matchFormat = this.CompetitionRound.CompetitionEvent.GetMatchFormat();
			var leg = (byte) (this._matches.Count + 1);
			var match = new PlayerMatch
			{
				PlayerFixture = this,
				Date = this.PendingDate.Value,
				Leg = leg,
				MatchFormat = matchFormat,
				MatchStatusID = MatchStatuses.Pending
			};
			match.MatchCalculationEngineID = this.CompetitionRound.CompetitionEvent.GetMatchCalculationEngine();
			match.Home = entrant1Home ? this.Entrant1 : this.Entrant2;
			match.Away = entrant1Home ? this.Entrant2 : this.Entrant1;
			match.VenueTypeID = VenueTypes.Neutral;
			match.Pitch = this.ResolvePitch();

			if (leg == 1 && match.Pitch != null)
			{
				match.SetIncomplete();
			}

			this._matches.Add(match);
			return match;
		}

		private Pitch ResolvePitch()
		{
			if (this.CompetitionRound.Competition.VenuePitch != null)
			{
				return this.CompetitionRound.Competition.VenuePitch;
			}
			else if (this.CompetitionRound.Competition.VenueClub != null)
			{
				return this.CompetitionRound.Competition.VenueClub.Pitch;
			}

			return null;
		}
	}
}