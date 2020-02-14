using System.Collections.Generic;
using System.Text;

namespace SourceIndexer
{
  public class GitBackEnd : IBackEnd
  {
    
    public override string ToString()
    {
      return "Git";
    }
    public override string BuildSrcSrvStream(RepositoryList repositories)
    {
      var builder = new StringBuilder();
      builder.AppendLine("SRCSRV: ini ------------------------------------------------");
      builder.AppendLine("VERSION=1");
      builder.AppendLine("VERCTRL=GIT");
      builder.AppendLine("SRCSRV: variables------------------------------------------");
      builder.AppendLine(@"SRCSRVTRG=%targ%\%var2%\%var3%\%var4%");
      builder.AppendLine("SRCSRVCMD=git.exe -C \"%fnvar%(%var2%)\" show %var3%:%var4% > %SRCSRVTRG%");
      builder.AppendLine("SRCSRV: source files ---------------------------------------");
      foreach (var repo in repositories.Repositories)
      {
        foreach (var file in repo.SourceFiles)
        {
          builder.AppendLine(string.Format("{0}*{1}*{2}*{3}", file.PdbFilePath, repo.RepositoryName, repo.CurrentId, file.RelativePath));
        }
      }
      builder.AppendLine("SRCSRV: end------------------------------------------------");
      return builder.ToString();
    }
  }
}
