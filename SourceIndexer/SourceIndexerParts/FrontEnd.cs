using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public abstract class IFrontEnd
  {
    public SourceIndexerConfig Config = null;

    public abstract void EvaluateFiles(List<SourceFile> pdbFiles, RepositoryList repositories);
    public abstract RepositoryList GetRepositoryList(bool recursive);
  }
}
