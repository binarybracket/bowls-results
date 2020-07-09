using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Data2.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public sealed class PlayerFixtureModel : BaseModel<PlayerFixture, IPlayerResultEngineContext>, IPlayerFixtureModel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IFixtureCalculationEngineFactory _fixtureCalculationEngineFactory;

		public PlayerFixtureModel(IServiceProvider serviceProvider, IFixtureCalculationEngineFactory fixtureCalculationEngineFactory)
		{
			this._serviceProvider = serviceProvider;
			this._fixtureCalculationEngineFactory = fixtureCalculationEngineFactory;
		}

		public IPlayerMatchModel GetMatch(int id)
		{
			var matchModel = this.GetMatchModel(this.Data.GetMatchByID(id));
			return matchModel;
		}

		public bool IsMatchProcessed(int id)
		{
			return this.Data.GetMatchByID(id).MatchStatusID.IsProcessed();
		}

		public void CalculateFixture()
		{
			switch (this.Context.CompetitionEvent.CompetitionEventTypeID)
			{
				case CompetitionEventTypes.Knockout:
					var engine = this._fixtureCalculationEngineFactory.Create(this.Context.CompetitionEvent.GetFixtureCalculationEngine());
					engine.Calculate(this.Data);
					break;
				case CompetitionEventTypes.League:
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void CalculateMatches()
		{
			foreach (var playerMatch in this.Data.Matches)
			{
				var model = this.GetMatchModel(playerMatch);
				model.CalculateResultFromGames();
			}
		}

		private IPlayerMatchModel GetMatchModel(PlayerMatch data)
		{
			var matchModel = this._serviceProvider.GetService<IPlayerMatchModel>();
			matchModel.Initialise(data, this.Context);
			return matchModel;
		}
	}
}