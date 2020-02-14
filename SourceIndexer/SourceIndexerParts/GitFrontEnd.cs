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
    public override void EvaluateFiles(List<SourceFile> pdbFiles, RepositoryList repositories)
    {
      foreach (var repo in repositories.Repositories)
      {
        List<SourceFile> remainingPdbFiles = new List<SourceFile>();
        Utilities.FindSourceFileIntersection(pdbFiles, repo.RepositoryPath, repo.SourceFiles, ref repo.SourceFiles, ref remainingPdbFiles, Config.Logger);
        pdbFiles = remainingPdbFiles;


        Config.Logger.Log(VerbosityLevel.Basic, string.Format("Repository {0} found with {1} matching files:", repo.Location, repo.SourceFiles.Count));
        if (Config.Logger.IsVerboseEnough(VerbosityLevel.Detailed))
        {
          foreach (var file in repo.SourceFiles)
          {
            Config.Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", file.FullPath));
          }
        }
      }
    }
    public override RepositoryList GetRepositoryList(bool recursive)
    {
      return FindRepositories(Config.SourceRoot);
    }
    protected RepositoryList FindRepositories(string sourceRoot)
    {
      var repositories = new RepositoryList();
      repositories.SourceRoot = sourceRoot;

      var repos = Git.FindSubModules(sourceRoot);

      var tasks = new List<Task>();
      foreach (var repo in repos)
      {
        var task = new Task(() =>
        {
          RepositoryInfo repoInfo = new RepositoryInfo();
          repoInfo.RepositoryName = Path.GetFileName(repo);
          repoInfo.RepositoryType = "git";
          repoInfo.RepositoryPath = repo;
          repoInfo.CurrentId = Git.GetRevisionSha(repo);
          repoInfo.Location = Git.FindRemoteUrl(repo);
          var gitFiles = Git.GetFileList(repoInfo.RepositoryPath);

          Config.Logger.Log(VerbosityLevel.Detailed, string.Format("Git repo {0} at '{1}' has file list:", repoInfo.Location, repoInfo.RepositoryPath));
          foreach (var relativePath in gitFiles)
          {
            var sourceFile = new SourceFile();
            try
            {
              var fullPath = Path.Combine(sourceRoot, relativePath);
              sourceFile.FullPath = Path.GetFullPath(fullPath);
              sourceFile.RelativePath = relativePath;
              sourceFile.PdbFilePath = sourceFile.FullPath;
              repoInfo.SourceFiles.Add(sourceFile);

              
              Config.Logger.Log(VerbosityLevel.Detailed, string.Format("  {0}", fullPath));
            }
            catch(Exception)
            {

            }
            
          }

          repositories.Repositories.Add(repoInfo);
          Config.Logger.Log(VerbosityLevel.Basic, string.Format("Found Git module {0} at sha {1} with remote {2}", repoInfo.RepositoryPath, repoInfo.CurrentId, repoInfo.Location));
        });
        task.Start();
        tasks.Add(task);
      }
      Task.WaitAll(tasks.ToArray());
      return repositories;
    }

  }
}
