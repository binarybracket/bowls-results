using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Commands.Contact;
using Com.BinaryBracket.BowlsResults.Common.Domain.Commands.Contact.Validators;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.CommandHandlers;
using Com.BinaryBracket.Core.Domain2.Commands;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.CommandHandlers.Contact
{
	public sealed class CreateContactCommandHandler : ICommandHandler<CreateContactCommand, DefaultIdentityCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<CreateContactCommandHandler> _logger;
		private readonly CreateContactCommandValidator _validator;
		private readonly IContactRepository _contactRepository;
		private readonly IClubRepository _clubRepository;
		private readonly IAssociationRepository _associationRepository;
		
		private ValidationResult _validationResult;
		private Entities.Contact _contact;
		private Club _club;
		private Association _association;

		public CreateContactCommandHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, CreateContactCommandValidator validator, IContactRepository contactRepository, IClubRepository clubRepository, IAssociationRepository associationRepository)
		{
			this._logger = loggerFactory.CreateLogger<CreateContactCommandHandler>();
			this._unitOfWork = unitOfWork;
			this._validator = validator;
			this._contactRepository = contactRepository;
			this._clubRepository = clubRepository;
			this._associationRepository = associationRepository;
		}
		
		public async Task<DefaultIdentityCommandResponse> Handle(CreateContactCommand command)
		{
			this._unitOfWork.Begin();

			try
			{
				this._validationResult = this._validator.Validate(command);

				if (this._validationResult.IsValid)
				{
					await this.Load(command);
					if (this._contact == null)
					{
						this._contact = new Entities.Contact();
						this._contact.ContactTypeID = command.ContactTypeID;
					}

					this._contact.Forename = command.Forename;
					this._contact.Surname = command.Surname;
					this._contact.EmailAddress = command.EmailAddress;
					this._contact.Telephone = command.Telephone;

					await this._contactRepository.Save(this._contact);

					if (command.ClubID.HasValue)
					{
						this._club.AddContact(this._contact);
						await this._clubRepository.Save(this._club);
					}
					if (command.AssociationID.HasValue)
					{
						this._association.AddContact(this._contact);
						await this._associationRepository.Save(this._association);
					}
				}

				if (this._validationResult.IsValid)
				{
					this._unitOfWork.SoftCommit();
					return DefaultIdentityCommandResponse.Create(this._validationResult, this._contact.ID);
				}
				else
				{
					this._unitOfWork.Rollback();
					return DefaultIdentityCommandResponse.Create(this._validationResult);
				}
			}
			catch (Exception exception)
			{
				this._unitOfWork.Rollback();
				this._logger.LogError(exception, "Exception In Command Handler");
				throw;
			}
		}

		private async Task Load(CreateContactCommand command)
		{
			this._contact = await this._contactRepository.GetByContactTypeAndEmail(command.ContactTypeID, command.EmailAddress);
			if (command.ClubID.HasValue)
			{
				this._club = await this._clubRepository.GetWithContacts(command.ClubID.Value);
			}
			if (command.AssociationID.HasValue)
			{
				this._association = await this._associationRepository.GetWithContacts(command.AssociationID.Value);
			}
		}
	}
}