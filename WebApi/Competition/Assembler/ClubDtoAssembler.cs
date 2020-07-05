using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class ClubDtoAssembler
	{
		public static List<ClubDto> AssembleDtoList(this IEnumerable<Club> clubs)
		{
			var list = new List<ClubDto>();

			foreach (var club in clubs)
			{
				list.Add(club.AssembleDto());
			}

			return list;
		}
		public static ClubDto AssembleDto(this Club club)
		{
			return  new ClubDto
			{
				ID = club.ID,
				Name = club.Name,
				Longitude = club.Longitude,
				Latitude = club.Latitude,
				Active = club.Active
			};
		}
	}
}