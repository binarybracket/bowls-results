using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public interface IPlayerMatchModel : IBaseModel<PlayerMatch, IPlayerResultEngineContext>
	{
		bool GameExists(short matchFormatXGameVariationID);

		Task<Game> AddGame(GameResult gameResult);
		
		Task<Game> UpdateGame(GameResult gameResult);
		PlayerMatchXGame GetGame(short matchFormatXGameVariationID);
	}
}