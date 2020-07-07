using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class Match : AuditableEntity<short>
	{
		public virtual CompetitionScopes ScopeID { get; set; }
		public virtual MatchFormat MatchFormat { get; set; }
		public virtual MatchStatuses MatchStatusID { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual byte Leg { get; set; }
		public virtual int PitchID { get; set; }
		public virtual VenueTypes VenueTypeID { get; set; }
		public virtual byte? HomeChalkHandicap { get; set; }
		public virtual byte? AwayChalkHandicap { get; set; }
		public virtual short? HomeGameScore { get; set; }
		public virtual short? AwayGameScore { get; set; }
		public virtual short? HomeBonusScore { get; set; }
		public virtual short? AwayBonusScore { get; set; }
		public virtual short? HomeChalkScore { get; set; }
		public virtual short? AwayChalkScore { get; set; }
		public virtual bool? HomeWalkover { get; set; }
		public virtual bool? AwayWalkover { get; set; }
		public virtual ResultType? HomeResultTypeID { get; set; }
		public virtual ResultType? AwayResultTypeID { get; set; }
		public virtual MatchCalculationEngines MatchCalculationEngineID { get; set; }
		public virtual int? Sequence { get; set; }
		public virtual string DataString1 { get; set; }
		public virtual string DataString2 { get; set; }
		
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
		
		public virtual void ClearScores()
		{
			this.MatchStatusID = MatchStatuses.Incomplete;
			this.HomeGameScore = null;
			this.AwayGameScore = null;
			this.HomeBonusScore = null;
			this.AwayBonusScore = null;
			this.HomeChalkScore = null;
			this.AwayChalkScore = null;
			this.HomeWalkover = null;
			this.AwayWalkover = null;
			this.HomeResultTypeID = null;
			this.AwayResultTypeID = null;

			this.SetAuditFields();
		}
	}
}