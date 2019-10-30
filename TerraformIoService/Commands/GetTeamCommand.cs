using System;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// Get Team Command
  /// t
  /// t --ti=team-BEPxE9EQwHCUwnwG
  /// </summary>
  public class GetTeamCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string TeamId { get; set; }

    public GetTeamCommand()
    {
      IsCommand("t", "Get Team");
      HasLongDescription("Get Team.");
      HasOption("ti|teamid=", "Team Id.", x => TeamId = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var result = default(string);

        if (!string.IsNullOrEmpty(TeamId))
        {
          result = service.GetTeam(TeamId).Result;
        }
        else
        {
          result = service.GetTeam().Result;
        }
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
