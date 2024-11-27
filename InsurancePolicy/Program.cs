using InsurancePolicy.Data;
using InsurancePolicy.Exceptions;
using InsurancePolicy.Repositories;
using InsurancePolicy.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

namespace InsurancePolicy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
                options.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IDocumentService, DocumentService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IInsurancePlanService, InsurancePlanService>();
            builder.Services.AddTransient<IInsuranceSchemeService, InsuranceSchemeService>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<ISchemeDetailsService, SchemeDetailsService>();
            builder.Services.AddTransient<IPaymentService,PaymentService>();    
            builder.Services.AddTransient<IClaimService, ClaimService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(_ => { });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
