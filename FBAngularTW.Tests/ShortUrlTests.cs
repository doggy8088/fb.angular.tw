using Microsoft.Extensions.Options;

namespace FBAngularTW.Tests;

public class ShortUrlTests : IClassFixture<ServiceProviderClassFixture<IOptionsMonitor<ShortUrlOptions>>>
{
    private readonly ServiceProviderClassFixture<IOptionsMonitor<ShortUrlOptions>> _factory;
    public ShortUrlTests(ServiceProviderClassFixture<IOptionsMonitor<ShortUrlOptions>> fixture)
    {

        _factory = fixture;
    }

    [Theory]
    [MemberData(nameof(UrlProvider))]
    public void Snapshut_options_can_mapping_short_url_to_origin_url(string shortUrl, string expectUrl)
        => Assert.Equal(expectUrl, _factory.Service.Get(shortUrl).Url);

    public static IEnumerable<object[]> UrlProvider()
    {
        yield return new object[] { "fb.angular.tw", "https://www.facebook.com/groups/augularjs.tw" };
        yield return new object[] { "yt.angular.tw", "https://www.youtube.com/c/AngularUserGroupTaiwan/videos" };
        yield return new object[] { "ts.angular.tw", "https://willh.gitbook.io/typescript-tutorial" };
        yield return new object[] { "vscode.angular.tw", "https://marketplace.visualstudio.com/items?itemName=doggy8088.angular-extension-pack" };
        yield return new object[] { "cli.angular.tw", "https://youtu.be/v4_YsDZbs3g" };
        yield return new object[] { "rx6.angular.tw", "https://youtu.be/BA1vSZwzkK8" };
        yield return new object[] { "install.angular.tw", "https://gist.github.com/doggy8088/15e434b43992cf25a78700438743774a" };
        yield return new object[] { "", "https://www.facebook.com/will.fans" };
    }

    [Fact]
    async public Task New_settings_is_apply_when_appsetting_changed()
    {
        try
        {
            var options = _factory.Service;

            Assert.Equal(string.Empty, options.Get(EXPECT_KEY).Url);

            File.Copy(CHANGED_APPSETTINGS_PATH, APPSETTINGS_PATH, true);

            // configuration invoke delay 250 ms when reload
            await Task.Delay(500);

            Assert.Equal(EXPECT_VALUE, options.Get(EXPECT_KEY).Url);

        }
        finally
        {

            File.Copy(ORIGINAL_APPSETTINGS_PATH, APPSETTINGS_PATH, true);
        }
    }

    private string APPSETTINGS_PATH = "appsettings.json";
    private string ORIGINAL_APPSETTINGS_PATH = "appsettings.origin.json";
    private string CHANGED_APPSETTINGS_PATH = "appsettings.changed.json";
    private string EXPECT_KEY = "expect_key";
    private string EXPECT_VALUE = "expect_value";
}
