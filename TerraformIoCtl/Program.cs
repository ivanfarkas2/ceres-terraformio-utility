using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ManyConsole;

namespace TerraformIoCtl
{
  public class Program
  {
    private const string EnvironmentVariablePrefix = "TFIO_";
    public static TerraformIoService TerraformIoService;
    public static IConfiguration Configuration;

    static int Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var serviceScope = host.Services.CreateScope())
      {
        var services = serviceScope.ServiceProvider;

        try
        {
          TerraformIoService = services.GetRequiredService<TerraformIoService>();

          var commands = GetCommands();

          return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();

          logger.LogError(ex, "An error occurred.");
          return -1;
        }
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .ConfigureHostConfiguration(hostConfig =>
          {
            hostConfig.SetBasePath(Directory.GetCurrentDirectory());
            hostConfig.AddJsonFile("hostsettings.json", true);
            hostConfig.AddEnvironmentVariables(EnvironmentVariablePrefix);
            hostConfig.AddCommandLine(args);
          })
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
            config.AddJsonFile("appsettings.json", true, true);
            config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json");
            config.AddEnvironmentVariables(EnvironmentVariablePrefix);
            config.AddCommandLine(args);
          })
          .ConfigureServices((hostContext, services) =>
          {
            services.AddHttpClient<TerraformIoService>();
          });

    public static IEnumerable<ConsoleCommand> GetCommands() => ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
  }
}
