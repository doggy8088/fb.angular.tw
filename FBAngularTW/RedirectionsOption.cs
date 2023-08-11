namespace FBAngularTW;

// ReSharper disable once ClassNeverInstantiated.Global
public class RedirectionsOption
{
    public string FallbackUrl { get; set; } = null!;
    
    public RedirectionMapping[] Mappings { get; set; } = null!;
}