using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public abstract class IBackEnd
  {
    public Logger Logger = new Logger();
    public abstract string BuildSrcSrvStream(List<RepositoryInfo> repositories);
  }
}
