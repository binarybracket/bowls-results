using System.Collections.Generic;
using System.Linq;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;


namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class ContactDtoAssembler
	{
		public static List<ContactDto> AssembleDtoList(this IEnumerable<Contact> contacts)
		{
			var list = new List<ContactDto>();

			foreach (var contact in contacts.OrderBy(x => x.ContactTypeID))
			{
				list.Add(contact.AssembleDto());
			}

			return list;
		}

		public static ContactDto AssembleDto(this Contact data)
		{
			var dto = new ContactDto
			{
				Surname = data.Surname,
				Forename = data.Forename,
				DisplayName = data.DisplayName(),
				Telephone = data.Telephone,
				ID = data.ID,
				EmailAddress = data.EmailAddress,
				ContactTypeID = data.ContactTypeID
			};

			return dto;
		}
	}
}