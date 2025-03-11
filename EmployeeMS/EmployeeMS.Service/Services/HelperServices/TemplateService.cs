using EmployeeMS.Domain.Interfaces.Services.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Service.Services.HelperServices
{
    public class TemplateService : ITemplateService
    {
        private readonly string _templateFolderPath;

        public TemplateService()
        {
            // Assuming your templates are stored in a "Templates" folder under the infrastructure project directory
            _templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
        }

        public async Task<string> LoadTemplateAsync(string templateName)
        {
            // Combine the base path with the template name to get the full path of the template file
            string templatePath = Path.Combine(_templateFolderPath, templateName);

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template '{templateName}' not found at '{templatePath}'.");
            }

            // Read the template file asynchronously
            return await File.ReadAllTextAsync(templatePath);
        }
    }
}