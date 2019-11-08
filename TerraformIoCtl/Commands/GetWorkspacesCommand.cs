using System;

using ManyConsole;

namespace TerraformIoCtl.Commands
{
  /// <summary>
  /// Get Workspaces Command - ws
  /// </summary>
  public class GetWorkspacesCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public GetWorkspacesCommand()
    {
      IsCommand("ws", "Get Workspaces");
      HasLongDescription("Get Workspaces.");
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;

        Console.WriteLine("Get Workspaces");
        var result = service.GetWorkspaces().Result;
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
