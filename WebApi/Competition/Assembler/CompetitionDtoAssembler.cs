using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class CompetitionDtoAssembler
	{
		public static List<CompetitionDto> AssembleDtoList(this IEnumerable<Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition> competitions)
		{
			var list = new List<CompetitionDto>();

			foreach (var competition in competitions)
			{
				list.Add(competition.AssembleDto());
			}

			return list;
		}

		public static CompetitionDto AssembleDto(this Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition competition)
		{
			var dto = new CompetitionDto();

			dto.ID = competition.ID;
			dto.RegistrationStatus = competition.GetRegistrationStatus();
			dto.Name = competition.Name;
			dto.GameVariation = competition.GameVariation.Name;
			dto.StartDate = competition.StartDate;
			dto.VenueClub = competition.VenueClub.AssembleDto();
			dto.Sponsor = competition.Sponsor;

			if (competition.RegistrationConfiguration != null)
			{
				dto.RegistrationConfiguration = competition.RegistrationConfiguration.AssembleDto();
			}

			return dto;
		}
	}
}