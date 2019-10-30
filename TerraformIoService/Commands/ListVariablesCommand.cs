﻿using System;
using System.Collections.Generic;

using ManyConsole;

namespace TerraformIoUtility.Commands
{
  /// <summary>
  /// List Variables Command - l --wn=titan-prod-green
  /// </summary>
  public class ListVariablesCommand : ConsoleCommand
  {
    private const int Success = 0;
    private const int Failure = 2;

    public string WorkspaceName { get; set; }

    public ListVariablesCommand()
    {
      IsCommand("l", "List Variables");
      HasLongDescription("List Variables.");
      HasRequiredOption("wn|workspacename=", "Workspace Name.", x => WorkspaceName = x);
    }

    public override int Run(string[] remainingArguments)
    {
      try
      {
        var service = Program.TerraformIoService;
        var vars = default(List<Variable>);

        vars = service.ListVariables(WorkspaceName).Result;

        foreach (var var in vars)
        {
          Console.Out.WriteLine($"{var.Name}: {var.Value}; {var.IsHcl}, {var.IsSensitive}, {var.Created}");
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
