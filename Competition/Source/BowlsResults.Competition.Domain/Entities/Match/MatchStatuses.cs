using System;
using System.Collections.Generic;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public enum MatchStatuses
	{
		Incomplete = 1, // Not processed
		Temporary = 2, // Processed
		Overridden = 3, // Processed
		Complete = 4, // Processed
		Postponed = 5, // Processed
		NoResult = 6, // Processed
		Review = 7, // Processed
		Locked = 8, // Processed
		Pending = 9 // Pending
	}

	public static class MatchStatus
	{
		private static readonly HashSet<MatchStatuses> ProcessedStatuses = new HashSet<MatchStatuses>
		{
			MatchStatuses.Temporary,
			MatchStatuses.Overridden,
			MatchStatuses.Complete,
			MatchStatuses.Postponed,
			MatchStatuses.NoResult,
			MatchStatuses.Review,
			MatchStatuses.Locked
		};

		private static readonly HashSet<MatchStatuses> CompletedStatuses = new HashSet<MatchStatuses>
		{
			MatchStatuses.Overridden,
			MatchStatuses.Complete,
			MatchStatuses.Locked
		};

		private static readonly HashSet<MatchStatuses> HasResultStatuses = new HashSet<MatchStatuses>
		{
			MatchStatuses.Temporary,
			MatchStatuses.Overridden,
			MatchStatuses.Complete,
			MatchStatuses.Review,
			MatchStatuses.Locked
		};
		
		public static bool IsPending(this MatchStatuses matchStatus)
		{
			return matchStatus == MatchStatuses.Pending;
		}
		
		public static bool IsIncomplete(this MatchStatuses matchStatus)
		{
			return matchStatus == MatchStatuses.Incomplete;
		}

		public static bool IsProcessed(this MatchStatuses matchStatus)
		{
			return ProcessedStatuses.Contains(matchStatus);
		}

		public static bool IsComplete(this MatchStatuses matchStatus)
		{
			return CompletedStatuses.Contains(matchStatus);
		}

		public static bool IsProcessedWithResult(this MatchStatuses matchStatus)
		{
			return ProcessedStatuses.Contains(matchStatus) && HasResultStatuses.Contains(matchStatus);
		}

		public static bool IsFullResult(this MatchStatuses matchStatus)
		{
			return matchStatus == MatchStatuses.Complete;
		}

		public static void GuardCheckProcessedWithResult(this MatchStatuses matchStatus)
		{
			if (!matchStatus.IsProcessedWithResult())
			{
				throw new InvalidOperationException("Match does not have a result");
			}
		}
	}
}