using System.ComponentModel.DataAnnotations;

namespace FBAngularTW
{

    public class RedirectInfos
    {
        [Required]
        public string DefaultUrl { get; set; }
        public Redirectlist[] RedirectList { get; set; }
    }

    public class Redirectlist
    {
        public string Host { get; set; }
        public string Url { get; set; }
    }
}
