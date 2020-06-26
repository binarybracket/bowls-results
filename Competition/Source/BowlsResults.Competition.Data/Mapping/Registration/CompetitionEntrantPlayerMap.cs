using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration
{
	public class CompetitionEntrantPlayerMap : IdentityEntityMap<CompetitionEntrantPlayer, int>
	{
		public CompetitionEntrantPlayerMap()
		{
			this.Table("CompetitionEntrantPlayer");
			this.LazyLoad();

			this.References(x => x.Entrant).Column("CompetitionEntrantID");
			this.References(x => x.Player).Column("PlayerID").Nullable().Cascade.None();
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.FirstName).Column("FirstName").Not.Nullable();
			this.Map(x => x.LastName).Column("LastName").Not.Nullable();
		}
	}
}