using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class PlayerMatch : Match
	{
		public PlayerMatch()
		{
		}

		public virtual PlayerFixture PlayerFixture { get; set; }
		public virtual Game.Game Game { get; set; }
		public virtual ResultType? ResultTypeID { get; set; }
		public virtual bool Player1Home { get; set; }

		public virtual void SetComplete()
		{
			this.MatchStatusID = MatchStatuses.Complete;
			this.SetAuditFields();
		}

		public virtual void SetIncomplete()
		{
			this.ClearScores();
			this.MatchStatusID = MatchStatuses.Incomplete;			

			this.SetAuditFields();
		}

		public virtual void SetPostponed()
		{
			this.ClearScores();
			this.MatchStatusID = MatchStatuses.Postponed;

			this.SetAuditFields();
		}

		public virtual void SetNoResult()
		{
			this.ClearScores();
			this.MatchStatusID = MatchStatuses.NoResult;

			this.SetAuditFields();
		}
		
		private void ClearScores()
		{
			// NOTE - Will need to talk to game object
			throw new NotImplementedException();
		}
	}
}