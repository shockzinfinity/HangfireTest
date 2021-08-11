using Hangfire;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HangfireTest.Services
{
  public class SendEmailsJob
  {
    public SendEmailsJob(IConfiguration configuration)
    {
      // you can ask for configuration or any other dependency the job might need via DI
    }

    [JobDisplayName("Send {0} emails")]
    [AutomaticRetry(Attempts = 3)]
    public async Task Execute(int count)
    {
      for (int i = 0; i < count; i++)
      {
        await Task.Delay(1000);
      }
    }
  }
}