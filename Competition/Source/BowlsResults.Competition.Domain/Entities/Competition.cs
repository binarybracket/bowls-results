using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class Competition : AuditableEntity<int>
	{
		public Competition()
		{
			//this.InternalStages = new List<CompetitionStage>();
			this.Stages = new List<CompetitionStage>();
		}

		public virtual int? CompetitionTemplateID { get; set; }
		public virtual CompetitionOrganisers CompetitionOrganiserID { get; set; }
		public virtual CompetitionScopes CompetitionScopeID { get; set; }
		public virtual CompetitionFormats CompetitionFormatID { get; set; }
		public virtual AgeGroups AgeGroupID { get; set; }
		public virtual Genders GenderID { get; set; }
		public virtual Season Season { get; set; }
		public virtual int AssociationID { get; set; }
		public virtual Club OrganisingClub { get; set; }
		public virtual Club VenueClub { get; set; }
		public virtual Pitch VenuePitch { get; set; }
		public virtual int CompetitionHeaderID { get; set; }
		public virtual string Name { get; set; }
		public virtual string Sponsor { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }

		public virtual int? PlayerMeritTableCalculationEngineID { get; set; }
		public virtual GameVariation GameVariation { get; set; }

//		protected internal virtual IList<CompetitionStage> InternalStages { get; set; }
		public virtual IList<CompetitionStage> Stages { get; set; }

		public virtual CompetitionRegistrationConfiguration RegistrationConfiguration { get; set; }
		
//		public virtual ReadOnlyCollection<CompetitionStage> Stages
//		{
//			get { return new ReadOnlyCollection<CompetitionStage>(this.InternalStages); }
//		}

		public virtual CompetitionRegistrationStatuses GetRegistrationStatus()
		{
			var status = CompetitionRegistrationStatuses.Unavailable;

			if (this.RegistrationConfiguration != null && this.RegistrationConfiguration.CompetitionRegistrationModeID == CompetitionRegistrationModes.Online)
			{
				return this.RegistrationConfiguration.CalculateStatus();
			}

			return status;
		}

		public static Competition Create(CompetitionHeader header, Season season, CompetitionOrganisers organiser, CompetitionScopes scope, CompetitionFormats format, AgeGroups ageGroup, Genders gender, int associationID, string name,
			DateTime startDate,
			DateTime? endDate)
		{
			var data = new Competition
			{
				CompetitionHeaderID = header.ID,
				Season = season,
				CompetitionOrganiserID = organiser,
				CompetitionScopeID = scope,
				CompetitionFormatID = format,
				AgeGroupID = ageGroup,
				GenderID = gender,
				AssociationID = associationID,
				Name = name,
				StartDate = startDate,
				EndDate = endDate
			};

			return data;
		}

		public virtual CompetitionStage CreateStage(CompetitionStageFormats format, string name, byte sequence)
		{
			var data = new CompetitionStage
			{
				Competition = this,
				CompetitionStageFormatID = format,
				Name = name,
				Sequence = sequence
			};

			this.Stages.Add(data);

			return data;
		}

		public virtual void CreateRegistrationConfiguration(CompetitionRegistrationModes mode, Contact contact)
		{
			this.RegistrationConfiguration = new CompetitionRegistrationConfiguration();
			this.RegistrationConfiguration.Competition = this;
			this.RegistrationConfiguration.CompetitionRegistrationModeID = mode;
			this.RegistrationConfiguration.OrganiserContact = contact;
		}
		
		public virtual CompetitionRegistration CreateRegistration(string forename, string surname, string emailAddress)
		{
			var data = new CompetitionRegistration();
			data.CompetitionID = this.ID;
			data.Forename = forename;
			data.Surname = surname;
			data.EmailAddress = emailAddress;
			return data;
		}

		public virtual CompetitionStage GetStageByID(int id)
		{
			return this.Stages.Single(x => x.ID == id);
		}
		
		public virtual CompetitionStage GetStageBySequence(int sequence)
		{
			return this.Stages.Single(x => x.Sequence == sequence);
		}

		public virtual CompetitionStage GetStage(CompetitionLookupModel.CompetitionStageLookupModes mode, int? competitionCompetitionStageValue)
		{
			switch (mode)
			{
				case CompetitionLookupModel.CompetitionStageLookupModes.Auto:
					return this.Stages.Single();
					break;
				case CompetitionLookupModel.CompetitionStageLookupModes.ByID:
					if (competitionCompetitionStageValue == null) throw new ArgumentNullException(nameof(competitionCompetitionStageValue));
					return this.GetStageByID(competitionCompetitionStageValue.Value);
					break;
				case CompetitionLookupModel.CompetitionStageLookupModes.BySequence:
					if (competitionCompetitionStageValue == null) throw new ArgumentNullException(nameof(competitionCompetitionStageValue));
					return this.GetStageBySequence(competitionCompetitionStageValue.Value);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
			}
		}

		public virtual GameFormats GetEntryGameFormat()
		{
			var competitionGameFormat = this.GameVariation.GameFormatID;
			if (this.RegistrationConfiguration.EntryGameFormatID.HasValue)
			{
				competitionGameFormat = this.RegistrationConfiguration.EntryGameFormatID.Value;
			}

			return competitionGameFormat;
		}
	}
}