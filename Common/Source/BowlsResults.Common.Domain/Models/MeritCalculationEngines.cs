namespace BowlsResults.Common.Domain.Models
{
	/// <summary>
	/// Merit Calculation Engines
	/// </summary>
	public enum MeritCalculationEngines
	{
		Default = 0,
		IomMenNightLeague = 1,
		IomMenOver60League = 2,
		IomLadiesNightOver60LeagueStandard = 3,
		IomLadiesNightOver60LeagueDropGames = 4,
		IomWinterLeague2006 = 5,
		DefaultAverages = 6,
		IomSuperLegaue = 7
	}
}