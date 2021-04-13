using System;
using BowlsResults.WebApi.Common.Dto;
using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.PlayerCompetition
{
	public abstract class BasePlayerFixtureDto : BaseFixtureDto
	{
		public FixtureSummaryDataDto SummaryData { get; set; }
	}
}