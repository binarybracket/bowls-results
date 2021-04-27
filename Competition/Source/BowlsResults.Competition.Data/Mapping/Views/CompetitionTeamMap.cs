using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Views;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Views
{
	public class CompetitionTeamMap : ClassMap<CompetitionTeam>
	{
		public CompetitionTeamMap()
		{
			this.Table("CompetitionXTeam");
			this.ReadOnly();

			this.CompositeId()
				.KeyReference(x => x.Team, "TeamID")
				.KeyProperty(x => x.CompetitionID);

			//this.References(x => x.Team).Column("TeamID");
			//this.Map(x => x.CompetitionID).Column("CompetitionID");
			this.Map(x => x.CompetitionHeaderID).Column("CompetitionHeaderID");
			this.Map(x => x.SeasonID).Column("SeasonID");
		}
	}
}