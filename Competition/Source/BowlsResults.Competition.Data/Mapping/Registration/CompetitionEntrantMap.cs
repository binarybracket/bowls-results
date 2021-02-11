using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration {
    
    
    public class CompetitionEntrantMap : IdentityEntityMap<CompetitionEntrant, int> {
        
        public CompetitionEntrantMap() {
			this.Table("CompetitionEntrant");
			this.LazyLoad();
			
			this.References(x => x.CompetitionRegistration).Column("CompetitionRegistrationID").Nullable();
			this.Map(x => x.CompetitionDateID).Column("CompetitionDateID").Nullable();
			this.Map(x => x.CompetitionEntrantStatusID).Column("CompetitionEntrantStatusID").Not.Nullable();
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.EntrantGameFormatID).Column("EntrantGameFormatID").Not.Nullable();
			this.HasMany(x => x.Players).KeyColumn("CompetitionEntrantID").Inverse().AsSet().Cascade.SaveUpdate();
        }
    }
}
