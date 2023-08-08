using FBAngularTW.ShortUrlLib.Models;
using Microsoft.Extensions.Options;

namespace FBAngularTW.ShortUrlLib
{
    public class ShortUrlService
    {
        private readonly IOptionsMonitor<ShortUrlSetting> _shortUrlOption;

        public ShortUrlService(IOptionsMonitor<ShortUrlSetting> shortUrlOption)
        {
            _shortUrlOption = shortUrlOption;
        }

        public string GetUrl(string host)
        {
            var shortUrlLookup = _shortUrlOption.CurrentValue.UrlLookup;
            if (shortUrlLookup == null)
            {
                throw new Exception("No Url Lookup found");
            }

            if (shortUrlLookup.ContainsKey(host))
            {
                return shortUrlLookup[host];
            }

            return shortUrlLookup["default"];
        }
    }
}
