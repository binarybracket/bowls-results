﻿using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionHeaderRepository : IdentityRepository<CompetitionHeader, int>, ICompetitionHeaderRepository
	{
		public CompetitionHeaderRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}