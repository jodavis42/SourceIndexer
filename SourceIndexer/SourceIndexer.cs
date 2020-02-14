using System;
using System.Collections.Generic;

namespace SourceIndexer
{
  public class SourceIndexerConfig
  {
    public Logger Logger = new Logger();
    public bool FastMode = true;
    public bool ExtractDebugStreams = false;
    public string SourceRoot = "";
  }

  // A collection of debug info from a pdb after indexing
  public class SourceIndexerDebugStreams
  {
    public string UnindexedResults = "";
    public string RawResults = "";
    public string EvaluatedResults = "";
  }


  public class SourceIndexer
  {
    public SourceIndexerConfig Config = new SourceIndexerConfig();
    public List<string> PdbPaths = new List<string>();
    public IFrontEnd FrontEnd = null;
    public IBackEnd BackEnd = null;
    public RepositoryList Repositories = null;
    public Dictionary<string, SourceIndexerDebugStreams> DebugStreams = new Dictionary<string, SourceIndexerDebugStreams>();

    public void CompileSourceRepositories()
    {
      if (FrontEnd.Config == null)
        FrontEnd.Config = Config;
      Repositories = FrontEnd.GetRepositoryList(true);
    }
    
    public virtual void RunSourceIndexing()
    {
      try
      {
        // Pull out the repositories with all of their files (if this wasn't done for us already)
        if (Repositories == null)
          CompileSourceRepositories();

        RunIndexingInternal();
      }
      catch (Exception ex)
      {
        Config.Logger.Log(VerbosityLevel.Error, string.Format("Exception: {0}", ex.ToString()));
      }
    }

    public string GetSourceIndexingResults(string pdbPath)
    {
      return PdbStr.ReadStream(pdbPath);
    }

    public string EvaluateSourceIndexing(string pdbPath)
    {
      return SrcTool.GetSrcSrvResults(pdbPath);
    }

    public string GetUnindexedList(string pdbPath)
    {
      return SrcTool.GetUnindexedList(pdbPath);
    }

    public SourceIndexerDebugStreams GetDebugStreams(string pdbPath)
    {
      if(DebugStreams.ContainsKey(pdbPath))
        return DebugStreams[pdbPath];
      return null;
    }

    // Returns the list of files in that need source indexing
    private List<SourceFile> GetFileListing(string pdbPath)
    {
      return SrcTool.GetFileListing(pdbPath);
    }

    private void RunIndexingInternal()
    {
      // Setup the Configs
      FrontEnd.Config = Config;
      BackEnd.Config = Config;

      foreach(var pdb in PdbPaths)
        IndexPdb(pdb);
    }

    private void IndexPdb(string pdbPath)
    {
      var repositories = Repositories;
      // If we're not in fast mode, we need to prune the list of repo files based upon the pdb file listing
      if (!Config.FastMode)
      {
        // Get the list of files from the source tool
        var files = this.GetFileListing(pdbPath);
        Config.Logger.Log(VerbosityLevel.Basic, string.Format("Pdb contains {0} files:", files.Count));
        if (Config.Logger.IsVerboseEnough(VerbosityLevel.Detailed))
        {
          foreach (var file in files)
          {
            Config.Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.PdbFilePath));
          }
        }

        // Evaluate the pdb source files given our front end
        repositories = Repositories.Clone();
        FrontEnd.EvaluateFiles(files, Repositories);
      }

      // Build the text data for the srcsrv stream from the backend
      var srcSrvStream = BackEnd.BuildSrcSrvStream(Repositories);

      Config.Logger.Log(VerbosityLevel.Detailed, string.Format("Writing stream\n{0}", srcSrvStream));

      // Write the stream out to the pdb file
      PdbStr.WriteStream(pdbPath, srcSrvStream);

      if (Config.ExtractDebugStreams)
      {
        var debugStream = new SourceIndexerDebugStreams();
        ExtractDebugStreams(pdbPath, debugStream);
        DebugStreams.Add(pdbPath, debugStream);
      }
    }

    private void ExtractDebugStreams(string pdbPath, SourceIndexerDebugStreams debugStreams)
    {
      debugStreams.UnindexedResults = GetUnindexedList(pdbPath);
      debugStreams.EvaluatedResults = EvaluateSourceIndexing(pdbPath);
      debugStreams.RawResults = GetSourceIndexingResults(pdbPath);
    }
  }
}
