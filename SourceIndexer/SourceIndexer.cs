using System;
using System.Collections.Generic;

namespace SourceIndexer
{
  public class SourceFile
  {
    public string PdbFilePath = "";
    public string FullPath = "";
    public string RelativePath = "";
  }
  public class RepositoryInfo
  {
    public List<SourceFile> SourceFiles = new List<SourceFile>();
    public string RepositoryName = "";
    public string RepositoryPath = "";
    public string RepositoryType = "";
    public string CurrentId = "";
    public string Location = "";
  }

  public class SourceIndexer
  {
    public Logger Logger = new Logger();
    public string FullPdbPath = "";
    protected string SourceRoot = "";
    public IFrontEnd FrontEnd = null;
    public IBackEnd BackEnd = null;
    public virtual void SetSourceRoot(string sourceRoot)
    {
      SourceRoot = sourceRoot;
      
    }
    public virtual string BuildSrcSrvStream(List<SourceFile> files) { return ""; }
    public virtual void RunSourceIndexing()
    {
      try
      {
        // Get the list of files from the source tool
        var files = this.GetFileListing();
        Logger.Log(VerbosityLevel.Basic, string.Format("Pdb contains {0} files:", files.Count));
        if(Logger.IsVerboseEnough(VerbosityLevel.Detailed))
        {
          foreach (var file in files)
          {
            Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.PdbFilePath));
          }
        }

        // Setup the front end
        FrontEnd.SetSourceRoot(SourceRoot);
        // Evaluate the pdb source files given our front end
        FrontEnd.EvaluateFiles(files);
        // Pull out the repositories with all of their files
        var repositories = FrontEnd.GetRepositoryInfo();
        // Build the text data for the srcsrv stream from the backend
        var srcSrvStream = BackEnd.BuildSrcSrvStream(repositories);

        Logger.Log(VerbosityLevel.Detailed, string.Format("Writing stream\n{0}", srcSrvStream));

        // Write the stream out to the pdb file
        PdbStr.WriteStream(this.FullPdbPath, srcSrvStream);
      }
      catch (Exception ex)
      {
        Logger.Log(VerbosityLevel.Error, string.Format("Exception: {0}", ex.ToString()));
      }
    }
    public string GetSourceIndexingResults()
    {
      return PdbStr.ReadStream(this.FullPdbPath);
    }

    public string EvaluateSourceIndexing()
    {
      return SrcTool.GetSrcSrvResults(this.FullPdbPath);
    }

    public string GetUnindexedList()
    {
      return SrcTool.GetUnindexedList(this.FullPdbPath);
    }

    // Returns the list of files in that need source indexing
    private List<SourceFile> GetFileListing()
    {
      return SrcTool.GetFileListing(this.FullPdbPath);
    }
  }
}
