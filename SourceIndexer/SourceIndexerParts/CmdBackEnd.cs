using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class CmdBackEnd : IBackEnd
  {
    public override string ToString()
    {
      return "Cmd";
    }
    public override string BuildSrcSrvStream(RepositoryList repositories)
    {
      var builder = new StringBuilder();
      builder.AppendLine("SRCSRV: ini ------------------------------------------------");
      builder.AppendLine("VERSION=1");
      builder.AppendLine("VERCTRL=GIT");
      builder.AppendLine("SRCSRV: variables------------------------------------------");
      builder.AppendLine(@"SRCLOCATION=%CMD_SOURCE_INDEXER_PATH%");
      builder.AppendLine(@"SRCSRVTRG=%targ%\%var4%\%var5%");
      builder.AppendLine("SRCSRVCMD=%fnvar%(SRCLOCATION) \"%var2%\" \"%var3%\" \"%var4%\" \"%var5%\" %SRCSRVTRG%");
      builder.AppendLine("SRCSRV: source files ---------------------------------------");
      foreach (var repo in repositories.Repositories)
      {
        foreach (var file in repo.SourceFiles)
        {
          builder.AppendLine(string.Format("{0}*{1}*{2}*{3}*{4}", file.PdbFilePath, repo.RepositoryType, repo.RepositoryName, repo.CurrentId, file.RelativePath));
        }
      }
      builder.AppendLine("SRCSRV: end------------------------------------------------");
      return builder.ToString();
    }
  }
}
