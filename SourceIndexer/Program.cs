using CommandLine;
using Microsoft.Extensions;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class Options
  {
    [Option("workingDir", Required = false, HelpText = "Working directory to search pdbs for.")]
    public string WorkingDir { get; set; } = null;
    [Option("pdb", Required = true, HelpText = "Search expression for pdb files. This is relative the the working directory.")]
    public string PdbSearchExpression { get; set; }

    [Option("sourceRoot", Required = true, HelpText = "Root directory of the repositories to use for indexing.")]
    public string SourceRoot { get; set; }

    [Option("submodules", Required = false, HelpText = "Should submodules be indexed as well.")]
    public bool IndexSubmodules { get; set; } = true;
  }

  class Program
  {
    static void ParseSucceeded(Options options)
    {
      var indexer = new SourceIndexer();
      indexer.Logger = new ConsoleLogger();
      indexer.FrontEnd = new GitFrontEnd();
      indexer.BackEnd = new CmdBackEnd();
      indexer.SetSourceRoot(options.SourceRoot);

      string workingDir = options.WorkingDir;
      if (string.IsNullOrEmpty(workingDir))
        workingDir = Directory.GetCurrentDirectory();

      Matcher matcher = new Matcher();
      matcher.AddInclude(options.PdbSearchExpression);

      PatternMatchingResult matches = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(workingDir)));
      foreach(var match in matches.Files)
      {
        var path = Path.Combine(workingDir, match.Path);
        var pdbPath = Path.GetFullPath(path);
        indexer.FullPdbPath = pdbPath;
        indexer.RunSourceIndexing();
      }
    }
    static void ParseFailed(IEnumerable<Error> errors)
    {
      Console.WriteLine("Command Line Error:");
      foreach(var error in errors)
      {
        Console.WriteLine(error.ToString());
      }
    }

    static void Main(string[] args)
    {
      Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o => { ParseSucceeded(o); })
        .WithNotParsed<Options>(o => { ParseFailed(o); });
    }
  }
}
