using Domain;
using System;

namespace Repository
{
    public interface IEmailTemplateRepository : IRepositoryBase<EmailTemplate>, IDisposable
    {
    }
}