using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BowlsResults.WebApi.Jobs;
using Com.BinaryBracket.BowlsResults.Common.Data.Repository;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Data.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using Com.BinaryBracket.Core.Domain2;
using Com.BinaryBracket.Core.Domain2.Email;
using Com.BinaryBracket.Core.Domain2.reCAPTCHA;
using Com.BinaryBracket.Core.Domain2.reCAPTCHA.Gateway;
using Com.BinaryBracket.Core.Job.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BowlsResults.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
			
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				//.WriteTo.Console()
				//.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("BOB",
					builder =>
					{
						builder.WithOrigins("http://localhost:8080", "http://dev.iombowls.dev.cc")
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});
			
			services.AddMvc();

			services.AddApiVersioning(x =>
			{
				x.ReportApiVersions = true;
				x.UseApiBehavior = false;
			});

			services.AddVersionedApiExplorer(
				options =>
				{
					// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
					// note: the specified format code will format the version as "'v'major[.minor][-status]"
					options.GroupNameFormat = "'v'VVV";

					// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
					// can also be used to control the format of the API version in route templates
					options.SubstituteApiVersionInUrl = true;
				});
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen(
				options =>
				{
					// add a custom operation filter which sets default values
					options.OperationFilter<SwaggerDefaultValues>();

					// integrate xml comments
					//options.IncludeXmlComments(XmlCommentsFilePath);
				});


//			services.AddLogging(loggingBuilder =>
//				loggingBuilder.AddSerilog(dispose: true));


			var recaptchaSection = this.Configuration.GetSection("RecaptchaSettings");
			if (!recaptchaSection.Exists())
				throw new ArgumentException("Missing RecaptchaSettings in configuration.");
			services.Configure<RecaptchaSettings>(recaptchaSection);

			var emailSection = this.Configuration.GetSection("EmailSettings");
			if (!emailSection.Exists())
				throw new ArgumentException("Missing EmailSettings in configuration.");
			services.Configure<EmailSettings>(emailSection);

			services.AddTransient<IRecaptchaGateway, RecaptchaGateway>();
			services.AddTransient<IRecaptchaService, RecaptchaService>();
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddScoped<IUnitOfWork, TestAppUnitOfWork>();
			services.AddScoped<ISessionProvider, TestAppSessionProvider>();
			services.AddScoped<IRegistrationSessionProvider, RegistrationSessionProvider>();
			services.AddTransient<ICompetitionRepository, CompetitionRepository>();
			services.AddTransient<ICompetitionRegistrationRepository, CompetitionRegistrationRepository>();
			services.AddTransient<IClubRepository, ClubRepository>();

			services.AddTransient<CreateSinglesRegistrationCommandHandler, CreateSinglesRegistrationCommandHandler>();
			services.AddTransient<CreateDoublesRegistrationCommandHandler, CreateDoublesRegistrationCommandHandler>();
			services.AddTransient<CreateTriplesRegistrationCommandHandler, CreateTriplesRegistrationCommandHandler>();

			services.AddTransient<CreateSinglesRegistrationCommandValidator, CreateSinglesRegistrationCommandValidator>();
			services.AddTransient<CreateDoublesRegistrationCommandValidator, CreateDoublesRegistrationCommandValidator>();
			services.AddTransient<CreateTriplesRegistrationCommandValidator, CreateTriplesRegistrationCommandValidator>();

			services.AddTransient<IRegistrationEmailManager, RegistrationEmailManager>();

			TestAppSessionProvider.Initialise(this.Configuration.GetConnectionString("BowlingDatabase"));
			RegistrationSessionProvider.Initialise(this.Configuration.GetConnectionString("RegistrationDatabase"));

			services.AddSingleton<IJobFactory, SingletonJobFactory>();
			services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

			services.AddSingleton<HelloWorldJob>();
			//services.AddSingleton(new JobSchedule(
			//	jobType: typeof(HelloWorldJob),
			//	cronExpression: "0/5 * * * * ?")); // run every 5 seconds

			services.AddHostedService<QuartzHostedService>();
		}

		static string XmlCommentsFilePath
		{
			get
			{
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				return Path.Combine(basePath, fileName);
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddSerilog();
			
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// And add this, an endpoint for our swagger doc 
			app.UseSwagger();
			app.UseSwaggerUI(
				options =>
				{
					// build a swagger endpoint for each discovered API version
					foreach (var description in provider.ApiVersionDescriptions)
					{
						options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
					}
				});

			app.UseCors("BOB"); 
			app.UseMvc();

			//loggerFactory.AddSerilog();

//			
		}
	}

	/// <summary>
	/// Configures the Swagger generation options.
	/// </summary>
	/// <remarks>This allows API versioning to define a Swagger document per API version after the
	/// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		readonly IApiVersionDescriptionProvider provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
		/// </summary>
		/// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

		/// <inheritdoc />
		public void Configure(SwaggerGenOptions options)
		{
			// add a swagger document for each discovered API version
			// note: you might choose to skip or document deprecated API versions differently
			foreach (var description in this.provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
			}
		}

		static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new OpenApiInfo()
			{
				Title = "Sample API",
				Version = description.ApiVersion.ToString(),
				Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
				Contact = new OpenApiContact() {Name = "Bill Mei", Email = "bill.mei@somewhere.com"},
				License = new OpenApiLicense() {Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT")}
			};

			if (description.IsDeprecated)
			{
				info.Description += " This API version has been deprecated.";
			}

			return info;
		}
	}

	/// <summary>
	/// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
	/// </summary>
	/// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
	/// Once they are fixed and published, this class can be removed.</remarks>
	public class SwaggerDefaultValues : IOperationFilter
	{
		/// <summary>
		/// Applies the filter to the specified operation using the given context.
		/// </summary>
		/// <param name="operation">The operation to apply the filter to.</param>
		/// <param name="context">The current operation filter context.</param>
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var apiDescription = context.ApiDescription;

			operation.Deprecated |= apiDescription.IsDeprecated();

			if (operation.Parameters == null)
			{
				return;
			}

			// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
			// REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
			foreach (var parameter in operation.Parameters)
			{
				var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

				if (parameter.Description == null)
				{
					parameter.Description = description.ModelMetadata?.Description;
				}

				if (parameter.Schema.Default == null && description.DefaultValue != null)
				{
					parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
				}

				parameter.Required |= description.IsRequired;
			}
		}
	}
}