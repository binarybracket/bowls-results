using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class CompetitionDateDto
	{
		public int ID { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public bool Qualifier { get; set; }
		public CompetitionDateStatuses CompetitionDateStatusID { get; set; }
	}
}