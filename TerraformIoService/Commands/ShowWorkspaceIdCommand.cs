using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Show Workspace Command - wi --wn=titan-prod-green
  /// </summary>
  public class ShowWorkspaceIdCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string WorkspaceName { get; set; }

    public ShowWorkspaceIdCommand()
    {
      IsCommand("wi", "Show Workspace Id");
      HasLongDescription("Show Workspace Id.");
      HasRequiredOption("wn|workspacename=", "Workspace Name.", x => WorkspaceName = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = default(string);

        result = service.ShowWorkspaceId(WorkspaceName).Result;
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
