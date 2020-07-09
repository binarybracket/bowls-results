using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game
{
	public class GameServiceFactory : IGameServiceFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public GameServiceFactory(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}
		
		public IGameService Create(MatchFormatXGameVariation matchConfiguration)
		{
			switch (matchConfiguration.GameVariation.GameFormatID)
			{
				case GameFormats.Singles:
					return this._serviceProvider.GetService<IGameService<SinglesGame>>();
					break;
				case GameFormats.Doubles:
				case GameFormats.Threesomes:
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public interface IGameServiceFactory
	{
		IGameService Create(MatchFormatXGameVariation matchConfiguration);
	}
}