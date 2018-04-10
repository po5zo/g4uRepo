using g4u.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace g4u.Controllers
{
    [Route("/api/settings")]
    public class SettingsController : Controller
    {       
        private readonly AuthSettings settings;
        public SettingsController(IOptionsSnapshot<AuthSettings> options)
        {
            this.settings = options.Value;
        }

        [HttpGet("auth")]
        public IActionResult GetAuthSettings()
        {
            return Ok(settings);
        }
    }
}