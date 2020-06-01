using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class CompetitionEvent : IdentityEntity<short>
	{
		public CompetitionEvent()
		{
			//this.InternalCompetitionRound= new List<CompetitionRound>();
		}

		public virtual Competition Competition { get; set; }
		public virtual CompetitionEventTypes CompetitionEventTypeID { get; set; }
		public virtual CompetitionStage CompetitionStage { get; set; }
		public virtual Season Season { get; set; }
		public virtual int? SaveResultRuleSetID { get; set; }
		public virtual int? DataInt1 { get; set; }

		public virtual int? DataInt2 { get; set; }
		//protected internal virtual IList<CompetitionRound> InternalCompetitionRounds { get; set; }

		//public virtual ReadOnlyCollection<CompetitionStage> Stages
		//	{
		//		get { return new ReadOnlyCollection<CompetitionStage>(this.InternalCompetitionRounds); }
		//	

		public virtual MatchFormat GetMatchFormat()
		{
			throw new NotImplementedException();
		}
		
		public virtual T CreateRound<T>(CompetitionRoundTypes competitionRoundTypeID, byte gameNumber) where T : CompetitionRound, new()
		{
			var round = new T();
			round.Competition = this.Competition;
			round.CompetitionEvent = this;
			round.Season = this.Competition.Season;
			round.CompetitionRoundTypeID = competitionRoundTypeID;
			round.GameNumber = gameNumber;
			round.CompetitionScopeID = this.Competition.CompetitionScopeID;
			return round;
		}
	}
}