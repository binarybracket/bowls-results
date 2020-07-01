using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration {
    
    
    public class CompetitionRegistrationMap : IdentityEntityMap<CompetitionRegistration, int> {
        
        public CompetitionRegistrationMap() {
			this.Table("CompetitionRegistration");
			this.LazyLoad();
			
			//this.References(x => x.Competition).Column("CompetitionID").Not.Nullable().Cascade.	None();
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.EmailAddress).Column("EmailAddress").Not.Nullable();
			this.Map(x => x.Forename).Column("Forename").Not.Nullable();
			this.Map(x => x.Surname).Column("Surname").Not.Nullable();
			this.HasMany(x => x.Entrants).KeyColumn("CompetitionRegistrationID").Inverse().Cascade.SaveUpdate();
        }
    }
}
