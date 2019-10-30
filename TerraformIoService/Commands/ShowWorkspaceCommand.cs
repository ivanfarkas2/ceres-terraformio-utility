using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Show Workspace Command - w --wn=titan-prod-green
  /// </summary>
  public class ShowWorkspaceCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string WorkspaceName { get; set; }

    public ShowWorkspaceCommand()
    {
      IsCommand("w", "Show Workspace");
      HasLongDescription("Show Workspace.");
      HasRequiredOption("wn|workspacename=", "Workspace Name.", x => WorkspaceName = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = default(string);

        result = service.ShowWorkspace(WorkspaceName).Result;
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
