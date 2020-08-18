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
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Domain2.Email;
using FluentValidation.Internal;
using Microsoft.Extensions.FileProviders;
using MimeKit;
using MimeKit.Utils;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Messages
{
	public class CompetitionRegistrationOrganiserSummaryEmailMessage : IEmailMessage
	{
		private const string AssetBasePath = "Email/Registration/Assets/";

		private static IFileInfo _logoFile;
		private static IFileInfo _okFile;
		private static string _competitionValueTemplate;
		private static string _contactValueTemplate;
		private static string _entrantValueTemplate;
		private static string _registrationConfirmationTemplate;

		private const string PlainTextTemplateClosed = @"Dear %%CONTACT-NAME%%

Entries have closed for the %%COMPETITION-NAME%% competition that you are running.  Below is a summary of all entries received.

%%ENTRANTS-HEADER%%
%%ENTRANTS%%

Competition Details
%%COMPETITION-VALUES%%";
		
		private const string PlainTextTemplateUpdate = @"Dear %%CONTACT-NAME%%

For the %%COMPETITION-NAME%% competition that you are running.  Below is a summary of all entries received to date.

%%ENTRANTS-HEADER%%
%%ENTRANTS%%

Competition Details
%%COMPETITION-VALUES%%";

		private readonly Entities.Competition _competition;
		private readonly List<CompetitionRegistration> _competitionRegistrations;

		static CompetitionRegistrationOrganiserSummaryEmailMessage()
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

			reader = new StreamReader(embeddedProvider.GetFileInfo($"{AssetBasePath}registration-organiser-summary.html").CreateReadStream());
			_registrationConfirmationTemplate = reader.ReadToEnd();
		}

		public CompetitionRegistrationOrganiserSummaryEmailMessage(Entities.Competition competition, List<CompetitionRegistration> competitionRegistrations)
		{
			this._competition = competition;
			this._competitionRegistrations = competitionRegistrations;
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
			return $"Competition Entry Summary ({this._competition.Name})";
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

			var template = PlainTextTemplateUpdate;
			if (this._competition.RegistrationConfiguration.IsClosed())
			{
				template = PlainTextTemplateClosed;
			}
			
			var message = template.Replace("%%CONTACT-NAME%%", this._competition.RegistrationConfiguration.OrganiserContact.DisplayName());
			message = message.Replace("%%COMPETITION-NAME%%", this._competition.Name);
			message = message.Replace("%%ENTRANTS%%", entrants.ToString());
			message = message.Replace("%%COMPETITION-VALUES%%", competitionValues.ToString());
			message = message.Replace("%%ENTRANTS-HEADER%%", entrantsHeader);
			builder.TextBody = message;
		}

		private void BuildHtml(BodyBuilder builder)
		{
			var emailHeader = this.GetEmailHeader();
			var entrants = this.GetHtmlEntrants();
			var competitionValues = this.GetHtmlCompetitionValues();
			var entrantsHeader = this.GetEntrantsHeader();
			var entriesMessage = this.GetEntriesMessageHtml();

			var image1 = builder.LinkedResources.Add("logo.png", _logoFile.CreateReadStream());
			image1.ContentId = MimeUtils.GenerateMessageId();
			var image2 = builder.LinkedResources.Add("ok.gif", _okFile.CreateReadStream());
			image2.ContentId = MimeUtils.GenerateMessageId();

			var file = _registrationConfirmationTemplate.Replace("%%COMPETITION-VALUES%%", competitionValues.ToString());
			file = file.Replace("%%ENTRANTS%%", entrants.ToString());
			file = file.Replace("LOGOIMAGEID", image1.ContentId);
			file = file.Replace("OKIMAGEID", image2.ContentId);
			file = file.Replace("%%ENTRANTS-HEADER%%", entrantsHeader.ToUpperInvariant());
			file = file.Replace("%%COMPETITION-NAME%%", this._competition.Name);
			file = file.Replace("%%EMAIL-HEADER%%", emailHeader);
			file = file.Replace("%%ENTRIES-MESSAGE%%", entriesMessage);
			builder.HtmlBody = file;
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
			var entrants = new StringBuilder();
			var count = 0;
			foreach (var competitionRegistration in this._competitionRegistrations)
			{
				foreach (var competitionEntrant in competitionRegistration.GetPendingOrConfirmedEntrants())
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

					entrants.Append(_entrantValueTemplate.Replace("%%ENTRANT%%", $"{players}"));
				}
			}

			return entrants;
		}

		private StringBuilder GetPlainTextEntrants()
		{
			this.GetEntrantType(out _, out var entrantTypeSingular);
			var count = 0;
			var entrants = new StringBuilder();

			foreach (var competitionRegistration in this._competitionRegistrations)
			{
				foreach (var competitionEntrant in competitionRegistration.GetPendingOrConfirmedEntrants())
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

					entrants.AppendLine($"{entrantTypeSingular} {++count} - " + players);
				}
			}

			return entrants;
		}

		private string GetEntrantsHeader()
		{
			this.GetEntrantType(out var entrantTypePlural, out _);
			return $"Total {this._competitionRegistrations.Sum(x => x.GetPendingOrConfirmedEntrants().ToList().Count)} {entrantTypePlural} entered";
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

		private string GetEmailHeader()
		{
			if (this._competition.RegistrationConfiguration.IsClosed())
			{
				return "Entries Closed";
			}
			return "Entries Update";
		}

		private string GetEntriesMessageHtml()
		{
			if (this._competition.RegistrationConfiguration.IsClosed())
			{
				return "Entries have now <b>closed</b>.  Below is a summary of all the entries received online.";
			}
			return "Below is a summary of all the entries received online to date.";
		}
	}
}