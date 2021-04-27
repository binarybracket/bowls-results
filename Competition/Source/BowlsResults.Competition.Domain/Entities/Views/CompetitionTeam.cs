using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Views
{
	public class CompetitionTeam
	{
		public virtual Team Team { get; set; }
		public virtual int CompetitionHeaderID { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual int SeasonID { get; set; }
		
		public override bool Equals(object obj)
		{
			return this.Team.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.Team.GetHashCode();
		}
	}
}