using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class GitHubResolverBackEnd : IBackEnd
  {
    public override string ToString()
    {
      return "GitHubResolver";
    }
    public override string BuildSrcSrvStream(RepositoryList repositories)
    {
      var builder = new StringBuilder();
      builder.AppendLine("SRCSRV: ini ------------------------------------------------");
      builder.AppendLine("VERSION=1");
      builder.AppendLine("VERCTRL=GIT");
      builder.AppendLine("SRCSRV: variables------------------------------------------");
      builder.AppendLine(@"SRCLOCATION=%GIT_HUB_SOURCEINDEXER_RESOLVER%");
      builder.AppendLine(@"SRCSRVTRG=%fnbksl%(%targ%\%var4%\%var5%)");
      builder.AppendLine("SRCSRVCMD=%fnvar%(SRCLOCATION) \"%var2%\" \"%var3%\" \"%var4%\" \"%var5%\" %SRCSRVTRG%");
      builder.AppendLine("SRCSRVTRG=%SRCSRVCMD%");
      builder.AppendLine("SRCSRV: source files ---------------------------------------");
      foreach (var repo in repositories.Repositories)
      {
        string organization = "";
        string project = "";
        ExtractRepoInfo(repo.RepositoryUrl, ref organization, ref project);
        foreach (var file in repo.SourceFiles)
        {
          builder.AppendLine(string.Format("{0}*{1}*{2}*{3}*{4}", file.PdbFilePath, organization, project, repo.CurrentId, file.RelativePath));
        }
      }
      builder.AppendLine("SRCSRV: end------------------------------------------------");
      return builder.ToString();
    }

    bool ExtractRepoInfo(string remoteUrl, ref string organization, ref string project)
    {
      var regex = new Regex(@"github\.com\/([\d\w-_]+)\/([\d\w-_]+)");
      var match = regex.Match(remoteUrl);
      if (match.Success)
      {
        organization = match.Groups[1].Value.ToString();
        project = match.Groups[2].Value.ToString();
        Config.Logger.Log(VerbosityLevel.Detailed, string.Format("Repo '{0}' found with organization '{1}' and project '{2}'", remoteUrl, organization, project));
        return true;
      }
      return false;
    }
  }
}
