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
                        onDelete: ReferentialAction.Cascade);
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
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentName = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Documents_InsurancePolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    InstallmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_Installments_InsurancePolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId");
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
                    { new Guid("09efe447-e886-4db7-97c5-10823caa8605"), "Agent" },
                    { new Guid("8a6c2dfe-c4a9-4c01-88e9-75beb685047e"), "Customer" },
                    { new Guid("b881f3eb-008d-44d1-92dd-19b5f2f5b457"), "Employee" },
                    { new Guid("f46c4ed6-f199-4e56-9509-a1e8385f6036"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[,]
                {
                    { new Guid("042288c7-69f9-485b-83f6-3fe9016ddbb8"), "Punjab" },
                    { new Guid("04cc4f56-401d-414c-8811-049850e5ce61"), "Chhattisgarh" },
                    { new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be"), "Jharkhand" },
                    { new Guid("07e6dfa8-d161-44c7-b93d-5da076e75f13"), "Tripura" },
                    { new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91"), "Arunachal Pradesh" },
                    { new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40"), "Andhra Pradesh" },
                    { new Guid("247c6ec8-78ad-4e78-8d2f-2010fd421f75"), "Sikkim" },
                    { new Guid("2f50e124-bcc3-4e2a-a976-0a553678a644"), "Uttarakhand" },
                    { new Guid("4d811250-8578-4936-973d-69b96b3c03b3"), "Meghalaya" },
                    { new Guid("5d3999d7-1d55-493c-b1e1-ee09c71fa7ea"), "Nagaland" },
                    { new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f"), "Kerala" },
                    { new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a"), "Madhya Pradesh" },
                    { new Guid("7146bfea-51be-42a6-beda-24d263b72a90"), "Karnataka" },
                    { new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2"), "Himachal Pradesh" },
                    { new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7"), "Uttar Pradesh" },
                    { new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d"), "West Bengal" },
                    { new Guid("9c8a7e3f-4c61-49af-9fe1-0e918f833a26"), "Manipur" },
                    { new Guid("9f214661-19a9-436c-9d37-ebcf06893101"), "Tamil Nadu" },
                    { new Guid("a121f82a-8b74-46b3-b025-63295382f709"), "Telangana" },
                    { new Guid("a28a9644-3f9a-4268-98a6-514a2d52950d"), "Mizoram" },
                    { new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c"), "Assam" },
                    { new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67"), "Bihar" },
                    { new Guid("bf0eced8-f4b1-4e46-a1e3-6c1a4cd556fb"), "Odisha" },
                    { new Guid("ca1d8293-55da-4692-b537-5f96f380a430"), "Maharashtra" },
                    { new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd"), "Rajasthan" },
                    { new Guid("d85af509-b286-44d1-ad26-957cf87f68f8"), "Gujarat" },
                    { new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45"), "Goa" },
                    { new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37"), "Haryana" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "StateId" },
                values: new object[,]
                {
                    { new Guid("008ec713-ea2d-48f0-9d34-33cdbdcc3bc8"), "Nashik", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("00c3c661-e4c4-4a89-980b-f09518dc233d"), "Changlang", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("00dc75c8-d611-4fa7-88aa-772657b487db"), "Purnia", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("03a06e3f-542f-47b7-ae11-993afb43af7e"), "Chamba", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("03f17026-c65b-458e-af86-834afb3bfabf"), "Jabalpur", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("0618230a-e947-4ba1-8955-9aa525084c39"), "Thrissur", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("0ab7c864-0eca-414c-bc40-a208ddb95474"), "Nagpur", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("0b5c1f6b-d384-4286-b39b-19befad5b89d"), "Mapusa", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("0df703f4-ac7f-45cd-85a8-17f1b4ab0842"), "Bellary", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("0e484ac1-8a58-4e8b-8165-7a3cd0033222"), "Jamshedpur", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("1019edb5-b4ab-49f8-bea5-03355f37b5bf"), "Bongaigaon", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("125abc46-4d98-4d2a-bbd7-fdc91283b5db"), "Sirsa", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("12ed6999-fc04-457b-863d-c263bcbff2fa"), "Shimoga", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("1468a2ab-91cc-439c-aee8-43bba14c3673"), "Madurai", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("15853dbd-21eb-4e30-879e-6ce657ebff0b"), "Surat", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("15d212ae-3ead-44ab-9498-8d529e922794"), "Kollam", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("18cad84b-4bb0-4371-b071-e6f82691f342"), "Porvorim", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("18dad116-24d6-445f-bd0f-f05b34adede8"), "Hazaribagh", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("1949381c-d2d8-40a9-8a00-fd219e2a99de"), "Gwalior", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("1bda5068-ac77-44b2-bc6f-cf531a7095b3"), "Sasaram", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("1c6d5f6b-2580-47eb-a03b-2e4fc9c5c249"), "Begusarai", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("1d2aa3b8-1faa-4549-b556-670e7adc4de9"), "Pakur", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("1d4dedbd-2f6b-4d1c-89d5-355e88677c12"), "Solapur", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("1e41234a-bcac-4950-936e-06ab57a435d7"), "Bhiwani", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("1e9d9b16-5817-4045-ac56-b0103df8ee1f"), "Ramagundam", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("1eda4e19-428a-45c8-9585-eeaa959f4ea1"), "Chennai", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("1fc4cac7-a297-4118-a6ac-e805ab90c8e2"), "Udupi", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("23ef89c5-281d-4bf1-9682-6a6c77b3c3f8"), "Dibrugarh", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("246f8e76-d6ae-4870-ae2c-25edfadb8e13"), "Thane", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("25f7a542-dda3-4493-91f0-33face58193f"), "Jagdalpur", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("268ecb5d-59d3-4fbf-a9e7-61beaf34f8ad"), "Ara", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("27898ff7-e45f-42b3-97b4-26144aa86407"), "Mancherial", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("27908009-075c-4704-926d-580d6c29eb62"), "Thiruvananthapuram", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("2870a9b2-2e97-4bda-b001-09e28889c98e"), "Nellore", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("28ef16ce-c9bc-4a2d-a2a1-67e348840373"), "Guwahati", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("294bcfa7-b153-4672-b6b9-b4879b25cae9"), "Lucknow", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("29e0cf72-38c9-4aad-8eda-8af2e3a8d85c"), "Kakinada", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("2a939ac9-f61e-444a-99f7-f693c2c20e0b"), "Pune", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("2aaacb97-8ce9-4d09-8636-27a66c47ee06"), "Salem", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("2ced74f2-ec14-4c22-a1fd-bb8278db041f"), "Indore", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("2d5d8d86-bafc-4a21-a715-d12072b9fb22"), "Bareilly", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("2dfc198c-ecd5-43e4-9df0-9a68ac3ffa2c"), "Hubli", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("300f45ec-fe7c-43b0-8bb4-6a75a45189e8"), "Moradabad", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("307b1672-26b8-4381-a3b0-08b3ee8b1bb1"), "Asansol", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("364997dd-d620-4db2-af84-b5f62add9468"), "Hamirpur", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("376832b8-ab45-40f6-84bf-534e3a1ebe80"), "Itanagar", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("38376da7-8b37-49fd-8955-605d706d30f2"), "Rewa", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("399aadee-f7e9-4d7a-b69f-0e0b213c6871"), "Anand", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("3a8fb278-b3d5-4424-b237-2b10dacc571c"), "Canacona", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("3d3a999d-c7bc-4b7a-ae65-c8c1882b0401"), "Siliguri", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("406e35c8-ef9a-4e55-a9fe-af0a509eb631"), "Howrah", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("4149682f-3701-4eb4-a7c8-fd16d87a2569"), "Rajahmundry", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("42be8453-1248-4ece-80c9-a34bb4acebc2"), "Jorhat", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("45b65f47-5b7b-4101-b826-89fc0efea868"), "Deoghar", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("45ceff07-9ca3-4a5a-a18d-23720a285d87"), "Tinsukia", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("4b935fe8-7ad5-4f9b-a2c5-851fa83188dd"), "Nizamabad", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("4bb250d4-062e-4851-b3d1-2cf974ff3834"), "Silchar", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("4d4d9c7c-7355-4f28-a4f0-8a0d809759c9"), "Bhagalpur", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("4f88043d-b464-4dcf-af8d-2a92b064d3f7"), "Bilaspur", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("4fc82300-aeee-48de-87e0-c3d82c320cbd"), "Ahmedabad", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("50e37a08-f275-481b-b6df-858fe4e5483f"), "Ratlam", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("51db35da-d6c5-46e4-a9dc-90fd8adbfdd4"), "Calangute", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("529286ae-d3d3-470f-af4a-aed44c787806"), "Kannur", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("53e4710d-9a9c-4624-b669-b7d6f45a6f5e"), "Margao", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("545c46d8-cb06-4937-ad2e-8142182ada65"), "Aligarh", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("5707f1ff-5db6-4f94-bb94-18d9ceaf2544"), "Bilaspur", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("58a3819b-d2d0-48b0-aa51-35ab994cde27"), "Jaipur", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("599b8616-9a03-4992-b9ec-f28f5b9f96ff"), "Panipat", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("5a5557bf-c534-4ebd-9c37-4cf9bf8c3886"), "Ambikapur", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("5af5faa3-b391-4555-86c8-85fb2d7570fa"), "Jodhpur", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("5bf32853-51f3-4396-a9e2-f46982f37ac5"), "Tirupati", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("5def813a-c640-4811-a677-1421aec7f491"), "Along", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("5e00260e-eaf1-401a-b83b-00c88deec57c"), "Tezu", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("62a1d260-5d63-4134-bb86-764748f92448"), "Guntur", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("62cde59b-e146-4902-a6f9-07ff1287a7fb"), "Sikar", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("6405afc1-0b10-4e15-acff-2953a529c6b1"), "Korba", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("6872ea2e-275b-4a29-877d-96b5537cb7c1"), "Vellore", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("6adffea4-7fae-44e0-a9cd-2fa747375e1b"), "Anini", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("6c991895-24fe-46a4-b920-17c73da11651"), "Goalpara", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("708d0bfb-c04d-4dc7-92db-2da09c9df62b"), "Panaji", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("74537b67-5fec-4e47-8f0d-69549b347a7e"), "Tiruchirappalli", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("747655c2-5b6c-4c68-a2bf-ce6f626eefc5"), "Chittoor", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("7626b42b-7091-4707-a539-c093615315d7"), "Bhopal", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("7915a129-2be7-4b20-ae47-f6c13640d6fc"), "Bikaner", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("7a8c18b1-3518-49df-bbe9-bab2c8fdc77e"), "Thoothukudi", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("7b653036-2975-4d2f-af6e-720cf79f0cb5"), "Nagaon", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("7c408cba-3d0d-42e9-a445-f69f793cb9d6"), "Kozhikode", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("7f1b754e-bfca-46fd-a4c3-000c983b7ce8"), "Ajmer", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("7ffa3643-e14f-4e2d-9064-7db499dcc105"), "Bhavnagar", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("814476fa-077f-4353-ad86-423bb45ae822"), "Idukki", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("82637c87-dab2-4135-a906-073502f8c436"), "Mysore", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("85ad2fbd-3115-4f44-ae91-4b90b9db0c97"), "Sangli", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("85e66ddf-bcd5-4d41-a26e-cf05a0a879ee"), "Vadodara", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("88b0da6b-faa9-4cb5-83b1-de613f03a14d"), "Khammam", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("8a87f943-52f6-48b9-bbd5-645e2c379839"), "Dharamshala", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("8abf2aa2-3c45-4847-9ed1-364562513ca5"), "Bijapur", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("8c2df6cd-87ba-4f07-b546-633b854bf3c6"), "Solan", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("8ce21a05-e997-43d8-9b97-3b5369505a63"), "Bardhaman", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("8d184555-a93d-449b-a115-931418c13b33"), "Ghaziabad", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("8de63e8f-34a3-4a01-a171-a97f4f458854"), "Raipur", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("8ec52f1a-22bb-4ece-8ee3-35d56ebb83f2"), "Bomdila", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("8f82188d-7da2-4a49-9904-933b88e716db"), "Dindigul", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("926748fb-a8b5-408f-9f0a-7b8c9ff7dc3f"), "Rajnandgaon", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("94237a95-7a91-48c7-b282-d564eebe4365"), "Allahabad", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("9505c1d1-00a4-426c-bc19-f675a8306626"), "Patna", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("95564dce-e0c4-4564-a4fb-058ec02d1285"), "Malappuram", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("965ae95a-3c3c-4cbd-9be1-4523d2ff470c"), "Ujjain", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("972f3c0d-9a1d-49b4-8173-008a62afa184"), "Erode", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("97a1fc56-a390-4790-992f-6d0d2e389fc5"), "Visakhapatnam", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("9b035887-d6c1-494b-a7c3-a70d5adfc463"), "Navsari", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("9cf6cac3-c048-49ae-9300-7b6843f61c7d"), "Varanasi", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("9d2dd7dc-d210-446a-ba20-8da08de6cc12"), "Coimbatore", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("9d3871b1-7dbc-47e1-aeb9-91b5f881ecbc"), "Muzaffarpur", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("9e44d481-93ce-4c27-bde8-8f0aa505c535"), "Malda", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("a073083a-69ea-4f8a-abba-1e2f1d6485b0"), "Agra", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("a13adfff-f0d1-4ee7-b8ff-4b8cb66bce01"), "Durgapur", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("a162f568-f517-46d9-a36e-55895c30020c"), "Ranchi", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("a171f434-4083-46cf-9240-94f736d4689a"), "Tirunelveli", new Guid("9f214661-19a9-436c-9d37-ebcf06893101") },
                    { new Guid("a39a0ce7-a304-4a42-a5fc-ac173b478182"), "Faridabad", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("a43d7ee1-44d3-433a-add5-e9c4523e54a6"), "Mumbai", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("a4df921d-0644-44d2-a247-d188911c6605"), "Tawang", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("a5570291-11ee-4ca3-8800-7790e5f2eb53"), "Kadapa", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("a55aefdf-e748-48ee-802d-4cca440dbe20"), "Pasighat", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("a56a6356-3f45-47d2-8a90-8871d17cbadd"), "Vijayawada", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("a5d9abf5-014e-4cd1-9fdf-ec6e0ed80b5d"), "Jamnagar", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("a62caa20-acf7-447e-bc32-cae9350bd6ee"), "Rajkot", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("a77d6d08-c984-4088-aa55-1b7b03894f7f"), "Ambala", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("a8029b6f-9b89-4330-bdcb-117aeecdaec8"), "Jalpaiguri", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("a9670429-666e-4fd4-9313-7c0ca1cf8f90"), "Gandhinagar", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("aaeb7729-4576-4453-b438-cc7c9e85de30"), "Warangal", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("ab9b5f65-0e7f-4f8f-be30-1a0ac0d50a25"), "Giridih", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("ac135408-414a-4d2c-b062-2182e8b0582f"), "Palakkad", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("ac23a4d4-80ac-4067-9bc7-c35e5e9752dc"), "Kolkata", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("ae438fee-813e-4dde-a83f-31b838133496"), "Nahan", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("ae652b34-189f-45d5-b0d7-830ed1bbf97b"), "Durg", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("ae6c286c-5ebc-42c1-81fc-f47347648740"), "Mandi", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("ae7c9bb7-46d0-4681-96d8-e2369f8c5d27"), "Vasco da Gama", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("aef13e6a-d338-4de2-a6c4-81a74c887f17"), "Tezpur", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("b3ba2046-39f5-478c-81da-d404b8a252fc"), "Mangalore", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("b517ab3f-094e-448c-b220-e0d8a1f36320"), "Rohtak", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("b5c099e1-c461-4938-aa30-13692e2cb1dc"), "Mahbubnagar", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("b6581d38-fca1-43b9-a2df-83c93d2452cf"), "Alappuzha", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("b7767337-d796-4736-803e-5774004bd679"), "Alwar", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("b8c75fac-ef40-4a07-a67b-6e401366ced2"), "Haldia", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("b94f91ca-794b-402e-bc58-ba0855f207fb"), "Gaya", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("b96594f6-85d6-4bfa-95cb-bbba23703d67"), "Raigarh", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("ba52c7f2-29b4-46f6-b75f-240cfaa0e656"), "Dona Paula", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("ba576795-74d0-4f70-98d6-1b0a43499734"), "Kullu", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("bd7d4fa8-e8bf-4665-82d3-2315657a2e39"), "Kolhapur", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("bf0f9591-54cc-4fe0-8560-1cd723836e18"), "Yamunanagar", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("c03afa66-7280-42a0-83d2-f7e95664dc33"), "Kota", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("c178c515-7864-469e-9927-e774f0b6bc38"), "Udaipur", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("c1f0bb68-ae46-4ef4-a2f2-6b5798c5cc62"), "Ziro", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("c505187e-47f4-42e8-bf1e-bd05971d624a"), "Bhilai", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("c8d6bf7f-8ed6-47a7-8e58-cb7944ca5c63"), "Darjeeling", new Guid("89b5b4e0-efd3-4689-ba11-22079e885e3d") },
                    { new Guid("c94a3fe9-a525-47c4-8e6b-e5782f192243"), "Barpeta", new Guid("aa1c4a8c-8d8e-4ac9-b7e1-b2ae6506688c") },
                    { new Guid("cacd8228-9a35-4e43-8292-904de9f95e32"), "Bokaro", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("cb753b4c-1d4c-4438-ad9e-0ab6bf5a5b91"), "Chhindwara", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("cffb6be4-999b-4cbc-a8ba-253cea798772"), "Karimnagar", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("d2188e44-c96e-4a56-a0e0-043db6763345"), "Meerut", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("d2b1e5f1-66ab-4d9e-b346-e0448420ba74"), "Roing", new Guid("16af42d2-153e-46b7-a039-dcf8460c2d91") },
                    { new Guid("d9967e32-678f-43c6-8017-a1d81fecba78"), "Bharatpur", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("d9cff225-3363-4b02-a82b-0c666f82ab05"), "Davanagere", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("db619c4a-797e-41ca-966c-ff796c30b278"), "Mehsana", new Guid("d85af509-b286-44d1-ad26-957cf87f68f8") },
                    { new Guid("db998871-0532-4cc1-bed0-1e3d6390d310"), "Bicholim", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("dcc5228e-3785-419c-827e-272e63774f11"), "Darbhanga", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("dd676fa9-d1d6-4dad-8637-b68cd8503ef0"), "Ramgarh", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("e04a533a-9332-46b8-bcd1-7afcfff27d53"), "Sagar", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("e09387b4-5e86-43c0-b05a-4961c4ac2b48"), "Dhanbad", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("e4ea66be-262a-4d3e-92d6-45cc8edac114"), "Kochi", new Guid("65cc5127-d7f4-4fd2-b1b3-a211be5c5b6f") },
                    { new Guid("e60c1bb8-b213-4771-9863-873cc9496142"), "Amravati", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("e66a8cb0-a2dc-4d79-a343-cbab1f732216"), "Anantapur", new Guid("1a4c0cbf-9f2d-43e3-a3ec-1e34e4f93e40") },
                    { new Guid("e6e3eeb7-9432-4563-8b5e-a47448cf5183"), "Godda", new Guid("07195103-6193-4b4f-8a19-532fbbaaf6be") },
                    { new Guid("e7fd46fc-64a6-4655-b22f-23aa857f9295"), "Siddipet", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("e9011a3f-71b8-4a5d-8e0f-4faacb9f9df7"), "Hisar", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("e9bf0a14-cece-4c4e-8eeb-386bf4e087aa"), "Adilabad", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("eab89a2c-f395-4377-950e-f59fa24b2ded"), "Hyderabad", new Guid("a121f82a-8b74-46b3-b025-63295382f709") },
                    { new Guid("eb813051-a60f-4499-9d83-48eb1069ecbf"), "Satna", new Guid("6b9885c2-33d0-49b2-ada2-85b15d60304a") },
                    { new Guid("ec726501-a30d-48e4-a617-34502d4a5143"), "Mahasamund", new Guid("04cc4f56-401d-414c-8811-049850e5ce61") },
                    { new Guid("ecb6af1b-8abb-43c1-a4aa-525668271079"), "Kanpur", new Guid("7c208edc-4ab9-4e5f-8854-9696615432e7") },
                    { new Guid("f07264ae-c37d-4634-8341-e75d4259bb69"), "Manali", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") },
                    { new Guid("f18c3de9-9976-4237-8435-f3776ae7317e"), "Jaisalmer", new Guid("d6105a3d-0e49-41b8-82e3-fcf2253134dd") },
                    { new Guid("f31ffe15-2551-4d31-806f-0c3ff1c8d903"), "Bangalore", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("f413ce10-89c2-4fa6-a1b7-9241f3753331"), "Hajipur", new Guid("abb85bbf-2e9b-4555-8cac-904d755b3e67") },
                    { new Guid("f65640db-fa1b-4722-bec1-6123ec7bf5db"), "Karnal", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("f8842d7e-3684-48f5-a171-20fb34fbb438"), "Ponda", new Guid("e83dede9-f1e6-4750-8228-70570fb6bf45") },
                    { new Guid("fc3c9ee9-1d02-4c1b-8146-c62bd4ed3ab0"), "Gurgaon", new Guid("fb50bb28-bcac-4843-8ace-3707816d5b37") },
                    { new Guid("feb07749-fce6-4000-99d0-69a5824e400c"), "Aurangabad", new Guid("ca1d8293-55da-4692-b537-5f96f380a430") },
                    { new Guid("fefe67a3-13d8-4fc4-93c0-4dc4b0df3c1a"), "Belgaum", new Guid("7146bfea-51be-42a6-beda-24d263b72a90") },
                    { new Guid("ff03e1db-6b35-4c2f-8a22-8443f7f0b9f2"), "Shimla", new Guid("797c9ff8-ed39-4b00-b2df-532e5d51fcb2") }
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
                name: "IX_Documents_PolicyId",
                table: "Documents",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VerifiedById",
                table: "Documents",
                column: "VerifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_PolicyId",
                table: "Installments",
                column: "PolicyId");

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
