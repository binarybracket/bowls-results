using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class Match : AuditableEntity<short>
	{
		public virtual short MatchFormatID { get; set; }
		public virtual MatchCalculationEngines MatchCalculationEngineID { get; set; }
		public virtual MatchStatuses MatchStatusID { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual byte Leg { get; set; }
		public virtual int? Sequence { get; set; }
		
		public virtual string DataString1 { get; set; }
		public virtual string DataString2 { get; set; }
	}
}