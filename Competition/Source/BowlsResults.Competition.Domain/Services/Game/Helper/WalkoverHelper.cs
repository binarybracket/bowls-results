using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game.Helper
{
	/// <summary>
	/// Walkover Helper
	/// </summary>
	public static class WalkoverHelper
	{
		public static bool MapHomeWalkoverValue(Walkover walkover)
		{
			switch (walkover)
			{
				case Walkover.None:
					return false;
				case Walkover.Home:
					return true;
				case Walkover.Away:
					return false;
				default:
					throw new ArgumentOutOfRangeException("walkover");
			}
		}

		public static bool MapAwayWalkoverValue(Walkover walkover)
		{
			switch (walkover)
			{
				case Walkover.None:
					return false;
				case Walkover.Home:
					return false;
				case Walkover.Away:
					return true;
				default:
					throw new ArgumentOutOfRangeException("walkover");
			}
		}
	}
}