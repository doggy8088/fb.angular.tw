
using System;
using System.Collections.Generic;

namespace FBAngularTW
{
    public class MySitesOptions
    {

        public const string Position = "MySites";

        public string Default { get; set; } = String.Empty;

        public Dictionary<string, string> Site { get; set; } = new Dictionary<string, string>();
    }
}