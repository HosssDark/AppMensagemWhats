namespace Site.libraries.SubmitEmail
{
    public interface IEmailSettings
    {
        string PrimaryDomain { get; set; }
        int PrimaryPort { get; set; }
        string UsernameEmail { get; set; }
        string UsernamePassword { get; set; }
        string FromEmail { get; set; }
        bool Ssl { get; set; }
    }
}