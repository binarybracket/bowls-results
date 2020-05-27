using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	public class Game : AuditableEntity<int>
	{
		private readonly IList<GameXPlayer> _players;

		public Game()
		{
			this._players = new List<GameXPlayer>();
		}

		public Game(GameFormats gameFormatID)
		{
			this._players = new List<GameXPlayer>();
			this.GameFormatID = gameFormatID;
		}

		public virtual int AssociationID { get; set; }
		public virtual short SeasonID { get; set; }
		public virtual byte GameVariationID { get; set; }
		public virtual GameFormats GameFormatID { get; set; }
		public virtual GameCalculationEngines GameCalculationEngineID { get; set; }

		public virtual VenueTypes VenueTypeID { get; set; }
		public virtual Pitch Pitch { get; set; }

		public virtual DateTime Date { get; set; }

		public virtual byte? HomeHandicap { get; set; }
		public virtual byte? AwayHandicap { get; set; }

		public virtual byte HomeScore { get; set; }
		public virtual bool HomeWalkover { get; set; }
		public virtual byte AwayScore { get; set; }
		public virtual bool AwayWalkover { get; set; }
		public virtual bool Completed { get; set; }
		public virtual GameStatuses GameStatusID { get; set; }
		public virtual ResultType? HomeResultTypeID { get; set; }
		public virtual ResultType? AwayResultTypeID { get; set; }

		public virtual ReadOnlyCollection<GameXPlayer> Players
		{
			get { return new ReadOnlyCollection<GameXPlayer>(this._players.ToList()); }
		}

		public virtual IList<GameXPlayer> HomePlayers
		{
			get { return new ReadOnlyCollection<GameXPlayer>(this._players.Where(q => q.SideID == Sides.Home).ToList()); }
		}

		public virtual IList<GameXPlayer> AwayPlayers
		{
			get { return new ReadOnlyCollection<GameXPlayer>(this._players.Where(q => q.SideID == Sides.Away).ToList()); }
		}

		public virtual void ClearPlayers()
		{
			foreach (var gameXPlayer in this._players)
			{
				gameXPlayer.Player = null;
				gameXPlayer.Game = null;
			}

			this._players.Clear();
		}

		/// <summary>
		/// Add Home Player to Game
		/// </summary>
		/// <param name="player"><see cref="Player"/></param>
		public virtual void AddHomePlayer(Player player)
		{
			var gamePlayer = new HomePlayer();
			gamePlayer.Game = this;
			gamePlayer.Player = player;
			this._players.Add(gamePlayer);
		}

		/// <summary>
		/// Add Away Player to Game
		/// </summary>
		/// <param name="player"><see cref="Player"/></param>
		public virtual void AddAwayPlayer(Player player)
		{
			var gamePlayer = new AwayPlayer();
			gamePlayer.Game = this;
			gamePlayer.Player = player;
			this._players.Add(gamePlayer);
		}

		public virtual void GetScoresByPlayerID(int playerID, out short chalks, out short? handicap, out bool? walkover)
		{
			if (this.HomePlayers.Any(x => x.Player.ID == playerID))
			{
				chalks = this.HomeScore;
				handicap = this.HomeHandicap;
				walkover = this.HomeWalkover;
			}
			else
			{
				chalks = this.AwayScore;
				handicap = this.AwayHandicap;
				walkover = this.AwayWalkover;
			}
		}

		public virtual ResultType GetResultByPlayerID(int playerID)
		{
			if (this.HomePlayers.Any(x => x.Player.ID == playerID))
			{
				return this.HomeResultTypeID.Value;
			}

			return this.AwayResultTypeID.Value;
		}

		public virtual IList<GameXPlayer> GetWinners()
		{
			if (this.HomeResultTypeID == ResultType.Win)
			{
				return this.HomePlayers;
			}
			else if (this.AwayResultTypeID == ResultType.Win)
			{
				return this.AwayPlayers;
			}

			throw new InvalidOperationException("There is no winner set");
		}

		public virtual IList<GameXPlayer> GetLosers()
		{
			if (this.HomeResultTypeID == ResultType.Lose)
			{
				return this.HomePlayers;
			}
			else if (this.AwayResultTypeID == ResultType.Lose)
			{
				return this.AwayPlayers;
			}

			throw new InvalidOperationException("There is no loser set");
		}
	}
}