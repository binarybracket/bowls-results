namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request
{
	public sealed class Handicap
	{
		public byte Home { get; set; }
		public byte Away { get; set; }
	}

	public sealed class HandicapDto
	{
		public byte? Home { get; set; }
		public byte? Away { get; set; }
	}
}