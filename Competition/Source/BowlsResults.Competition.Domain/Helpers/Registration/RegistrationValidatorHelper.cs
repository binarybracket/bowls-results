using System.Runtime.CompilerServices;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Validation.Failure;
using FluentValidation.Results;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Helpers.Registration
{
	public static class RegistrationValidatorHelper
	{
		public static void Validate(ValidationResult validationResult, Entities.Competition competition)
		{
			ValidateRegistrationOnline(validationResult, competition);
			if (validationResult.IsValid)
			{
				ValidateRegistrationStatus(validationResult, competition);
			}
		}

		public static void ValidateGameFormat(ValidationResult validationResult, Entities.Competition competition, GameFormats gameFormat)
		{
			if (competition.GetEntryGameFormat() != gameFormat)
			{
				validationResult.Errors.Add(new ValidationFailure("GameFormat", "Invalid Game Format."));
			}
		}

		private static void ValidateRegistrationOnline(ValidationResult validationResult, Entities.Competition competition)
		{
			bool unavailable = false;
			if (competition.RegistrationConfiguration != null)
			{
				switch (competition.RegistrationConfiguration.CompetitionRegistrationModeID)
				{
					case CompetitionRegistrationModes.Unavailable:
						unavailable = true;
						break;
					case CompetitionRegistrationModes.Invitational:
						validationResult.Errors.Add(new ValidationFailure("RegistrationStatus", "This competition is an invitational and cannot be entered online."));
						break;
				}
			}
			else
			{
				unavailable = true;
			}

			if (unavailable)
			{
				validationResult.Errors.Add(new ValidationFailure("RegistrationStatus", "This competition cannot be entered online.  Please contact the club directly using the detail below."));
			}
		}

		private static void ValidateRegistrationStatus(ValidationResult validationResult, Entities.Competition competition)
		{
			switch (competition.GetRegistrationStatus())
			{
				case CompetitionRegistrationStatuses.Closed:
					validationResult.Errors.Add(new CompetitionRegistrationClosed(competition.RegistrationConfiguration.CloseDate.Value));
					break;
				case CompetitionRegistrationStatuses.NotOpenYet:
					validationResult.Errors.Add(new CompetitionRegistrationNotOpen(competition.RegistrationConfiguration.OpenDate.Value));
					break;
			}
		}
	}
}