using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  class Program
  {
    static void Main(string[] args)
    {
      string pdbPath = Path.GetFullPath(args[0]);
      string sourceRoot = Path.GetFullPath(args[1]);
      var indexer = new SourceIndexer();
      indexer.Logger = new ConsoleLogger();
      indexer.FrontEnd = new GitFrontEnd();
      indexer.BackEnd = new CmdBackEnd();
      indexer.SetSourceRoot(sourceRoot);
      indexer.FullPdbPath = pdbPath;
      indexer.RunSourceIndexing();
    }
  }
}
