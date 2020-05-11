using BowlsResults.Common.Domain.Models;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace BowlsResults.Competition.Domain.Models
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
	//	}
		
	}
}