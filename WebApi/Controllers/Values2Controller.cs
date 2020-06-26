using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using reCAPTCHA.AspNetCore;

namespace BowlsResults.WebApi.Controllers
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/{v:apiVersion}/values")]
	public class Values2Controller : Controller
	{
		public Values2Controller(IRecaptchaService recaptcha, ICompetitionRepository repo)
		{
			this.Recaptcha = recaptcha;
			this.Repo = repo;
		}

		public ICompetitionRepository Repo { get; set; }

		public IRecaptchaService Recaptcha { get; set; }

		// GET api/values
		[HttpGet]
		public async Task<string[]> Get()
		{
			return new string[] {"2.0value1", "value2"};
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Competition Secretary", "compsec@iombowls.com"));
			message.To.Add(new MailboxAddress("Matthew Keggen", "matthew.keggen@googlemail.com"));
			//message.Subject = "Competition Entry for " + competition.Name;

			Console.WriteLine("*******************************************************");
			Console.WriteLine("MAIL - NONE");
			Console.WriteLine("*******************************************************");

			try
			{
				using (var client = new SmtpClient())
				{
					client.Connect("smtp1r.cp.blacknight.com", 25, SecureSocketOptions.None);
					//client.Connect("127.0.0.1", 587, false);

					// Note: only needed if the SMTP server requires authentication
					client.Authenticate("compsec@iombowls.com", "M4nun1t3d!");

					message.Body = new TextPart("plain")
					{
						Text = @"Dear Matthew

Thank you for your entry for the mens competition which has been confirmed.

regards
Competition Secretary - NONE
"
					};

					client.Send(message);
					client.Disconnect(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("*******************************************************");
			Console.WriteLine("MAIL - StartTls");
			Console.WriteLine("*******************************************************");
			try
			{
				using (var client = new SmtpClient())
				{
					client.Connect("smtp1r.cp.blacknight.com", 25, SecureSocketOptions.StartTls);
					//client.Connect("127.0.0.1", 587, false);

					// Note: only needed if the SMTP server requires authentication
					client.Authenticate("compsec@iombowls.com", "M4nun1t3d!");

					message.Body = new TextPart("plain")
					{
						Text = @"Dear Matthew

Thank you for your entry for the mens competition which has been confirmed.

regards
Competition Secretary - StartTls
"
					};

					client.Send(message);
					client.Disconnect(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("*******************************************************");
			Console.WriteLine("MAIL - StartTlsWhenAvailable");
			Console.WriteLine("*******************************************************");
			try
			{
				using (var client = new SmtpClient())
				{
					client.Connect("smtp1r.cp.blacknight.com", 25, SecureSocketOptions.StartTlsWhenAvailable);
					//client.Connect("127.0.0.1", 587, false);

					// Note: only needed if the SMTP server requires authentication
					client.Authenticate("compsec@iombowls.com", "M4nun1t3d!");

					message.Body = new TextPart("plain")
					{
						Text = @"Dear Matthew

Thank you for your entry for the mens competition which has been confirmed.

regards
Competition Secretary - StartTlsWhenAvailable
"
					};

					client.Send(message);
					client.Disconnect(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}


		}

		// GET api/values/5		
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value" + id;
		}
	}
}