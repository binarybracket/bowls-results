using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game
{
	public sealed class GameCalculationEngineFactory : IGameCalculationEngineFactory
	{
		private static readonly Game15UpCalculationEngine _game15UpInstance;
		private static readonly Game21UpCalculationEngine _game21UpInstance;

		static GameCalculationEngineFactory()
		{
			_game15UpInstance = new Game15UpCalculationEngine();
			_game21UpInstance = new Game21UpCalculationEngine();
		}

		public IGameCalculationEngine Create(GameCalculationEngines calculationEngine)
		{
			switch (calculationEngine)
			{
				case GameCalculationEngines.Game15Up:
					return _game15UpInstance;
				case GameCalculationEngines.Game21Up:
					return _game21UpInstance;
				default:
					throw new ArgumentOutOfRangeException("calculationEngine");
			}
		}
	}
}