﻿using CommandLine;
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
    [Option('v', "verbose", Required = false, HelpText = "Verbose logging.")]
    public bool Verbose { get; set; } = false;
    [Option("backend", Required = false, HelpText = "What backend to use for indexing? Options are Git, GitHub, and CMD.")]
    public string BackendType{ get; set; } = "GitHub";
    [Option("fastMode", Required = false, HelpText = "Should all of the files in the source root be used or should they be matched against the pdb (much slower). This seems to work but pdbs have some issues with case sensitivity.")]
    public bool FastMode { get; set; } = true;
  }

  class Program
  {
    static List<string> FindPdbPaths(Options options, Logger logger)
    {
      var results = new List<string>();

      // If no working directory was specified, use the current
      string workingDir = options.WorkingDir;
      if (string.IsNullOrEmpty(workingDir))
        workingDir = Directory.GetCurrentDirectory();

      logger.Log(VerbosityLevel.Detailed, string.Format("Searching for pdbs with working directory '{0}' and search pattern '{1}'", workingDir, options.PdbSearchExpression));

      Matcher matcher = new Matcher();
      matcher.AddInclude(options.PdbSearchExpression);

      PatternMatchingResult matches = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(workingDir)));
      foreach (var match in matches.Files)
      {
        var path = Path.Combine(workingDir, match.Path);
        var pdbPath = Path.GetFullPath(path);
        logger.Log(VerbosityLevel.Detailed, string.Format("Found pdb '{0}'", pdbPath));
        results.Add(pdbPath);
      }
      return results;
    }

    static IBackEnd GetBackend(Options options, Logger logger)
    {
      var backendType = options.BackendType.ToLower();
      if (backendType == "git")
      {
        logger.Log(VerbosityLevel.Detailed, "Using git backend");
        return new GitBackEnd();
      }
      else if (backendType == "github")
      {
        logger.Log(VerbosityLevel.Detailed, "Using github backend");
        return new GitHubBackEnd();
      }
      else if (backendType == "githubresolver")
      {
        logger.Log(VerbosityLevel.Detailed, "Using github resolver backend");
        return new GitHubResolverBackEnd();
      }
      else if (backendType == "cmd")
      {
        logger.Log(VerbosityLevel.Detailed, "Using cmd backend");
        return new CmdBackEnd();
      }
      logger.Log(VerbosityLevel.Detailed, "Falling back to github resolver backend");
      return new GitHubResolverBackEnd();
    }

    static void ParseSucceeded(Options options)
    {
      var config = new SourceIndexerConfig();
      config.SourceRoot = options.SourceRoot;
      config.FastMode = options.FastMode;
      config.Logger = new ConsoleLogger();
      if (options.Verbose)
        config.Logger.Level = VerbosityLevel.Detailed;

      var indexer = new SourceIndexer();
      indexer.Config = config;
      indexer.FrontEnd = new GitFrontEnd();
      indexer.BackEnd = GetBackend(options, config.Logger);
      // Set the source root and fetch the repos as soon as possible (threaded)
      indexer.CompileSourceRepositories();
      // Find all pdbs the user requested
      indexer.PdbPaths = FindPdbPaths(options, config.Logger);
      // Actually run the source indexing
      indexer.RunSourceIndexing();
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
