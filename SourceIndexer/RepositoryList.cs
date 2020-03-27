using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceIndexer
{
  public class SourceFile
  {
    public string PdbFilePath = "";
    public string FullPath = "";
    public string RelativePath = "";

    public SourceFile Clone()
    {
      var clone = new SourceFile();
      clone.PdbFilePath = PdbFilePath;
      clone.FullPath = FullPath;
      clone.RelativePath = RelativePath;
      return clone;
    }
  }

  public class RepositoryInfo
  {
    public List<SourceFile> SourceFiles = new List<SourceFile>();
    public string RepositoryName = "";
    public string RepositoryPath = "";
    public string RepositoryType = "";
    public string RepositoryUrl = "";
    public string CurrentId = "";

    public RepositoryInfo Clone()
    {
      var clone = new RepositoryInfo();
      clone.RepositoryName = RepositoryName;
      clone.RepositoryPath = RepositoryPath;
      clone.RepositoryType = RepositoryType;
      clone.CurrentId = CurrentId;
      clone.RepositoryUrl = RepositoryUrl;
      foreach(var file in SourceFiles)
      {
        clone.SourceFiles.Add(file.Clone());
      }
      return clone;
    }
  }
  public class RepositoryList
  {
    public string SourceRoot = "";
    public List<RepositoryInfo> Repositories = new List<RepositoryInfo>();

    public RepositoryList Clone()
    {
      var clone = new RepositoryList();
      clone.SourceRoot = SourceRoot;
      foreach(var repo in Repositories)
      {
        Repositories.Add(repo.Clone());
      }
      return clone;
    }
  }
}
