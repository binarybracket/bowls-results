using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class TeamDtoAssembler
	{
		public static List<TeamDto> AssembleDtoList(this IEnumerable<Com.BinaryBracket.BowlsResults.Common.Domain.Entities.Team> teams)
		{
			var list = new List<TeamDto>();

			foreach (var team in teams)
			{
				list.Add(team.AssembleDto());
			}

			return list;
		}

		public static TeamDto AssembleDto(this Com.BinaryBracket.BowlsResults.Common.Domain.Entities.Team team)
		{
			var dto = new TeamDto
			{
				ID = team.ID,
				Name = team.Name,
				Suffix = team.Suffix,
				Gender = team.GenderID,
				AgeGroup = team.AgeGroupID
			};
			if (team.Captain != null)
			{
				dto.Captain = ContactDtoAssembler.AssembleDto(team.Captain);
			}

			return dto;
		}
	}
}