using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public sealed class PlayerFixtureModel : BaseModel<PlayerFixture, IPlayerResultEngineContext>, IPlayerFixtureModel
	{
		private readonly IServiceProvider _serviceProvider;

		public PlayerFixtureModel(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}

		public IPlayerMatchModel GetMatch(int id)
		{
			var matchModel = this._serviceProvider.GetService<IPlayerMatchModel>();
			var match = this.Data.GetMatchByID(id);
			matchModel.Initialise(match, this.Context);
			return matchModel;
		}

		public bool IsMatchProcessed(int id)
		{
			return this.Data.GetMatchByID(id).MatchStatusID.IsProcessed();
		}
	}
}