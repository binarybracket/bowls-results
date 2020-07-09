using System.Collections.Generic;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request
{
	public sealed class GameResult
	{
		public GameResult()
		{
			this.VoidGame = false;
			this.HomePlayers = new List<int>();
			this.AwayPlayers = new List<int>();
		}

		public short MatchFormatXGameVariationID { get; set; }

		public bool VoidGame { get; set; }
		public Walkover Walkover { get; set; }

		public byte? HomeHandicap { get; set; }
		public byte? AwayHandicap { get; set; }

		public byte HomeScore { get; set; }
		public byte AwayScore { get; set; }

		public List<int> HomePlayers { get; set; }
		public List<int> AwayPlayers { get; set; }

		public List<int>  AllPlayers
		{
			get
			{
				var list = new List<int>();
				list.AddRange(this.HomePlayers);
				list.AddRange(this.AwayPlayers);
				return list;
			}
		}
	}
}