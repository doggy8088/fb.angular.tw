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

        public string GetTargetUrl(string host)
        {
            if (_setting.CurrentValue.Mappings.TryGetValue(host, out string targetUrl))
                return targetUrl;

            return _setting.CurrentValue.DefaultUrl;
        }
    }
}