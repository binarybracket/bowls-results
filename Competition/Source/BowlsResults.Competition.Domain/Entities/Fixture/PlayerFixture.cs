using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Extensions;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class PlayerFixture : Fixture
	{
		private ISet<PlayerMatch> _matches;

		public PlayerFixture()
		{
			this._matches = new HashSet<PlayerMatch>();
		}

		public virtual int? Player1ID { get; set; }
		public virtual int? Player2ID { get; set; }

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

		public virtual int WinningPlayerID
		{
			get
			{
				if (this.Player1ResultTypeID.Value == ResultType.Win)
				{
					return this.Player1ID.Value;
				}

				if (this.Player2ResultTypeID.Value == ResultType.Win)
				{
					return this.Player2ID.Value;
				}

				throw new InvalidOperationException("No winning team");
			}
		}

		public virtual int LosingPlayerID
		{
			get
			{
				if (this.Player1ResultTypeID.Value == ResultType.Lose)
				{
					return this.Player1ID.Value;
				}

				if (this.Player2ResultTypeID.Value == ResultType.Lose)
				{
					return this.Player2ID.Value;
				}

				throw new InvalidOperationException("No winning team");
			}
		}

		public virtual PlayerMatch CreateMatch(int homePlayerID, int awayPlayerID, VenueTypes venueTypeID)
		{
			throw new NotImplementedException(); // TODO
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

		public virtual int GetPlayerByResultType(ResultType pendingPlayer1ResultType)
		{
			switch (pendingPlayer1ResultType)
			{
				case ResultType.Win:
					return this.WinningPlayerID;
				case ResultType.Lose:
					return this.LosingPlayerID;
				default:
					throw new ArgumentOutOfRangeException("pendingPlayer1ResultType");
			}
		}
	}
}