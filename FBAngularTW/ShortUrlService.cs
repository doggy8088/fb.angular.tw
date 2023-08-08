namespace FBAngularTW
{
    public class ShortUrlService
    {
        private const string POSITION = "ShortUrl";
        private const string DEFAULT = "default";

        private readonly IConfiguration _configuration;

        public ShortUrlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetShortUrl(string host)
        {
            var positionOptions = _configuration.GetSection(POSITION);
            var shortUrl = positionOptions.GetValue<string>(host);
            return shortUrl ?? positionOptions.GetValue<string>(DEFAULT)!; 
        }
    }
}
