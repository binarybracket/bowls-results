using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Extensions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class PlayerFixture : Fixture
	{
		private ISet<PlayerMatch> _matches;

		public PlayerFixture()
		{
			this._matches = new HashSet<PlayerMatch>();
		}

		// TODO public virtual PendingPlayerFixture PendingPlayer1Fixture { get; set; }
		// TODO public virtual PendingPlayerFixture PendingPlayer2Fixture { get; set; }

		public virtual short? Player1GameScore { get; set; }
		public virtual short? Player2GameScore { get; set; }
		public virtual short? Player1ChalkScore { get; set; }
		public virtual short? Player2ChalkScore { get; set; }
		public virtual bool? Player1Walkover { get; set; }
		public virtual bool? Player2Walkover { get; set; }
		public virtual ResultType? Player1ResultTypeID { get; set; }
		public virtual ResultType? Player2ResultTypeID { get; set; }

		public virtual ReadOnlyCollection<PlayerMatch> Matches
		{
			get { return this._matches.ToReadOnlyCollection(); }
		}

		public virtual PlayerMatch CreateMatch()
		{
			if (this._matches.Count == this.Legs)
			{
				throw new InvalidOperationException("Too many Matches added for this fixture.  Fixture already has enough matches for the configured number of legs.");
			}

			var match = new PlayerMatch();
			match.PlayerFixture = this;
			match.MatchStatusID = MatchStatuses.Incomplete;
			match.Leg = (byte)(this._matches.Count + 1); // NOTE - calculated based on currently added matches
			match.Player1Home = false;
			
			match.SetAuditFields();
			this._matches.Add(match);
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
	}
}