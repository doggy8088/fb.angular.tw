using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FBAngularTW
{
    public class RedirectService
    {
        private readonly IOptionsMonitor<RedirectSetting> _setting;

        public RedirectService(IOptionsMonitor<RedirectSetting> shortUrlOption)
        {
            _setting = shortUrlOption;
        }

        internal string GetTargetUrl(HttpContext ctx)
        {
            string host = ctx.Request.Host.Host;
            if (_setting.CurrentValue.Mapping.TryGetValue(host, out string targetUrl))
                return targetUrl;

            return _setting.CurrentValue.DefaultUrl;
        }
    }
}