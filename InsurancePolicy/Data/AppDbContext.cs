using InsurancePolicy.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InsuranceScheme> InsuranceSchemes { get; set; }
        public DbSet<SchemeDetails> SchemeDetails { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<Policy> InsurancePolicies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.AgentId);
                entity.Property(e => e.AgentId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.EmployeeId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.ClaimId);
                entity.Property(e => e.ClaimId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocumentId);
                entity.Property(e => e.DocumentId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<InsurancePlan>(entity =>
            {
                entity.HasKey(e => e.PlanId);
                entity.Property(e => e.PlanId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<InsuranceScheme>(entity =>
            {
                entity.HasKey(e => e.SchemeId);
                entity.Property(e => e.SchemeId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.HasKey(e => e.PolicyId);
                entity.Property(e => e.PolicyId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<SchemeDetails>(entity =>
            {
                entity.HasKey(e => e.DetailId);
                entity.Property(e => e.DetailId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            var adminRoleId = Guid.NewGuid();
            var agentRoleId = Guid.NewGuid();
            var employeeId=Guid.NewGuid();
            var customerId=Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin" },
                new Role { Id = agentRoleId, Name = "Agent" },
                new Role { Id = employeeId,Name="Employee"},
                new Role { Id= customerId, Name="Customer"}
                );
        }        
    }
    
}
