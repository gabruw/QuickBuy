namespace Auxiliary.Email
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; } = 8955;

        public string UserEmail { get; set; } = "email@gmail.com";

        public string UserPassword { get; set; } = "12345";

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }
    }
}