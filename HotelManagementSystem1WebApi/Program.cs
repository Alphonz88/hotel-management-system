using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
namespace HotelManagementSystem1WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.UseNLog()
			.UseUrls("http://*:30106")
			.UseKestrel(options =>
			{
				options.Limits.MaxRequestBodySize = 30 * 1024;
			})
			.Build();
	}
}

