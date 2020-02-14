using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public abstract class IBackEnd
  {
    public SourceIndexerConfig Config = null;
    public abstract string BuildSrcSrvStream(RepositoryList repositories);
  }
}
