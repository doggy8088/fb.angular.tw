using Microsoft.Extensions.Options;

namespace FBAngularTW.Tests;

public class ShortUrlTests : IClassFixture<ServiceProviderClassFixture<IOptionsSnapshot<ShortUrlOptions>>>
{
    private readonly ServiceProviderClassFixture<IOptionsSnapshot<ShortUrlOptions>> _factory;
    public ShortUrlTests(ServiceProviderClassFixture<IOptionsSnapshot<ShortUrlOptions>> fixture) {
        _factory = fixture;
    }

    [Theory]
    [MemberData(nameof(UrlProvider))]
    public void Snapshut_options_can_mapping_short_url_to_origin_url(string shortUrl, string expectUrl)
        => Assert.Equal(expectUrl, _factory.Service.Get(shortUrl).RedirectUrl);

    public static IEnumerable<object[]> UrlProvider() {
        yield return new object[] { "fb.angular.tw", "https://www.facebook.com/groups/augularjs.tw" };
        yield return new object[] { "yt.angular.tw", "https://www.youtube.com/c/AngularUserGroupTaiwan/videos" };
        yield return new object[] { "ts.angular.tw", "https://willh.gitbook.io/typescript-tutorial" };
        yield return new object[] { "vscode.angular.tw", "https://marketplace.visualstudio.com/items?itemName=doggy8088.angular-extension-pack" };
        yield return new object[] { "cli.angular.tw", "https://youtu.be/v4_YsDZbs3g" };
        yield return new object[] { "rx6.angular.tw", "https://youtu.be/BA1vSZwzkK8" };
        yield return new object[] { "install.angular.tw", "https://gist.github.com/doggy8088/15e434b43992cf25a78700438743774a" };
        yield return new object[] { "", "https://www.facebook.com/will.fans" };
    }
}