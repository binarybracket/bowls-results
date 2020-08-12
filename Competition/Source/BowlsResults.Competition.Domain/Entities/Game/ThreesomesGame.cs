namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	public class ThreesomesGame : Game
	{
		public ThreesomesGame()
			: base(GameFormats.Threesomes)
		{
		}
		
		public virtual GameXPlayer HomePlayer1
		{
			get { return this.HomePlayers[0]; }
		}
		public virtual GameXPlayer AwayPlayer1
		{
			get { return this.AwayPlayers[0]; }
		}
		public virtual GameXPlayer HomePlayer2
		{
			get { return this.HomePlayers[1]; }
		}
		public virtual GameXPlayer AwayPlayer2
		{
			get { return this.AwayPlayers[1]; }
		}
		public virtual GameXPlayer HomePlayer3
		{
			get { return this.HomePlayers[2]; }
		}
		public virtual GameXPlayer AwayPlayer3
		{
			get { return this.AwayPlayers[2]; }
		}
	}
}