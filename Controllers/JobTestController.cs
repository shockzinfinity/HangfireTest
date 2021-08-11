using Hangfire;
using HangfireTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HangfireTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class JobTestController : ControllerBase
  {
    private readonly IJobTestService _jobTestService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    public JobTestController(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
    {
      _jobTestService = jobTestService ?? throw new ArgumentNullException(nameof(jobTestService));
      _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
      _recurringJobManager = recurringJobManager ?? throw new ArgumentNullException(nameof(recurringJobManager));
    }

    [HttpGet("/FireAndForgetJob")]
    public IActionResult CreateFireAndForgetJob()
    {
      _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());

      return Ok();
    }

    [HttpGet("/DelayedJob")]
    public IActionResult CreateDelayedJob()
    {
      _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(60));

      return Ok();
    }

    [HttpGet("/ReccuringJob")]
    public IActionResult CreateReccuringJob()
    {
      _recurringJobManager.AddOrUpdate("jobId", () => _jobTestService.ReccuringJob(), Cron.Minutely);

      return Ok();
    }

    [HttpGet("/ContinuationJob")]
    public IActionResult CreateContinuationJob()
    {
      var parentJobId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
      _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());

      return Ok();
    }
  }
}