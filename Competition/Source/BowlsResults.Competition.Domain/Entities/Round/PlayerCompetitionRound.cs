using System.Collections.Generic;
using System.Collections.ObjectModel;
using Com.BinaryBracket.BowlsResults.Common.Domain.Extensions;
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
			get
			{
				return this._fixtures.ToReadOnlyCollection();
			}
		}

		public virtual void Add(PlayerFixture playerFixture)
		{
			this._fixtures.Add(playerFixture);
		}
	}
}