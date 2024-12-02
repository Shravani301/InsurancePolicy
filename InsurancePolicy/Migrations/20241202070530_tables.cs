using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsurancePolicy.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePlans",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ClaimDeductionPercentage = table.Column<double>(type: "float", nullable: false),
                    PenaltyDeductionPercentage = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "TaxSettings",
                columns: table => new
                {
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TaxPercentage = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSettings", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceSchemes",
                columns: table => new
                {
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SchemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchemeImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinAmount = table.Column<double>(type: "float", nullable: false),
                    MaxAmount = table.Column<double>(type: "float", nullable: false),
                    MinInvestTime = table.Column<int>(type: "int", nullable: false),
                    MaxInvestTime = table.Column<int>(type: "int", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MaxAge = table.Column<int>(type: "int", nullable: false),
                    ProfitRatio = table.Column<double>(type: "float", nullable: false),
                    RegistrationCommRatio = table.Column<double>(type: "float", nullable: false),
                    InstallmentCommRatio = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSchemes", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_InsuranceSchemes_InsurancePlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "InsurancePlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AdminFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgentFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AgentLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionEarned = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Agents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AgentEarnings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    WithdrawalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentEarnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentEarnings_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "CustomersQueries",
                columns: table => new
                {
                    QueryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersQueries", x => x.QueryId);
                    table.ForeignKey(
                        name: "FK_CustomersQueries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersQueries_Employees_ResolvedByEmployeeId",
                        column: x => x.ResolvedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DocumentName = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Employees_VerifiedById",
                        column: x => x.VerifiedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    InsuranceSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PremiumType = table.Column<int>(type: "int", nullable: false),
                    SumAssured = table.Column<double>(type: "float", nullable: false),
                    PolicyTerm = table.Column<long>(type: "bigint", nullable: false),
                    PremiumAmount = table.Column<double>(type: "float", nullable: false),
                    InstallmentAmount = table.Column<double>(type: "float", nullable: true),
                    TotalPaidAmount = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxSettingsTaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuranceSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsuranceSettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_InsuranceSchemes_InsuranceSchemeId",
                        column: x => x.InsuranceSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_InsuranceSettings_InsuranceSettingsId",
                        column: x => x.InsuranceSettingsId,
                        principalTable: "InsuranceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_TaxSettings_TaxSettingsTaxId",
                        column: x => x.TaxSettingsTaxId,
                        principalTable: "TaxSettings",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalRequests",
                columns: table => new
                {
                    WithdrawalRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalRequests", x => x.WithdrawalRequestId);
                    table.ForeignKey(
                        name: "FK_WithdrawalRequests_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_WithdrawalRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimAmount = table.Column<double>(type: "float", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankIFSCCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Claim = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claims_Agents_Claim",
                        column: x => x.Claim,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_Claims_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_InsurancePolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    CommissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CommissionType = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.CommissionId);
                    table.ForeignKey(
                        name: "FK_Commissions_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commissions_InsurancePolicies_PolicyNo",
                        column: x => x.PolicyNo,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    InstallmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsurancePolicyPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountDue = table.Column<double>(type: "float", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.InstallmentId);
                    table.ForeignKey(
                        name: "FK_Installments_InsurancePolicies_InsurancePolicyPolicyId",
                        column: x => x.InsurancePolicyPolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nomines",
                columns: table => new
                {
                    NomineeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    NomineeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomines", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_Nomines_InsurancePolicies_PolicyNo",
                        column: x => x.PolicyNo,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    paymentType = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    TotalPayment = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_InsurancePolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1c9b2dc2-7bc3-4890-80db-0d2793a6e497"), "Agent" },
                    { new Guid("58637d49-15ab-41ca-b59b-6bc963b79002"), "Employee" },
                    { new Guid("b11240f6-7e78-4b5f-a118-1e859b9a66aa"), "Customer" },
                    { new Guid("b5689c7d-54c8-4db9-a151-ddb82296fba4"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[,]
                {
                    { new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b"), "Madhya Pradesh" },
                    { new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a"), "Goa" },
                    { new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118"), "Rajasthan" },
                    { new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356"), "Tamil Nadu" },
                    { new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0"), "Assam" },
                    { new Guid("4f8eb6cf-4a03-4f70-851a-a357f2f01a56"), "Uttarakhand" },
                    { new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e"), "Arunachal Pradesh" },
                    { new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa"), "Jharkhand" },
                    { new Guid("633565eb-6d02-4e49-b6c0-27a54562a43e"), "Odisha" },
                    { new Guid("6d83779f-b709-4445-854a-93b9d335a657"), "Andhra Pradesh" },
                    { new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad"), "Haryana" },
                    { new Guid("74127793-39a9-42ab-a951-4601e03e07c3"), "West Bengal" },
                    { new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa"), "Maharashtra" },
                    { new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43"), "Himachal Pradesh" },
                    { new Guid("77543aef-73fa-4df8-8b7f-15a6633c570d"), "Tripura" },
                    { new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a"), "Karnataka" },
                    { new Guid("9d19abc9-6b39-4247-b230-2def035802f8"), "Nagaland" },
                    { new Guid("a03a8a96-5b8b-4a23-b28a-a81f86c88a3f"), "Punjab" },
                    { new Guid("aaf298f1-7671-4114-a026-628734203d69"), "Uttar Pradesh" },
                    { new Guid("b1da210e-dfe0-4918-b031-a58d139e1f63"), "Meghalaya" },
                    { new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79"), "Chhattisgarh" },
                    { new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a"), "Bihar" },
                    { new Guid("b7417275-3b44-4a18-95ef-764659f1fd42"), "Gujarat" },
                    { new Guid("c8c7dfb0-7d10-44d1-bcd0-18951f2fee52"), "Mizoram" },
                    { new Guid("d189e8e4-1548-4772-ad3d-2263e2b2f3f9"), "Sikkim" },
                    { new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4"), "Telangana" },
                    { new Guid("efd2331a-2076-411f-a358-ee59bc448d41"), "Manipur" },
                    { new Guid("f64c52b3-43cc-42d5-8906-59adce903a78"), "Kerala" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "StateId" },
                values: new object[,]
                {
                    { new Guid("007b6370-f319-433e-8127-ee5d01e7d76f"), "Canacona", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("0094c580-0d43-47d6-81c5-5906b55ce969"), "Raipur", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("02012eb4-e655-4897-8192-96815dcd9d65"), "Salem", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("035e0224-e9e9-4f8f-98fe-1f5f3fe014ac"), "Rajkot", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("04383073-349f-4621-8aa0-42c1c4d0a9b5"), "Ramgarh", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("06f89f66-ab5e-46e1-acde-1cee7d595355"), "Rajahmundry", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("077dd178-4cdf-4495-98d4-6b7107ba4adb"), "Korba", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("0828301a-461c-4d81-bcb8-88d915b0fc9b"), "Dhanbad", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("0b5b6490-5e64-4c92-8bdb-be626d86856a"), "Jamshedpur", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("0c7b0cea-e12b-4e72-8700-d91a29de68ee"), "Alappuzha", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("0d893ada-fbdf-4af7-a3d9-75ada1f75f9b"), "Panipat", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("0d988f5b-fd0f-4046-8e6f-b642ab2324f9"), "Bhilai", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("0eedb046-b307-40c3-a6e3-364ff77ec831"), "Mandi", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("12cbc0d3-ef35-4594-82c1-72aed247c32f"), "Satna", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("17ab8bb7-597b-474d-9838-de59faec038c"), "Bellary", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("1860c43a-e36d-4993-af50-920c231e6107"), "Tawang", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("18e6b830-5383-4083-a83e-e9c33dd49fbd"), "Jagdalpur", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("18eabfc3-13ad-4106-9d2a-b84b06fa285b"), "Mahasamund", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("1e89bfe2-ce92-4426-a34f-d37688f98c75"), "Chittoor", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("1f1ff35f-808e-4a8f-89f8-cfb72085e58e"), "Chennai", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("1f654052-97ed-44e4-9d36-9bb41bab0612"), "Meerut", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("1f93379c-49a9-4644-80fb-59670f718b6f"), "Kannur", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("20d1a422-67df-4534-9313-199c2ebd01f6"), "Kollam", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("212dba81-cb2b-4da2-94fd-df70cc4e0643"), "Ghaziabad", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("221980a8-2f0b-42a3-b69d-3b6f72f54933"), "Howrah", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("23a12cd3-82d0-4b38-bef4-77166f82b8a4"), "Jalpaiguri", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("23b3a393-ae50-4b9e-b5f3-9b04c4651ac0"), "Tirupati", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("23dcd089-7ea6-445b-ac64-02a9bf186787"), "Siddipet", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("24aae3f7-b7e4-4edc-96a2-a995270ef5b3"), "Anand", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("27994bb0-b7cd-4ddc-9d1a-8e1022cdb619"), "Khammam", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("293d9e46-c177-4180-a4e4-a8e27975e5a6"), "Jabalpur", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("29704c7f-73fc-4919-bced-28468fead987"), "Nagpur", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("2b6c0c67-6a9c-448c-8fda-dc061c5fcee8"), "Tirunelveli", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("2c081d23-a1cc-46c0-8909-e021b9fd87d9"), "Mehsana", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("2eb23eb2-7f4d-4fc3-950b-bfa5e3b22611"), "Darjeeling", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("31cb208f-b154-4f79-a1eb-21c6303e70a7"), "Mumbai", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("33799e24-2c22-4448-adee-3d36b0bb60c8"), "Asansol", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("33f06e97-aea9-4549-9071-a41555a10cda"), "Thane", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("3444ce5b-d47e-486f-becf-7655681326ff"), "Lucknow", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("344de080-970c-4cf0-9f32-331048fa6b0e"), "Ranchi", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("34f9b62d-ea79-490a-af4a-4dad846921d7"), "Agra", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("36bb632d-5a55-4850-a097-6fc68f25b6fb"), "Yamunanagar", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("379ec754-2710-4f6b-9af7-a753a22d0639"), "Hubli", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("3870ea27-08d2-4b49-a346-7536378cf825"), "Idukki", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("39ebc53c-fe4e-43aa-93c7-944536c34f02"), "Goalpara", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("3c42849f-2d87-4ba0-91b7-fa2f574cea8d"), "Vijayawada", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("406a6e4e-c441-469e-8a2a-f1e8e4c8ad8d"), "Ponda", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("40e38907-4085-41a5-80b2-5840503202a1"), "Rewa", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("4298cea0-88f3-452d-9fe5-3e9d24e4f89f"), "Erode", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("42b74495-9912-4dfd-9804-282e41bcdb7f"), "Surat", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("44b9051a-6622-4823-9723-ca569dc9089b"), "Jaipur", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("4725b578-8052-4577-97a0-f3536bbc4899"), "Ara", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("4822b05b-a284-48ec-93d3-3bccbbf62c64"), "Gurgaon", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("4afe0b03-6f0c-4e99-8949-7982a6db1c79"), "Raigarh", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("4bf3d9b0-0289-41f5-a760-dc5d77754c8d"), "Bicholim", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("4d600e4d-6516-4086-80ba-2f658d2df0c8"), "Giridih", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("4dc51ebe-b8e9-4821-9410-12e3242c3148"), "Hisar", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("4ec0b7d5-c803-4d14-b7b2-fc372cc30b54"), "Aurangabad", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("4ee7ef4f-7c39-4a37-8a92-bdc5b1162817"), "Thoothukudi", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("501f093c-462f-4f37-a449-d9a7325c5db4"), "Bokaro", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("50aa0904-2ca5-43db-830a-f62988e6ca11"), "Manali", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("52decb23-2f52-4d19-91e1-d5a0d9776678"), "Bhagalpur", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("53ed2525-d390-414b-bdc7-aa580f51c7f5"), "Bongaigaon", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("54ca0f83-f40a-4fe2-a883-48a6c8009055"), "Mangalore", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("555dce6a-29df-41d3-8ddc-e23d7f33ab12"), "Nellore", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("56a59d9c-bb1d-4688-8b87-645eb81cd2ed"), "Gandhinagar", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("5808cacd-5f12-427d-bb5a-90625151f1aa"), "Shimla", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("5ac626a2-2ba9-4ea0-bb32-d3275d7c41d5"), "Godda", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("5bd68d4c-bc60-483c-a93e-6f206e5432c2"), "Along", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("5dcbf175-7609-419d-98c6-f3ca9a839f65"), "Changlang", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("614ee91d-4e7d-4161-802f-ab241ddaf191"), "Vadodara", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("6251e138-2b22-4bcf-b604-eafbaedbbf6c"), "Pasighat", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("652c0588-88a5-470c-af58-c459098d6550"), "Bijapur", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("66491c4c-8f9e-460f-9379-92d429881dbc"), "Ziro", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("66ef1a2d-b25c-46ca-892a-58e427b4a132"), "Dindigul", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("67334f8a-81ae-49d8-ac5d-fdae3dd5e4ed"), "Mapusa", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("67ac2a39-240d-46f1-8269-81321ef44e76"), "Kullu", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("681055bb-fddc-4be8-bde2-d185523914cf"), "Kota", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("6a8b5f86-7974-454d-9be3-a6a31d51aa72"), "Sagar", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("6accec67-5b6c-454d-bc3d-5a6eba9458a0"), "Jaisalmer", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("6b1d7f43-779a-40b5-a2c6-165a33bf5002"), "Durg", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("6bd8278e-80bd-4e48-a9c8-b4cea211bab5"), "Dona Paula", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("6d0de6df-a174-41b4-9964-c39b99e99aff"), "Varanasi", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("6d477679-25b7-4bf5-891b-c94ef248f539"), "Bangalore", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("6dd5bce3-fc4a-4b14-b21b-908c4691a46f"), "Udaipur", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("6e7cb70a-a71d-4c76-aa12-d94957ebe668"), "Madurai", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("718def1e-7aed-4312-aa43-1080382c809d"), "Nahan", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("71ed8f23-7a2d-40af-9cfa-6486246897ec"), "Tiruchirappalli", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("73801b49-10e4-4387-8d6c-3143a4ab9cda"), "Moradabad", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("73a2d097-a551-4a2c-8dc9-cbc934cff151"), "Chhindwara", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("759c0ef8-299d-4488-86d4-15886325465c"), "Bhiwani", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("75cf6e4a-b2bb-4112-9551-f3c5230a2bf2"), "Sangli", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("777208b7-533e-407b-a4ed-158d67c9195f"), "Sikar", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("794467b6-bb9e-4169-8ab7-d99224257d63"), "Thiruvananthapuram", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("7a0adfe0-98bc-46f9-a427-a359086b6d88"), "Hazaribagh", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("7a4e03d8-6a62-49f5-b5e1-0ddd1d50131f"), "Adilabad", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("7eaa0234-1d79-4035-b6f5-603241af89dd"), "Indore", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("7ebb99e1-b52a-4680-88ce-010c88bf11b4"), "Muzaffarpur", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("86ea3417-5103-4ef4-901f-810d72d9ba62"), "Bareilly", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("88b5136b-fa96-4c53-87ba-3f605fe4583e"), "Silchar", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("88d00b9f-1f71-4724-8531-ecd9aebd5f56"), "Hamirpur", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("89a366f3-9016-4171-a96f-fcaa309577e2"), "Vasco da Gama", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("8a925cae-ba50-4ead-9fc3-9548bc62c205"), "Purnia", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("8c5d4794-e94d-4f7c-81ba-f3cf5625f1e5"), "Vellore", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("8cf223ea-4293-4954-b2ab-a80b9c7096b1"), "Durgapur", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("8efe5718-389a-4542-b85f-0abedd2301d8"), "Dharamshala", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("8f7702a8-c75c-4260-981e-be9b8ef11a4e"), "Navsari", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("909c9e83-77bd-4ae1-873b-016a681ac17a"), "Siliguri", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("913a720e-02b5-4924-94ab-4953bb8d72fb"), "Bhavnagar", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("915ac00c-85d1-4555-bf17-a512159ed3ec"), "Panaji", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("924a1d7d-9f83-4e31-9b89-941b70762c48"), "Margao", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("924ab68d-3510-407e-a5f9-cfd3dbae8977"), "Darbhanga", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("9456fe17-a843-4f61-ae8a-abd3f3e7e3ff"), "Hajipur", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("952fbdc0-179f-4f1f-9819-695bed4ab446"), "Faridabad", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("967f9a4b-5334-4495-8f2c-fd360209bbbc"), "Nizamabad", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("968e85b0-c57f-4eff-a76e-35eee2e02785"), "Tinsukia", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("98a80d44-39ea-47d0-a6bd-5dde21481804"), "Warangal", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("9f254664-7516-4242-84d4-bd9e9783fbb5"), "Coimbatore", new Guid("3b0948e5-2921-4ff9-8f72-e162c2910356") },
                    { new Guid("a0a1ec63-d6d4-4b9f-aae6-14b9b28f352f"), "Allahabad", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("a1090664-6204-418e-99e1-b86d3577c759"), "Sasaram", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("a12b47de-b4eb-4071-9305-94af1b4dd43d"), "Visakhapatnam", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("a1f59a0a-c390-47eb-ab88-9fa4ce079186"), "Begusarai", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("a3f3e376-cabc-4fb9-bc07-933bc6de5ce8"), "Jodhpur", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("a87fe530-365e-4b4f-8a86-3768dfd29257"), "Guwahati", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("aa5e5cb0-e8f9-4340-b2b3-e352187e6b94"), "Ajmer", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("ad8f29ae-9ac5-414a-8b68-32bf91fc5082"), "Kadapa", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("ae109122-55ae-4ab4-b130-e47dc94ea8e0"), "Barpeta", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("af3c5ebc-e0b8-4a8a-899a-9d7591983735"), "Shimoga", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("af603fa3-322e-413b-b856-8ae81fc71226"), "Kozhikode", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("af717360-d057-41bd-8105-6d99c86ba9ea"), "Roing", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("b046bf46-3318-4022-804f-d599eebefee9"), "Gwalior", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("b31a9975-8d4b-47c4-bd57-ded82ba86cc5"), "Ahmedabad", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("b37d0c7d-c6f4-435b-ade8-c142d8ae7b2c"), "Pune", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("b3f400da-b8a6-465f-9377-d2d6007732a5"), "Gaya", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("b4d4e16c-6cc1-4c3b-aa5a-c2aff405c38f"), "Haldia", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("b7477dcd-e83d-48b0-b3aa-8ecfa81d41e1"), "Patna", new Guid("b5358f30-bdfa-48fd-91c6-85fca42ace1a") },
                    { new Guid("b861d7e6-7987-4766-8803-e89ff2fd586e"), "Guntur", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("b874b19f-7d43-4718-9a9b-91aef9f4425c"), "Ujjain", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("b9d27a86-f30d-41a6-91b4-8539688df32f"), "Solan", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("ba6b84c9-9086-4d35-8f7f-a331ebb8134c"), "Jamnagar", new Guid("b7417275-3b44-4a18-95ef-764659f1fd42") },
                    { new Guid("bad2f6e8-def1-4dab-a1c2-1e0c20cb181a"), "Belgaum", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("bbd533ef-0d8f-4926-84ae-23bc119dff24"), "Chamba", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("be7e4748-d8c7-4a9d-ae5f-0197ec002847"), "Bilaspur", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("be90c6b8-02b3-4517-a31b-a577b8b00aa8"), "Ramagundam", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("c04dfc6b-f813-4d03-80d9-a9d23ee93367"), "Solapur", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("c15549c2-bc2f-4f81-8dfe-b23dbbcb0412"), "Tezpur", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("c17cd2a5-9b70-4c36-9259-f30b96adee5b"), "Nagaon", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("c35e6226-f83b-4c4d-bdb8-de823b750e4c"), "Bilaspur", new Guid("7746ef13-b271-4ed4-af7f-9b4c99964f43") },
                    { new Guid("c64f1fa8-5011-4dda-8680-31fe77208cba"), "Calangute", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("ccc849e9-3c29-49c4-b425-44fb58991c4e"), "Davanagere", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("cd95016d-6402-4939-ab93-99274cb57b24"), "Bardhaman", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("ce5b9dfa-c620-4487-88b8-6aab77a3e157"), "Aligarh", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("d09cb306-e0d6-4ca9-a6ba-6c2c83074654"), "Bharatpur", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("d18e9167-55df-4693-b66a-49fa8c692932"), "Bhopal", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("d25d8794-779e-485e-89be-595900a76b8f"), "Anantapur", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("d36afc8a-cd7d-4fda-be70-28b04dd14f11"), "Hyderabad", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("d5a804be-decc-4937-96b6-13b409365330"), "Karimnagar", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("d5de5f1e-36ab-456a-9ee0-3e46fbcc947a"), "Deoghar", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("d67ce160-af49-4526-95bc-6af0336c5bf1"), "Ratlam", new Guid("027777db-9497-422d-9a17-b8ecc4fe1e0b") },
                    { new Guid("d6dee7ff-8d1c-4eb3-b10d-1f8c6fd85f6a"), "Karnal", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("d9726185-aa4b-4270-8314-853b6728bd78"), "Nashik", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("dad2af30-887e-4426-8a30-4e3902fb9be7"), "Alwar", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("dbb240bb-0bb3-4ec9-ac5f-1e50d75a6556"), "Mahbubnagar", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("dbcf94b2-c85d-4be6-94f4-a1bd2ee6f790"), "Kolhapur", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("dbfa0381-5d8b-4392-a3a0-4792390a7519"), "Mysore", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("e1159421-0ade-4112-adb4-68d2d8fd7b21"), "Bomdila", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("e2c0d51c-7c27-44f9-b827-72ea03a33db4"), "Pakur", new Guid("62f4621d-15b4-45eb-a47c-ea3e30e1bbfa") },
                    { new Guid("e48b9726-3936-402b-a349-a3b979aabc5f"), "Palakkad", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("e54c157f-7b20-46e7-a741-6d5cd54ac287"), "Malda", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("e58b216f-fcb6-446f-8b85-fd63fc9400f9"), "Thrissur", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("e61eac6c-b6e6-4e64-a870-df8538014c2d"), "Ambikapur", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("e7af81f3-6738-4c73-a05a-f0c15284ec0f"), "Ambala", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("e8914ad9-62e8-4b21-bc9b-84ef3e08c5fe"), "Malappuram", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("e8bf3530-6313-4888-91b4-effac8229c31"), "Kakinada", new Guid("6d83779f-b709-4445-854a-93b9d335a657") },
                    { new Guid("e8d2474b-0dc1-4f47-bffe-7b076b0f8984"), "Kanpur", new Guid("aaf298f1-7671-4114-a026-628734203d69") },
                    { new Guid("ea6ba2c8-8742-4b59-bdde-684217ce1f93"), "Itanagar", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("ecc7d3e6-95d9-45fc-895c-494a2161e4ac"), "Tezu", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") },
                    { new Guid("f03da0e8-b585-478b-9cdb-c5c270ab7d1c"), "Dibrugarh", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("f141bd14-9f7e-4d67-99b5-512241974960"), "Mancherial", new Guid("d8fe2065-e1a1-4301-bc42-4113de1f84a4") },
                    { new Guid("f269623e-a9c6-4992-b2aa-0e82b7b85eed"), "Amravati", new Guid("76f5d4a9-d70c-4f5d-8634-b584919c96aa") },
                    { new Guid("f3587e05-697b-4720-be21-c0d31bafe1d5"), "Kochi", new Guid("f64c52b3-43cc-42d5-8906-59adce903a78") },
                    { new Guid("f4edff40-441b-4b4d-aedf-2d9c8efc6424"), "Porvorim", new Guid("20e525a4-c4de-46fb-9dd6-8389c9af202a") },
                    { new Guid("f60f7ea6-d169-4aa9-8f3f-eef6188ac3df"), "Bikaner", new Guid("2d79d1cd-a93e-4848-a71e-6fc971f16118") },
                    { new Guid("f87a7757-0554-4b74-862e-95c14abb2388"), "Sirsa", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("f973cbc4-1f7c-4312-9246-2e2357b3145f"), "Udupi", new Guid("7d8dc724-e2b4-4bb8-8a25-ce4a41a09d2a") },
                    { new Guid("f9ea7e81-cb56-4f53-ac18-30511a265587"), "Rohtak", new Guid("73f6ced4-8489-4c72-b0e8-76fa648e54ad") },
                    { new Guid("f9fff5fa-ac17-4739-a027-3c0e0b62774f"), "Rajnandgaon", new Guid("b4253dca-1df7-46fc-b1e7-f9559bc9ef79") },
                    { new Guid("fa7e7849-95a1-498c-9f42-544147ef8c4a"), "Kolkata", new Guid("74127793-39a9-42ab-a951-4601e03e07c3") },
                    { new Guid("fca14fb8-19d5-4c95-8e74-a4ecccffdee5"), "Jorhat", new Guid("491dfbd7-89bc-423a-beab-4b01aa2effd0") },
                    { new Guid("ffff8e8e-f9f4-4c32-b5ca-442555c3b616"), "Anini", new Guid("4f98e29d-7965-4707-a0f2-c7f590fec70e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentEarnings_AgentId",
                table: "AgentEarnings",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AddressId",
                table: "Agents",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_Claim",
                table: "Claims",
                column: "Claim");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CustomerId",
                table: "Claims",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_AgentId",
                table: "Commissions",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_PolicyNo",
                table: "Commissions",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AgentId",
                table: "Customers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersQueries_CustomerId",
                table: "CustomersQueries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersQueries_ResolvedByEmployeeId",
                table: "CustomersQueries",
                column: "ResolvedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CustomerId",
                table: "Documents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VerifiedById",
                table: "Documents",
                column: "VerifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_InsurancePolicyPolicyId",
                table: "Installments",
                column: "InsurancePolicyPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_AgentId",
                table: "InsurancePolicies",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_CustomerId",
                table: "InsurancePolicies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceSchemeId",
                table: "InsurancePolicies",
                column: "InsuranceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceSettingsId",
                table: "InsurancePolicies",
                column: "InsuranceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_TaxSettingsTaxId",
                table: "InsurancePolicies",
                column: "TaxSettingsTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceSchemes_PlanId",
                table: "InsuranceSchemes",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomines_PolicyNo",
                table: "Nomines",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PolicyId",
                table: "Payments",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_AgentId",
                table: "WithdrawalRequests",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_CustomerId",
                table: "WithdrawalRequests",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AgentEarnings");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "CustomersQueries");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Nomines");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "WithdrawalRequests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InsuranceSchemes");

            migrationBuilder.DropTable(
                name: "InsuranceSettings");

            migrationBuilder.DropTable(
                name: "TaxSettings");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "InsurancePlans");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
