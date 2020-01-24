using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class PdbStr
  {
    static public string ReadStream(string pdbPath, string streamName)
    {
      ProcessStartInfo info = new ProcessStartInfo();
      info.FileName = "External/pdbstr.exe";
      info.Arguments = String.Format("-r -p:\"{0}\" -s:{1}", pdbPath, streamName);
      info.UseShellExecute = false;
      info.RedirectStandardOutput = true;
      Process process = Process.Start(info);
      string output = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return output;
    }

    static public string ReadStream(string pdbPath)
    {
      return ReadStream(pdbPath, "srcsrv");
    }

    static public void WriteStream(string pdbPath, string streamContents)
    {
      WriteStream(pdbPath, "srcsrv", streamContents);
    }

    static public void WriteStream(string pdbPath, string streamName, string streamContents)
    {
      var outFilePath = "SrcSrvOut.txt";
      File.WriteAllText(outFilePath, streamContents);

      ProcessStartInfo info = new ProcessStartInfo();
      info.FileName = "External/pdbstr.exe";
      info.Arguments = String.Format("-w -p:\"{0}\" -s:{1} -i:{2}", pdbPath, streamName, outFilePath);
      info.UseShellExecute = false;
      Process process = Process.Start(info);
      process.WaitForExit();
    }
  }
}
