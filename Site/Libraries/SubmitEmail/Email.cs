using EASendMail;
using Repository;
using System;

namespace Site.libraries.SubmitEmail
{
    public class Email
    {
        public class SubmitEmailSettings
        {
            public string ClientEmail { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
            public string SubjectMatter { get; set; }
            public string Link { get; set; }
            public string Button { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public IEmailSettings Email { get; set; }
        }

        public class EmailSettingsGmail : IEmailSettings
        {
            public string PrimaryDomain { get; set; } = "smtp.gmail.com";
            public int PrimaryPort { get; set; } = 587;
            public string UsernameEmail { get; set; } = "brhosss@gmail.com";
            public string UsernamePassword { get; set; } = "hunter2525";
            public string FromEmail { get; set; } = "brhosss@gmail.com";
            public bool Ssl { get; set; } = false;
        }

        public class EmailSettingsOutlook : IEmailSettings
        {
            public string PrimaryDomain { get; set; } = "smtp.live.com";
            public int PrimaryPort { get; set; } = 587;
            public string UsernameEmail { get; set; } = "brhosss@outlook.com";
            public string UsernamePassword { get; set; } = "hunter2525";
            public string FromEmail { get; set; } = "brhosss@outlook.com";
            public bool Ssl { get; set; } = false;
        }

        public static void SubmitEmail(SubmitEmailSettings Model)
        {
            IEmailTemplateRepository EmailTemplate = new EmailTemplateRepository();

            var Template = EmailTemplate.GetById(1).Template;

            Template = Template.Replace("@title", Model.Title);
            Template = Template.Replace("@image", Model.Image);
            Template = Template.Replace("@name", Model.Name);
            Template = Template.Replace("@message", Model.Message);
            Template = Template.Replace("@link", Model.Link);
            Template = Template.Replace("@button", Model.Button);
            Template = Template.Replace("@date", DateTime.Now.ToString("dd/MM/yyyy"));

            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = Model.Email.FromEmail;
            oMail.To = Model.ClientEmail;
            oMail.Subject = Model.SubjectMatter;
            oMail.TextBody = Template;

            SmtpServer oServer = new SmtpServer(Model.Email.PrimaryDomain);
            oServer.User = Model.Email.UsernameEmail;
            oServer.Password = Model.Email.UsernamePassword;
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, oMail);
        }
    }
}