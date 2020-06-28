using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class ClubDtoAssembler
	{
		public static ClubDto AssembleDto(this Club club)
		{
			return  new ClubDto
			{
				ID = club.ID,
				Name = club.Name
			};
		}
	}
}