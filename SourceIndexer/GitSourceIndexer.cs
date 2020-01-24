using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class GitSourceIndexer : ISourceIndexer
  {
    protected List<RepositoryInfo> Respositories = new List<RepositoryInfo>();

    public override void SetSourceRoot(string sourceRoot)
    {
      base.SetSourceRoot(sourceRoot);
      FindRepositories();
    }

    public override string BuildSrcSrvStream(List<SourceFile> files)
    {
      return "";
    }

    protected void PopulateFiles(List<SourceFile> pdbFiles)
    {
      foreach(var repo in Respositories)
      {
        var gitFiles = Git.GetFileList(repo.RepositoryPath);
        if (Config.Verbose)
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

        if (Config.Verbose)
        {
          Logger.Log(VerbosityLevel.Basic, string.Format("Repository {0} found with {1} matching files:", repo.Location, repo.SourceFiles.Count));
          foreach(var file in repo.SourceFiles)
          {
            Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.FullPath));
          }
        }
      }
    }

    protected void FindRepositories()
    {
      var repos = Git.FindSubModules(SourceRoot);
      foreach(var repo in repos)
      {
        RepositoryInfo repoInfo = new RepositoryInfo();
        repoInfo.RepositoryPath = repo;
        repoInfo.CurrentId = Git.GetRevisionSha(repo);
        repoInfo.Location = Git.FindRemoteUrl(repo);
        Respositories.Add(repoInfo);

        if(Config.Verbose)
        {
          Logger.Log(VerbosityLevel.Basic, string.Format("Found Git module {0} at sha {1} with remote {2}", repoInfo.RepositoryPath, repoInfo.CurrentId, repoInfo.Location));
        }
      }
    }
  }
}
