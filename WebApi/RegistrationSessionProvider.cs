using System;
using System.Collections.Generic;
using System.IO;
using Com.BinaryBracket.BowlsResults.Common.Data.Mappings;
using Com.BinaryBracket.BowlsResults.Competition.Data.Mapping;
using Com.BinaryBracket.Core.Data2.SessionProvider.Conventions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.Logging;
using NHibernate;

namespace BowlsResults.WebApi
{
	public class RegistrationSessionProvider : IRegistrationSessionProvider
	{
		private static ISessionFactory _sessionFactory;
		private static NHibernate.Cfg.Configuration _nHibernateConfiguration;

		private ISession _session;

		public RegistrationSessionProvider(ILogger<TestAppSessionProvider> logger)
		{
			logger.LogWarning("AAA");
		}

		public ISession Session
		{
			get
			{
				if (_sessionFactory == null)
				{
					throw new NotImplementedException("Session Factory has not been initialised");
				}

				if (this._session == null)
				{
					this._session = _sessionFactory.OpenSession();
				}

				return this._session;
			}
		}

		public void Dispose()
		{
			this._session?.Dispose();
		}

		public static void Initialise(string connectionString)
		{
			if (_sessionFactory != null)
			{
				throw new NotImplementedException("Session Factory already initialised");
			}

			NHibernate.Cfg.Configuration configuration = null;

			var fluentConfig = Fluently.Configure()
				.Database(
					MsSqlConfiguration
						.MsSql2008
						.ConnectionString(connectionString)
						.AdoNetBatchSize(100))
				//.Cache(cache => cache.ProviderClass<NHibernate.Caches.SysCache.SysCacheProvider>().UseQueryCache().UseSecondLevelCache().UseMinimalPuts())
				.ExposeConfiguration(cfg => configuration = cfg);

			var mappings = GetMappingConfigurations();
			fluentConfig.Mappings(m => m.FluentMappings.Conventions.Setup(x => x.Add(AutoImport.Never())));
			foreach (Action<MappingConfiguration> mapping in mappings)
			{
				fluentConfig.Mappings(mapping);
			}

			_sessionFactory = fluentConfig.BuildSessionFactory();
			_nHibernateConfiguration = configuration;
		}

		public static IEnumerable<Action<MappingConfiguration>> GetMappingConfigurations()
		{
			var mappings = new List<Action<MappingConfiguration>>();

			mappings.Add(GetMappingConfiguration<CompetitionMap>());
			mappings.Add(GetHbmMappingConfiguration<CompetitionMap>());

			mappings.Add(GetMappingConfiguration<ClubMap>());
			mappings.Add(GetHbmMappingConfiguration<ClubMap>());

			return mappings;
		}


		public static Action<MappingConfiguration> GetMappingConfiguration<TType>()
		{
#if DEBUG
			string schemaExportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MappingsExport");
			if (!Directory.Exists(schemaExportPath))
			{
				Directory.CreateDirectory(schemaExportPath);
			}
#endif

			return configuration => configuration.FluentMappings.AddFromAssemblyOf<TType>()
					.Conventions.Add(new IConvention[]
					{
						new CustomEnumConvention(),
						new CustomForeignKeyConvention(), 
					})
#if DEBUG
					.ExportTo(schemaExportPath)
#endif
				;
		}

		public static Action<MappingConfiguration> GetHbmMappingConfiguration<TType>()
		{
			return configuration => configuration.HbmMappings.AddFromAssemblyOf<TType>();
		}
	}
}