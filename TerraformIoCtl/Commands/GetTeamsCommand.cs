using System;

using ManyConsole;

namespace TerraformIoCtl.Commands
{
  /// <summary>
  /// Get Teams Command - ts
  /// </summary>
  public class GetTeamsCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public GetTeamsCommand()
    {
      IsCommand("ts", "Get Teams");
      HasLongDescription("Get Teams.");
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;

        Console.WriteLine("Get Teams");
        var result = service.GetTeams().Result;
        Console.Out.WriteLine(result);
        return Success;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine(ex.Message);
        Console.Error.WriteLine(ex.StackTrace);
        return Failure;
      }
    }
  }
}
