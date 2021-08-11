using System;

namespace HangfireTest.Services
{
  public class JobTestService : IJobTestService
  {
    public void FireAndForgetJob()
    {
      Console.WriteLine("Hello from a Fire and Forget job!");
    }

    public void ReccuringJob()
    {
      Console.WriteLine("Hello from a Scheduled job!");
    }

    public void DelayedJob()
    {
      Console.WriteLine("Hello from Delayed job!");
    }

    public void ContinuationJob()
    {
      Console.WriteLine("Hello from Continuation job!");
    }
  }
}