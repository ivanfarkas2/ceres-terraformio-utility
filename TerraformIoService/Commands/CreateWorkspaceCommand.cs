using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Create Workspace Command - wcr --wf=C:\Projects\utility-test.json
  /// </summary>
  public class CreateWorkspaceCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string WorkspaceFile { get; set; }

    public CreateWorkspaceCommand()
    {
      IsCommand("wcr", "Create Workspace");
      HasLongDescription("Create Workspace.");
      HasRequiredOption("wf|workspacefile=", "Workspace File.", x => WorkspaceFile = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = default(string);

        result = service.CreateWorkspace(WorkspaceFile).Result;
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
