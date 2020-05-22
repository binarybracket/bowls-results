using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game
{
    /// <summary>
    /// AwayPlayer Domain Object
    /// </summary>
    public class AwayPlayer : GameXPlayer
    {
        public AwayPlayer()
        {
            this.SideID = Sides.Away;
        }
    }
}