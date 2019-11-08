using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Copy Variables Command - cpv --swn=ceres-buoy-dpl-ivan --twn=utility-test --x="CONFIRM_DESTROY,admin_username,admin_password"
  ///   --i="admin_username,admin_password"
  /// </summary>
  public class CopyVariablesCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string SourceWorkspaceName { get; set; }
    public string TargetWorkspaceName { get; set; }
    public string ExcludeVariables { get; set; }
    public string IncludeVariables { get; set; }

    public CopyVariablesCommand()
    {
      IsCommand("cpv", "Copy Variables");
      HasLongDescription("Copy Variables.");
      HasRequiredOption("swn|sourceworkspacename=", "Source Workspace Name.", x => SourceWorkspaceName = x);
      HasRequiredOption("twn|targetworkspacename=", "Target Workspace Name.", x => TargetWorkspaceName = x);
      HasOption("x|excludevariables=", "Exclude Variables.", x => ExcludeVariables = x);
      HasOption("i|includevariables=", "Include Variables.", x => IncludeVariables = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var vars = service.CopyVariables(SourceWorkspaceName, TargetWorkspaceName, ExcludeVariables, IncludeVariables).Result;

        foreach (var var in vars)
        {
          Console.Out.WriteLine($"{var.Key}: {var.Value}; {var.Hcl}, {var.Sensitive}, {var.Created}");
        }
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
