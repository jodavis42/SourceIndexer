using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class SrcTool
  {
    static public string GetUnindexedList(string pdbPath)
    {
      return RunSrcTool(pdbPath, "-u");
    }
    static public string GetSrcSrvResults(string pdbPath)
    {
      return RunSrcTool(pdbPath, "-n");
    }
    static public List<SourceFile> GetFileListing(string pdbPath)
    {
      var output = RunSrcTool(pdbPath, "-r");

      var lines = output.Split('\n');

      // Copy all of the lines out. The last line is a newline and the one before that is a summary hence the -2.
      var results = new List<SourceFile>();
      for (var i = 0; i < lines.Length - 2; ++i)
      {
        var file = new SourceFile() { PdbFilePath = lines[i].Trim() };
        results.Add(file);
      }
      return results;
    }
    static public string RunSrcTool(string pdbPath, string arguments)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "External/srctool.exe";
      startInfo.Arguments = String.Format("\"{0}\" {1}", pdbPath, arguments);
      startInfo.RedirectStandardOutput = true;
      startInfo.UseShellExecute = false;

      var process = Process.Start(startInfo);
      var output = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return output;
    }
  }
}
