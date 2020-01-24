using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public string RepositoryPath = "";
    public string CurrentId = "";
    public string Location = "";
  }
  public class SourceIndexerConfig
  {
    public bool Verbose = true;
  }

  public abstract class ISourceIndexer
  {
    public SourceIndexerConfig Config = new SourceIndexerConfig();
    public Logger Logger = new Logger();
    public string FullPdbPath = "";
    protected string SourceRoot = "";
    public virtual void SetSourceRoot(string sourceRoot)
    {
      SourceRoot = sourceRoot;
    }
    public abstract string BuildSrcSrvStream(List<SourceFile> files);
    public virtual void RunSourceIndexing()
    {
      try
      {
        // Get the list of files from the source tool
        var files = this.GetFileListing();
        if (Config.Verbose)
        {
          Logger.Log(VerbosityLevel.Basic, string.Format("Pdb contains {0} files:", files.Count));
          foreach(var file in files)
          {
            Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.PdbFilePath));
          }
        }
        // Process any files that aren't need to look up their source control location somewhere special
        //this.ProcessFiles(files);
        // Build the text data for the srcsrv stream
        var srcSrvStream = this.BuildSrcSrvStream(files);

        if (Config.Verbose)
        {
          Logger.Log(VerbosityLevel.Detailed, string.Format("Writing stream\n{0}", srcSrvStream));
        }

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
    public string RunSrcTool(string arguments)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "Tools/srctool.exe";
      startInfo.Arguments = String.Format("\"{0}\" {1}", this.FullPdbPath, arguments);
      startInfo.RedirectStandardOutput = true;
      startInfo.UseShellExecute = false;

      var process = Process.Start(startInfo);
      var output = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return output;
    }
  }
}
