using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Clone Workspace Command - wcl --wns=titan-prod-green --wnd=titan-utility-test
  /// </summary>
  public class CloneWorkspaceCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string SourceWorkspaceName { get; set; }
    public string DestinationWorkspaceName { get; set; }

    public CloneWorkspaceCommand()
    {
      IsCommand("wcl", "Clone Workspace");
      HasLongDescription("Clone Workspace.");
      HasRequiredOption("swn|sourceworkspacename=", "Source Workspace Name.", x => SourceWorkspaceName = x);
      HasRequiredOption("dwn|destinationworkspacename=", "Destination Workspace Name.", x => DestinationWorkspaceName = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = default(string);

        // result = service.ShowWorkspace(WorkspaceName).Result;
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
