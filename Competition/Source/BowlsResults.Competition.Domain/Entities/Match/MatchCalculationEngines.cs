namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public enum MatchCalculationEngines
	{
		LeagueMatchByChalks = -1,
		LeagueMatchByGames = -2,
		KnockoutMatchByGames = -3,
		KnockoutMatchByChalksThenGames = -4,
		MatchByChalks = -5,
		KnockoutMatchByGamesThenChalks = -6,

		IomMenNightLeague2009 = 1
	}
}