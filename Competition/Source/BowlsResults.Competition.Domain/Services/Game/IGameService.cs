using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game
{
	public interface IGameService
	{
		Task<Entities.Game.Game> Create(IPlayerMatchModel matchModel, GameResult gameResult);
		Task<Entities.Game.Game> Update(IPlayerMatchModel matchModel, GameResult gameResult);
	}

	public interface IGameService<TData> : IGameService
	{
		
	}
}