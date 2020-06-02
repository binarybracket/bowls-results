using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class MatchXGame : IdentityEntity<int>
	{
		public virtual CompetitionScopes ScopeID { get; set; }
		public virtual MatchFormatXGameVariation MatchFormatXGameVariation { get; set; }
		public virtual Game.Game Game { get; set; }

		protected virtual Match GetMatch()
		{
			throw new NotImplementedException();
		}
	}
}