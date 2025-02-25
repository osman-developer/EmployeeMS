using EmployeeMS.Domain.DTOs.EmployeeFile;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using Microsoft.AspNetCore.Hosting;
using System.IO;


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
        public bool Delete(int id)
        {
            //var productImageResult = _repo.GetQueryable().Where(b => b.ProductImageId == id);
            //var productImageDB = productImageResult.Select(p => new ProductImage()
            //{
            //    ProductImageId = p.ProductImageId,
            //    Path = p.Path,
            //    ProductId = p.ProductId,

            //}).FirstOrDefault();

            //if (productImageDB != null)
            //{
            //    if (File.Exists(productImageDB.Path))
            //    {
            //        // If file found, delete it    
            //        File.Delete(productImageDB.Path);
            //    }
            //}
            //return _repo.Delete(id);
            return false;
        }
        public bool Save(ICollection<AddEmployeeFileDTO> EmployeeFiles, int employeeId)
        {
            if (EmployeeFiles == null || !EmployeeFiles.Any())
            {
                return false;
            }

            // Prepare file IDs and fetch existing files from DB
            var fileIds = EmployeeFiles.Where(file => file.Id != 0).Select(file => file.Id).ToList();
            var employeeFilesInDb = _employeeFileRepo.GetQueryable()
                .Where(file => fileIds.Contains(file.Id))
                .ToList();

            // Delete existing files from disk
            DeleteExistingFiles(employeeFilesInDb);

            // Process new and updated files and get lists of them
            var (addEmployeeFileDTOList, updateEmployeeFileDTOList) = ProcessFiles(EmployeeFiles, employeeId, employeeFilesInDb);

            // Save changes to the database
            if (addEmployeeFileDTOList.Any())
            {
                _employeeFileRepo.AddRange(addEmployeeFileDTOList);
            }

            if (updateEmployeeFileDTOList.Any())
            {
                _employeeFileRepo.UpdateRange(updateEmployeeFileDTOList);
            }

            return true;
        }

        private void DeleteExistingFiles(List<EmployeeFile> employeeFilesInDb)
        {
            foreach (var file in employeeFilesInDb)
            {
                if (File.Exists(file.FilePath))
                {
                    File.Delete(file.FilePath);
                }
            }
        }

        private (List<EmployeeFile> addEmployeeFileDTOList, List<EmployeeFile> updateEmployeeFileDTOList) ProcessFiles(
            ICollection<AddEmployeeFileDTO> EmployeeFiles, int employeeId, List<EmployeeFile> employeeFilesInDb)
        {
            var addEmployeeFileDTOList = new List<EmployeeFile>();
            var updateEmployeeFileDTOList = new List<EmployeeFile>();

            foreach (var file in EmployeeFiles)
            {
                if (file == null) continue;

                string filename = file.File.FileName;
                string uid = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(filename);
                string pyhsicalPath = _env.ContentRootPath + "/Photos/";
                string path = pyhsicalPath + uid + filename;

                if (file.Id == 0 || file.Id == null) // Add new file
                {
                    // Save the new file to disk
                    using (var fileStream = new FileStream(Path.Combine(pyhsicalPath, uid + filename), FileMode.Create))
                    {
                        file.File.CopyTo(fileStream);
                    }

                    addEmployeeFileDTOList.Add(new EmployeeFile
                    {
                        FilePath = path.Trim(),
                        FileExtension = extension,
                        EmployeeFileTypeId = file.EmployeeFileTypeId.Value,
                        EmployeeId = employeeId
                    });
                }
                else // Update existing file
                {
                    var existingFile = employeeFilesInDb.FirstOrDefault(f => f.Id == file.Id);
                    if (existingFile != null)
                    {
                        existingFile.FileExtension = extension;
                        existingFile.EmployeeFileTypeId = file.EmployeeFileTypeId.Value;
                        existingFile.EmployeeId = employeeId;
                        existingFile.FilePath = path.Trim();

                        // Save the updated file to disk
                        using (var fileStream = new FileStream(Path.Combine(pyhsicalPath, uid + filename), FileMode.Create))
                        {
                            file.File.CopyTo(fileStream);
                        }

                        updateEmployeeFileDTOList.Add(existingFile);
                    }
                }
            }

            // Return the lists of new and updated files
            return (addEmployeeFileDTOList, updateEmployeeFileDTOList);
        }

    }
}
