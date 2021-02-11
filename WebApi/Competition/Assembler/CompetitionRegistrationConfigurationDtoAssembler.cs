using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class CompetitionRegistrationConfigurationDtoAssembler
	{

		public static CompetitionRegistrationConfigurationDto AssembleDto(this CompetitionRegistrationConfiguration data)
		{
			var dto = new CompetitionRegistrationConfigurationDto();

			dto.Amount = data.Amount;
			dto.OpenDate = data.OpenDate.Value;
			dto.CloseDate = data.CloseDate.Value;
			dto.OrganiserContact = data.OrganiserContact.AssembleDto();
			dto.EntryGameFormatID = data.EntryGameFormatID;
			dto.Mode = data.CompetitionRegistrationModeID;
			dto.ShowQualificationDates = data.ShowQualificationDates;
			
			return dto;
		}
		
	}
}