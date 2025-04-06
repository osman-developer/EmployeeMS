using AutoMapper;
using EmployeeMS.Domain;
using EmployeeMS.Domain.DTOs.EmployeeContract;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Interfaces.Services.HelperServices;
using EmployeeMS.Domain.Pagination;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EmployeeMS.Service.Services.AppServices
{
    public class EmployeeContractService : IEmployeeContractService
    {
        private readonly IGenericRepository<EmployeeContract> _employeeContractRepo;
        private readonly ITemplateService _templateService;
        private readonly IContractStatusService _contractStatusService;
        private readonly IMapper _mapper;
        public EmployeeContractService(IGenericRepository<EmployeeContract> employeeContractRepo, ITemplateService templateService, IContractStatusService contractStatusService, IMapper mapper)
        {
            _employeeContractRepo = employeeContractRepo;
            _templateService = templateService;
            _contractStatusService = contractStatusService;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            return _employeeContractRepo.Delete(id);
        }

        public async Task<GetEmployeeContractDTO> Get(int id)
        {
            var contractResult = _employeeContractRepo.GetQueryable().Where(b => b.Id == id)
                .Include(c => c.Employee)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType).FirstOrDefault();
            return _mapper.Map<GetEmployeeContractDTO>(contractResult);
        }

        public async Task<List<GetEmployeeContractDTO>> GetAll()
        {
            var contractsDB = await _employeeContractRepo.GetAll();
            return _mapper.Map<List<GetEmployeeContractDTO>>(contractsDB);
        }

        public async Task<PagingResult<GetEmployeeContractDTO>> GetAllPagedAsync(PagingParams<EmployeeContract> pagingParams)
        {
            var includeExpression = new Expression<Func<EmployeeContract, object>>[] { c => c.Employee, c => c.ContractStatus, c => c.ContractType };
            var contractsResult = await _employeeContractRepo.GetAllPagedAsync(pagingParams, includes: includeExpression);

            var contractsDTOs = _mapper.Map<List<GetEmployeeContractDTO>>(contractsResult.Items);

            return new PagingResult<GetEmployeeContractDTO>
            {
                Items = contractsDTOs,
                TotalCount = contractsResult.TotalCount,
                PageSize = contractsResult.PageSize,
                CurrentPage = contractsResult.CurrentPage
            };
        }

        public bool Save(AddEmployeeContractDTO contract)
        {
            if (contract.Id != null)
            {
                // If the contract has an Id, update the existing contract
                var existingContractDTO = _mapper.Map<EmployeeContract>(contract);
                return _employeeContractRepo.Update(existingContractDTO);
            }

            // Check if there is an existing contract that is not "terminated"
            var existingActiveContract = _employeeContractRepo
                .GetAll(c => c.EmployeeId == contract.EmployeeId &&
                     c.ContractStatus.StatusName != Constants.ContractStatus.terminated &&
                     c.ContractStatus.StatusName != Constants.ContractStatus.on_leave)
                .Result
                .FirstOrDefault();

            // If there is an active contract, prevent creating a new one
            if (existingActiveContract != null)
            {
                return false; // You can handle this more gracefully depending on your business logic (e.g., throw an exception or return a custom message)
            }

            // Get the status for "active"
            var activeStatus = _contractStatusService.GetContractStatusByName(Constants.ContractStatus.active).Result;
            contract.ContractStatusId = activeStatus.Id;

            // Map the contract to DTO and add it
            var contractDTO = _mapper.Map<EmployeeContract>(contract);
            return _employeeContractRepo.Add(contractDTO);
        }

        public async Task<(byte[] pdfBytes, string sanitizedFileName)> GenerateEmployeeContractPdfReportWithTemplateAsync(int contractId)
        {
            var contract = await Get(contractId);
            // Load the HTML template using the TemplateService
            string htmlTemplate = await _templateService.LoadTemplateAsync("EmployeeContractTemplate.html");

            // Replace placeholders with actual data (e.g., contract data)
            string contractDataHtml = await GetContractDataHtmlAsync(contract);
            htmlTemplate = htmlTemplate.Replace("{{ContractData}}", contractDataHtml);

            // Generate the PDF and get the byte array
            byte[] pdfBytes = ConvertHtmlToPdf(htmlTemplate);

            // Retrieve the contract name (or any field you want) to set as the file name
            string contractName = await GetContractNameAsync(contract); // Method to fetch the contract name

            // Sanitize the contract name to make it a valid file name
            string sanitizedFileName = Path.GetInvalidFileNameChars()
                .Aggregate(contractName, (current, c) => current.Replace(c.ToString(), string.Empty));

            // Return both PDF bytes and sanitized contract name
            return (pdfBytes, sanitizedFileName);
        }

        private async Task<string> GetContractNameAsync(GetEmployeeContractDTO contract)
        {
            return contract?.Name ?? "EmployeeContractReport"; // Default name if contract is not found
        }

        private async Task<string> GetContractDataHtmlAsync(GetEmployeeContractDTO contract)
        {
            return $@"
                    <tr>
                        <td>{contract.Id}</td>
                        <td>{contract.Employee.Name}</td>
                        <td>{contract.Position}</td>
                        <td>{contract.StartDate}</td>
                        <td>{contract.EndDate}</td>
                        <td>{contract.Salary:C}</td>
                        <td>{contract.ContractType}</td>
                    </tr>";
        }

        public byte[] ConvertHtmlToPdf(string htmlTemplate)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    // Create PdfWriter from memory stream
                    using (var writer = new PdfWriter(memoryStream))
                    {
                        // Create PdfDocument from the writer
                        using (var pdf = new PdfDocument(writer))
                        {
                            // Convert a simple HTML string to PDF and write to the PdfDocument
                            var converterProperties = new ConverterProperties();
                            HtmlConverter.ConvertToPdf(htmlTemplate, pdf, converterProperties);
                        }
                    }

                    // Return the byte array representing the PDF document
                    return memoryStream.ToArray();
                }
                catch (Exception ex)
                {
                    // Log detailed exception
                    Console.WriteLine($"Error occurred while converting HTML to PDF: {ex.Message}");
                    throw;
                }
            }
        }

    }
}
