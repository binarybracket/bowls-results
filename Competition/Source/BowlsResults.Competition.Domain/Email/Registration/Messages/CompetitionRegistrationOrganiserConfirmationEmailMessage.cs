using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Email;
using FluentValidation.Internal;
using Microsoft.Extensions.FileProviders;
using MimeKit;
using MimeKit.Utils;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Messages
{
	public class CompetitionRegistrationOrganiserConfirmationEmailMessage : IEmailMessage
	{
		private const string AssetBasePath = "Email/Registration/Assets/";

		private static IFileInfo _logoFile;
		private static IFileInfo _okFile;
		private static string _competitionValueTemplate;
		private static string _contactValueTemplate;
		private static string _entrantValueTemplate;
		private static string _registrationConfirmationTemplate;

		private const string PlainTextTemplate = @"Dear %%CONTACT-NAME%%

You have received an entry into the competition you are running %%COMPETITION-NAME%%

%%ENTRANTS-HEADER%%
%%ENTRANTS%%

This entry was submitted by
%%CONTACT-VALUES%%

Competition Details
%%COMPETITION-VALUES%%";

		private readonly Entities.Competition _competition;
		private readonly CompetitionRegistration _competitionRegistration;
		private readonly List<CompetitionDate> _dates;

		static CompetitionRegistrationOrganiserConfirmationEmailMessage()
		{
			var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
			_logoFile = embeddedProvider.GetFileInfo($"{AssetBasePath}images/logo.png");
			_okFile = embeddedProvider.GetFileInfo($"{AssetBasePath}images/okok.gif");

			var reader = new StreamReader(embeddedProvider.GetFileInfo($"{AssetBasePath}competition-value.html").CreateReadStream());
			_competitionValueTemplate = reader.ReadToEnd();

			reader = new StreamReader(embeddedProvider.GetFileInfo($"{AssetBasePath}contact-value.html").CreateReadStream());
			_contactValueTemplate = reader.ReadToEnd();

			reader = new StreamReader(embeddedProvider.GetFileInfo($"{AssetBasePath}entrant-value.html").CreateReadStream());
			_entrantValueTemplate = reader.ReadToEnd();

			reader = new StreamReader(embeddedProvider.GetFileInfo($"{AssetBasePath}registration-organiser-confirmation.html").CreateReadStream());
			_registrationConfirmationTemplate = reader.ReadToEnd();
		}

		public CompetitionRegistrationOrganiserConfirmationEmailMessage(Entities.Competition competition, CompetitionRegistration competitionRegistration, List<CompetitionDate> dates)
		{
			this._competition = competition;
			this._competitionRegistration = competitionRegistration;
			this._dates = dates;
		}

		public MailboxAddress GetFrom()
		{
			return new MailboxAddress("Matthew Keggen", "compsec@iombowls.com");
		}

		public InternetAddressList GetTo()
		{
			var list = new InternetAddressList();
			list.Add(new MailboxAddress(this._competition.RegistrationConfiguration.OrganiserContact.DisplayName(), this._competition.RegistrationConfiguration.OrganiserContact.EmailAddress));
			return list;
		}

		public InternetAddressList GetCc()
		{
			return null;
		}

		public InternetAddressList GetBcc()
		{
			return null;
		}

		public string GetSubject()
		{
			return $"Competition Entry Received ({this._competition.Name})";
		}

		public BodyBuilder GetBodyBuilder()
		{
			var builder = new BodyBuilder();

			this.BuildHtml(builder);
			this.BuildPlainText(builder);

			return builder;
		}

		private void BuildPlainText(BodyBuilder builder)
		{
			var entrants = this.GetPlainTextEntrants();
			var competitionValues = this.GetPlainTextCompetitionValues();
			var entrantsHeader = this.GetEntrantsHeader();
			var contactValues = this.GetPlainTextContactValues();

			var message = PlainTextTemplate.Replace("%%CONTACT-NAME%%", this._competition.RegistrationConfiguration.OrganiserContact.DisplayName());
			message = message.Replace("%%COMPETITION-NAME%%", this._competition.Name);
			message = message.Replace("%%ENTRANTS%%", entrants.ToString());
			message = message.Replace("%%COMPETITION-VALUES%%", competitionValues.ToString());
			message = message.Replace("%%ENTRANTS-HEADER%%", entrantsHeader);
			message = message.Replace("%%CONTACT-VALUES%%", contactValues.ToString());
			builder.TextBody = message;
		}

		private void BuildHtml(BodyBuilder builder)
		{
			var entrants = this.GetHtmlEntrants();
			var competitionValues = this.GetHtmlCompetitionValues();
			var contactValues = this.GetHtmlContactValues();
			var entrantsHeader = this.GetEntrantsHeader();

			var image1 = builder.LinkedResources.Add("logo.png", _logoFile.CreateReadStream());
			image1.ContentId = MimeUtils.GenerateMessageId();
			var image2 = builder.LinkedResources.Add("ok.gif", _okFile.CreateReadStream());
			image2.ContentId = MimeUtils.GenerateMessageId();

			var file = _registrationConfirmationTemplate.Replace("%%COMPETITION-VALUES%%", competitionValues.ToString());
			file = file.Replace("%%CONTACT-VALUES%%", contactValues.ToString());
			file = file.Replace("%%ENTRANTS%%", entrants.ToString());
			file = file.Replace("LOGOIMAGEID", image1.ContentId);
			file = file.Replace("OKIMAGEID", image2.ContentId);
			file = file.Replace("%%ENTRANTS-HEADER%%", entrantsHeader.ToUpperInvariant());
			file = file.Replace("%%COMPETITION-NAME%%", this._competition.Name);
			builder.HtmlBody = file;
		}

		private StringBuilder GetHtmlContactValues()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(_contactValueTemplate.Replace("%%CONTACT-KEY%%", "Name").Replace("%%CONTACT-VALUE%%", this._competitionRegistration.DisplayName()));
			builder.Append(_contactValueTemplate.Replace("%%CONTACT-KEY%%", "Email").Replace("%%CONTACT-VALUE%%", this._competitionRegistration.EmailAddress));

			return builder;
		}

		private StringBuilder GetPlainTextContactValues()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine($"Name - {this._competitionRegistration.DisplayName()}");
			builder.AppendLine($"Email - {this._competitionRegistration.EmailAddress}");
			
			return builder;
		}
		
		private StringBuilder GetHtmlCompetitionValues()
		{
			StringBuilder builder = new StringBuilder();

			var ukCulture = CultureInfo.CreateSpecificCulture("en-GB");
			var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
			DateTime newTime = TimeZoneInfo.ConvertTimeFromUtc(this._competition.StartDate, britishZone);

			builder.Append(_competitionValueTemplate.Replace("%%COMPETITION-KEY%%", "Date").Replace("%%COMPETITION-VALUE%%", newTime.ToString("f")));
			builder.Append(_competitionValueTemplate.Replace("%%COMPETITION-KEY%%", "Venue").Replace("%%COMPETITION-VALUE%%", this._competition.VenueClub.Name));
			builder.Append(_competitionValueTemplate.Replace("%%COMPETITION-KEY%%", "Format").Replace("%%COMPETITION-VALUE%%", this._competition.GameVariation.Name));

			if (this._competition.RegistrationConfiguration.Amount.HasValue)
			{
				builder.Append(_competitionValueTemplate.Replace("%%COMPETITION-KEY%%", "Entry Fee").Replace("%%COMPETITION-VALUE%%", $"£" + String.Format("{0:n}", this._competition.RegistrationConfiguration.Amount)));
			}

			return builder;
		}

		private object GetPlainTextCompetitionValues()
		{
			StringBuilder builder = new StringBuilder();

			var ukCulture = CultureInfo.CreateSpecificCulture("en-GB");
			var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
			DateTime newTime = TimeZoneInfo.ConvertTimeFromUtc(this._competition.StartDate, britishZone);

			builder.Append("Date - ");
			builder.Append(newTime.ToString("f"));
			builder.AppendLine();

			builder.Append("Venue - ");
			builder.Append(this._competition.VenueClub.Name);
			builder.AppendLine();

			builder.Append("Format - ");
			builder.Append(this._competition.GameVariation.Name);
			builder.AppendLine();

			if (this._competition.RegistrationConfiguration.Amount.HasValue)
			{
				builder.Append("Entry Fee - ");
				builder.Append($"£" + String.Format("{0:n}", this._competition.RegistrationConfiguration.Amount));
				builder.AppendLine();
			}

			return builder;
		}

		private StringBuilder GetHtmlEntrants()
		{
			this.GetEntrantType(out _, out var entrantTypeSingular);
			var entrants = new StringBuilder();
			var count = 0;
			foreach (var competitionEntrant in this._competitionRegistration.Entrants)
			{
				var players = new StringBuilder();
				var first = true;
				foreach (var player in competitionEntrant.Players)
				{
					if (!first)
					{
						players.Append(" & ");
					}
					players.Append(player.DisplayName());
					first = false;
				}

				if (competitionEntrant.CompetitionDateID != null)
				{
					var competitionDate = this._dates.FirstOrDefault(x => x.ID == competitionEntrant.CompetitionDateID);
					if (competitionDate != null)
					{
						players.Append($" ({competitionDate.Description})");	
					}
				}
				
				entrants.Append(_entrantValueTemplate.Replace("%%ENTRANT%%", $"{entrantTypeSingular} {++count} - {players}"));
			}

			return entrants;
		}

		private StringBuilder GetPlainTextEntrants()
		{
			this.GetEntrantType(out _, out var entrantTypeSingular);
			var count = 0;
			var entrants = new StringBuilder();
			foreach (var competitionEntrant in this._competitionRegistration.Entrants)
			{
				var players = new StringBuilder();
				var first = true;
				foreach (var player in competitionEntrant.Players)
				{
					if (!first)
					{
						players.Append(" & ");
					}
					players.Append(player.DisplayName());
					first = false;
				}
				
				if (competitionEntrant.CompetitionDateID != null)
				{
					var competitionDate = this._dates.FirstOrDefault(x => x.ID == competitionEntrant.CompetitionDateID);
					if (competitionDate != null)
					{
						players.Append($" ({competitionDate.Description})");	
					}
				}

				entrants.AppendLine($"{entrantTypeSingular} {++count} - " + players);
			}

			return entrants;
		}

		private string GetEntrantsHeader()
		{
			this.GetEntrantType(out var entrantTypePlural, out _);
			return $"Entering {this._competitionRegistration.Entrants.Count} {entrantTypePlural}";
		}

		private void GetEntrantType(out string plural, out string singular)
		{
			plural = "Players";
			singular = "Player";
			if (this._competition.GetEntryGameFormat() != GameFormats.Singles)
			{
				plural = "Teams";
				singular = "Team";
			}
		}
	}
}