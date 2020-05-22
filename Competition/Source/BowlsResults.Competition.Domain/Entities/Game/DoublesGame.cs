namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	public class DoublesGame : Game
	{
		public DoublesGame()
			: base(GameFormats.Doubles)
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
	}
}