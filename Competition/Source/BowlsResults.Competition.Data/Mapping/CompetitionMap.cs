using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class CompetitionMap : AuditableEntityClassMap<Domain.Entities.Competition, int>
	{
		public CompetitionMap()
		{
			this.Table("Competition");
			this.LazyLoad();
			this.Map(x => x.CompetitionTemplateID).Column("CompetitionTemplateID");
			this.Map(x => x.CompetitionOrganiserID).Column("CompetitionOrganiserID");
			this.Map(x => x.CompetitionScopeID).Column("CompetitionScopeID");
			this.Map(x => x.CompetitionFormatID).Column("CompetitionFormatID");
			this.Map(x => x.AgeGroupID).Column("AgeGroupID");
			this.Map(x => x.GenderID).Column("GenderID");
			this.References(x => x.Season).Column("SeasonID");
			this.Map(x => x.AssociationID).Column("AssociationID");
			this.References(x => x.OrganisingClub).Column("OrganisingClubID");
			this.References(x => x.VenueClub).Column("VenueClubID");
			this.Map(x => x.CompetitionHeaderID).Column("CompetitionHeaderID").Not.Nullable();
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.Map(x => x.Sponsor).Column("Sponsor");
			this.Map(x => x.StartDate).Column("StartDate").Not.Nullable();
			this.Map(x => x.EndDate).Column("EndDate");
			this.Map(x => x.PlayerMeritTableCalculationEngineID).Column("PlayerMeritTableCalculationEngineID");
			this.HasMany(x => x.Stages).Cascade.SaveUpdate().Inverse();
		}
	}
}