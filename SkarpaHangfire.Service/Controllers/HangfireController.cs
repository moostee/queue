using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SkarpaHangfire.Core.Data;
using System;
using System.Globalization;

namespace SkarpaHangfire.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {
        public HangfireController()
        {

        }

        [HttpGet]
        [Route("fireandforget")]
        public IActionResult FireAndForget([FromQuery]string url)
        {
           return Ok(BackgroundJob.Enqueue(() => HangfireUrl.Call(url)));
        }
        [HttpGet]
        [Route("scheduleonce")]
        public IActionResult FireAtAParticularTime([FromQuery]string url, string scheduledDate)
        {
            DateTime MyTime = DateTime.ParseExact(scheduledDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToUniversalTime();
            DateTime MyTimeInWesternAfricanTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(MyTime, "W. Central Africa Standard Time");
            return Ok(BackgroundJob.Schedule(() => HangfireUrl.Call(url), MyTimeInWesternAfricanTime));
        }
        
        [HttpGet]
        [Route("recurring")]
        public IActionResult RecurringTasks([FromQuery]string url, [FromQuery]string cronExpression)
        {
            RecurringJob.AddOrUpdate(url, () => HangfireUrl.Call(url), cronExpression);
            return Ok();
        }
    }
}
