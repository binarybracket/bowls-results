﻿using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

 namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public interface IPlayerFixtureCalculationEngine
	{
		void Calculate(PlayerFixture fixture);
	}
}