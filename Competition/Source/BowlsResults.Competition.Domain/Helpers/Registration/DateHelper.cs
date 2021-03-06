using System;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Helpers.Registration
{
	public static class DateHelper
	{
		public static DateTime GenerateOpenDate(DateTime competitionDate, DateTime? openDate)
		{
			if (openDate.HasValue)
			{
				return openDate.Value;
			}
			else
			{
				DateTime newDate = competitionDate.Date.AddDays(-14);
				return newDate.AddHours(6);
			}
		}
		
		public static DateTime GenerateCloseDate(DateTime competitionDate, DateTime? closeDate)
		{
			if (closeDate.HasValue)
			{
				return closeDate.Value;
			}
			else
			{
				return competitionDate.AddHours(-19);
			}
		}
	}
}