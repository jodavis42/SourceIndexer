using System;

namespace SourceIndexer
{
  public enum VerbosityLevel { None, Error, Warning, Basic, Detailed}
  public class Logger
  {
    public VerbosityLevel Level = VerbosityLevel.Basic;
    public bool IsVerboseEnough(VerbosityLevel level)
    {
      return level <= Level;
    }
    public virtual void Log(VerbosityLevel level, string message)
    {

    }
  }

  public class ConsoleLogger : Logger
  {
    public override void Log(VerbosityLevel level, string message)
    {
      if(IsVerboseEnough(level))
        Console.WriteLine(message);
    }
  }

}
