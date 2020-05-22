using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
	/// <summary>
	/// HomePlayer Domain Object
	/// </summary>
	public class HomePlayer : GameXPlayer
	{
		public HomePlayer()
		{
			this.SideID = Sides.Home;
		}
	}
}