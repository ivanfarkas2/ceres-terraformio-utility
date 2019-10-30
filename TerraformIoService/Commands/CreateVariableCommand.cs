using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Create Variable Command - crv --twn=utility-test --vf=C:\Projects\Attribute.json
  /// </summary>
  public class CreateVariableCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string TargetWorkspaceName { get; set; }
    public string VariableFile { get; set; }

    public CreateVariableCommand()
    {
      IsCommand("crv", "Create Variable");
      HasLongDescription("Create Variable.");
      HasRequiredOption("twn|targetworkspacename=", "Target Workspace Name.", x => TargetWorkspaceName = x);
      HasRequiredOption("vf|variablefile=", "Variable File.", x => VariableFile = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = service.CreateVariable(TargetWorkspaceName, VariableFile).Result;
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
