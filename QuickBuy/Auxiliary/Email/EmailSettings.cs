namespace Auxiliary.Email
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }
    }
}