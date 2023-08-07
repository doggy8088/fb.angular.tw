using System.ComponentModel.DataAnnotations;

namespace FBAngularTW.Models
{
    public class RoutePair
    {
        [Required]
        public string Url { get; set; }

        [Required]
        [Url]
        public string RedirectUrl { get; set; }
    }
}
