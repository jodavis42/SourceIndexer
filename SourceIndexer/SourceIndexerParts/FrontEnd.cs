using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public abstract class IFrontEnd
  {
    string SourceRoot;
    public Logger Logger = new Logger();

    public virtual void SetSourceRoot(string sourceRoot)
    {
      SourceRoot = sourceRoot;
    }

    public abstract void EvaluateFiles(List<SourceFile> pdbFiles);
    public abstract List<RepositoryInfo> GetRepositoryInfo();
  }
}
