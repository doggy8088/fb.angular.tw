namespace FBAngularTW
{
    public class DomainUrlEntry
    {
        public string Domain { get; set; }

        public string Url { get; set; }
    }

    public class DomainUrlConfig
    {
        public List<DomainUrlEntry> Entries
        {
            get; set;
        }
    }

}
