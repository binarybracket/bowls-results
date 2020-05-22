using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	public class SinglesGame : Game
	{
		public SinglesGame()
			: base(GameFormats.Singles)
		{
		}

		public virtual GameXPlayer HomePlayer
		{
			get { return this.HomePlayers[0]; }
		}

		public virtual GameXPlayer AwayPlayer
		{
			get { return this.AwayPlayers[0]; }
		}

		public virtual void SetHomePlayer(Player player)
		{
			if (this.HomePlayers.Count > 0)
			{
				this.HomePlayers[0].Player = player;
			}
			else
			{
				this.AddHomePlayer(player);
			}
		}

		public virtual void SetAwayPlayer(Player player)
		{
			if (this.AwayPlayers.Count > 0)
			{
				this.AwayPlayers[0].Player = player;
			}
			else
			{
				this.AddAwayPlayer(player);
			}
		}

		public virtual GameXPlayer GetWinner()
		{
			if (this.HomePlayer.ResultTypeID == ResultType.Win)
			{
				return this.HomePlayer;
			}
			else if (this.AwayPlayer.ResultTypeID == ResultType.Win)
			{
				return this.AwayPlayer;
			}

			throw new InvalidOperationException("There is no winner set");
		}

		public virtual GameXPlayer GetLoser()
		{
			if (this.HomePlayer.ResultTypeID == ResultType.Lose)
			{
				return this.HomePlayer;
			}
			else if (this.AwayPlayer.ResultTypeID == ResultType.Lose)
			{
				return this.AwayPlayer;
			}

			throw new InvalidOperationException("There is no loser set");
		}
	}
}