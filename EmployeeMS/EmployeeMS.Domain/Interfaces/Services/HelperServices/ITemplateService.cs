using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.HelperServices
{
    public interface ITemplateService
    {
        Task<string> LoadTemplateAsync(string templateName);
    }
}
