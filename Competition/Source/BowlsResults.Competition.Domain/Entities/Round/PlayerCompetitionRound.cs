using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Com.BinaryBracket.BowlsResults.Common.Domain.Extensions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round
{
	public class PlayerCompetitionRound : CompetitionRound
	{
		public PlayerCompetitionRound()
		{
			this._fixtures = new HashSet<PlayerFixture>();
		}

		private readonly ISet<PlayerFixture> _fixtures;

		public virtual ReadOnlyCollection<PlayerFixture> Fixtures
		{
			get { return this._fixtures.ToReadOnlyCollection(); }
		}

		public virtual void Add(PlayerFixture playerFixture)
		{
			this._fixtures.Add(playerFixture);
		}

		public virtual PlayerFixture CreateFixture(byte legs, CompetitionEntrant entrant1, CompetitionEntrant entrant2)
		{
			if (legs <= 0 || legs > 2)
			{
				throw new InvalidOperationException("Legs can only be one or two.");
			}

			var fixture = new PlayerFixture();
			fixture.CompetitionID = this.Competition.ID;
			fixture.CompetitionRound = this;
			fixture.Season = this.Season;
			fixture.FixtureStatusID = FixtureStatuses.Incomplete;
			fixture.FixtureCalculationEngineID = this.CompetitionEvent.GetFixtureCalculationEngine();
			fixture.Legs = legs;
			fixture.SetAuditFields();
			fixture.Entrant1 = entrant1;
			fixture.Entrant2 = entrant2;
			this._fixtures.Add(fixture);

			return fixture;
		}

		public virtual PlayerFixture CreatePendingFixture(byte legs, DateTime date)
		{
			if (legs <= 0 || legs > 2)
			{
				throw new InvalidOperationException("Legs can only be one or two.");
			}

			var fixture = new PlayerFixture();
			fixture.CompetitionID = this.Competition.ID;
			fixture.CompetitionRound = this;
			fixture.Season = this.Season;
			fixture.FixtureStatusID = FixtureStatuses.Pending;
			fixture.FixtureCalculationEngineID = this.CompetitionEvent.GetFixtureCalculationEngine();
			fixture.Legs = legs;
			fixture.PendingDate = date;
			fixture.SetAuditFields();			
			this._fixtures.Add(fixture);

			return fixture;
		}
	}
}