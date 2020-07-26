using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class CompetitionDateDtoAssembler
	{
		public static List<CompetitionDateDto> AssembleDtoList(this IEnumerable<CompetitionDate> competitionDates)
		{
			var list = new List<CompetitionDateDto>();

			foreach (var competitionDate in competitionDates)
			{
				list.Add(competitionDate.AssembleDto());
			}

			return list;
		}
		public static CompetitionDateDto AssembleDto(this CompetitionDate competitionDate)
		{
			return  new CompetitionDateDto
			{
				Description =  competitionDate.Description,
				Date =  competitionDate.Date
			};
		}
	}
}