using System.IO;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game
{
	public sealed class Game21UpCalculationEngine : BaseGameCalculationEngine
	{
		public override short GameScoreTarget
		{
			get { return 21; }
		}

		protected override GameResultMargin CalculateMargin(int diff)
		{
			if (diff >= 1 && diff <=2)
			{
				return GameResultMargin.FiftyFifty;
			}
			else if (diff >= 3 && diff <= 4)
			{
				return GameResultMargin.Tight;
			}
			else if (diff >= 5 && diff <= 10)
			{
				return GameResultMargin.Good;
			}
			else if (diff >= 11 && diff <= 15)
			{
				return GameResultMargin.Strong;
			}
			else if (diff >= 16)
			{
				return GameResultMargin.Big;
			}
			throw new InvalidDataException("Difference not supported ["+diff+"]");
		}
	}
}