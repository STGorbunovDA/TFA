using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using Serilog.Events;

namespace TFA.API.Controllers;

public class SwitchLogLevelController : ControllerBase
{
    [HttpGet("switch-log-level")]
    public Task<IActionResult> SwitchLogLevel(
        [FromServices] LoggingLevelSwitch loggingLevelSwitch,
        CancellationToken cancellationToken)
    {
        loggingLevelSwitch.MinimumLevel = loggingLevelSwitch.MinimumLevel switch
        {
            LogEventLevel.Information => LogEventLevel.Error,
            _ => LogEventLevel.Information
        };

        return Task.FromResult((IActionResult)Ok());
    }
}