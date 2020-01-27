using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class GitFrontEnd : IFrontEnd
  {
    protected List<RepositoryInfo> Repositories = new List<RepositoryInfo>();
    public override void SetSourceRoot(string sourceRoot)
    {
      base.SetSourceRoot(sourceRoot);
      FindRepositories(sourceRoot);
    }

    public override void EvaluateFiles(List<SourceFile> pdbFiles)
    {
      foreach (var repo in Repositories)
      {
        var gitFiles = Git.GetFileList(repo.RepositoryPath);

        if (Logger.IsVerboseEnough(VerbosityLevel.Detailed))
        {
          Logger.Log(VerbosityLevel.Detailed, string.Format("Git repo {0} at '{1}' has file list:", repo.Location, repo.RepositoryPath));
          foreach (var file in gitFiles)
          {
            Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file));
          }
        }

        List<SourceFile> remainingPdbFiles = new List<SourceFile>();
        Utilities.FindSourceFileIntersection(pdbFiles, repo.RepositoryPath, gitFiles, ref repo.SourceFiles, ref remainingPdbFiles, Logger);
        pdbFiles = remainingPdbFiles;


        Logger.Log(VerbosityLevel.Basic, string.Format("Repository {0} found with {1} matching files:", repo.Location, repo.SourceFiles.Count));
        if (Logger.IsVerboseEnough(VerbosityLevel.Detailed))
        {
          foreach (var file in repo.SourceFiles)
          {
            Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.FullPath));
          }
        }
      }
    }
    public override List<RepositoryInfo> GetRepositoryInfo()
    {
      return Repositories;
    }
    protected void FindRepositories(string sourceRoot)
    {
      var repos = Git.FindSubModules(sourceRoot);
      foreach (var repo in repos)
      {
        RepositoryInfo repoInfo = new RepositoryInfo();
        repoInfo.RepositoryName = Path.GetFileName(repo);
        repoInfo.RepositoryType = "git";
        repoInfo.RepositoryPath = repo;
        repoInfo.CurrentId = Git.GetRevisionSha(repo);
        repoInfo.Location = Git.FindRemoteUrl(repo);
        Repositories.Add(repoInfo);

        Logger.Log(VerbosityLevel.Basic, string.Format("Found Git module {0} at sha {1} with remote {2}", repoInfo.RepositoryPath, repoInfo.CurrentId, repoInfo.Location));
      }
    }

  }
}
