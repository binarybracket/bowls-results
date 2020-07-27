using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetitionHeader
{
	public sealed class CreateCompetitionHeaderCommand : ICommand<DefaultIdentityCommandResponse>
	{
		public string Name { get; set; }
		public string ShortName { get; set; }
		public int AssociationID { get; set; }
		public int Priority { get; set; }
	}
}