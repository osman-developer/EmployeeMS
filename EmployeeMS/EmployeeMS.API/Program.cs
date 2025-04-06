using AutoMapper;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Interfaces.Services.HelperServices;
using EmployeeMS.Infrastructure;
using EmployeeMS.Infrastructure.Repository;
using EmployeeMS.Service.Services.AppServices;
using EmployeeMS.Service.Services.HelperServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EmployeeMSContext>(options =>
options.UseSqlServer(
          (builder.Configuration.GetConnectionString("DefaultConnection"))));


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IFilterBuilderService, FilterBuilderService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeFileService, EmployeeFileService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IContractStatusService,ContractStatusService>();
builder.Services.AddScoped<IEmployeeContractService, EmployeeContractService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        // Make sure the exact origin is specified     
        policy.WithOrigins("http://localhost:4200")   // Allow your Angular app
              .AllowAnyHeader()                    // Allow any headers
              .AllowAnyMethod()                     // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
              .AllowCredentials();                  // Allow credentials (cookies, authentication headers)

    });
});


builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();
