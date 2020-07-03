using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;


namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class ContactDtoAssembler
	{
		public static ContactDto AssembleDto(this Contact data)
		{
			var dto = new ContactDto
			{
				Surname = data.Surname,
				Forename = data.Forename,
				DisplayName =  data.DisplayName(),
				Telephone = data.Telephone,
				ID = data.ID,
				EmailAddress = data.EmailAddress
			};

			return dto;
		}
	}
}