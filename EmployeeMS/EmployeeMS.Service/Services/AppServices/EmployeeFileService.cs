using EmployeeMS.Domain.DTOs.EmployeeFile;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using Microsoft.AspNetCore.Hosting;


namespace EmployeeMS.Service.Services.AppServices
{
    public class EmployeeFileService : IEmployeeFileService
    {
        private readonly IWebHostEnvironment _env;

        private readonly IGenericRepository<EmployeeFile> _employeeFileRepo;
        public EmployeeFileService(IGenericRepository<EmployeeFile> employeeFileRepo, IWebHostEnvironment env)
        {

            _employeeFileRepo = employeeFileRepo;
            _env = env;
        }
        public bool Save(ICollection<AddEmployeeFileDTO> EmployeeFiles, int employeeId)
        {
            if (EmployeeFiles != null)
            {
                var addEmployeeFileDTOList = new List<EmployeeFile>();
                foreach (var file in EmployeeFiles)
                {
                    if (file != null)
                    {
                        string filename = file.File.FileName;
                        string uid = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(filename);
                        string pyhsicalPath = _env.ContentRootPath + "/Photos/";
                        string path = pyhsicalPath + uid + filename;

                        // Save the file to disk
                        using (var fileStream = new FileStream(Path.Combine(pyhsicalPath, uid + filename), FileMode.Create))
                        {
                            file.File.CopyTo(fileStream);
                            //await file.File.CopyToAsync(fileStream);
                        }

                        addEmployeeFileDTOList.Add(new EmployeeFile
                        {
                            FilePath = path.Trim(),
                            FileExtension = extension,
                            EmployeeFileTypeId = file.EmployeeFileTypeId.Value,
                            EmployeeId = employeeId

                        });

                    }
                }
                _employeeFileRepo.AddRange(addEmployeeFileDTOList);
                return true;
            }
            return false;

        }
    }
}
