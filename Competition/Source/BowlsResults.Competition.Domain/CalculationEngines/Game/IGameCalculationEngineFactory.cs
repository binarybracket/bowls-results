using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game
{
	public interface IGameCalculationEngineFactory
	{
		IGameCalculationEngine Create(GameCalculationEngines calculationEngine);
	}
}