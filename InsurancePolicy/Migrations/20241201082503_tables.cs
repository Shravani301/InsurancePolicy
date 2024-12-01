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
                    RequiredDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsurancePlanPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSchemes", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_InsuranceSchemes_InsurancePlans_InsurancePlanPlanId",
                        column: x => x.InsurancePlanPlanId,
                        principalTable: "InsurancePlans",
                        principalColumn: "PlanId");
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "Installments",
                columns: table => new
                {
                    InstallmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurancePolicyPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountDue = table.Column<double>(type: "float", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomines", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_Nomines_InsurancePolicies_PolicyNo",
                        column: x => x.PolicyNo,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("2509e485-5d58-4510-bfd8-8a581e3936eb"), "Customer" },
                    { new Guid("8e7c1dae-e296-4a3d-9b3d-9fa6dff8a89c"), "Admin" },
                    { new Guid("e6e5e706-5b7c-4c32-8853-d48e506f82cf"), "Employee" },
                    { new Guid("ebaa212c-39cf-4a32-9b2f-594eddf3b441"), "Agent" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[,]
                {
                    { new Guid("06e34b22-5e8f-4919-a66c-01353c586278"), "Assam" },
                    { new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a"), "Chhattisgarh" },
                    { new Guid("0dfe8761-5cc9-450a-8258-b4120fa48023"), "Tripura" },
                    { new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac"), "Goa" },
                    { new Guid("1c29f72c-b869-4767-83c0-861a70fbc688"), "Mizoram" },
                    { new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496"), "Himachal Pradesh" },
                    { new Guid("315a2463-00c1-4751-94fe-70ebb081c00c"), "Karnataka" },
                    { new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5"), "West Bengal" },
                    { new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d"), "Haryana" },
                    { new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec"), "Telangana" },
                    { new Guid("3c8fc976-291f-4eee-b092-e5b8a6df3ea7"), "Sikkim" },
                    { new Guid("5ba1336b-7b96-4b21-863c-8057c4121846"), "Nagaland" },
                    { new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38"), "Uttar Pradesh" },
                    { new Guid("7a50b9b5-3c16-467d-8240-8b50799fa7f9"), "Manipur" },
                    { new Guid("86c46d03-5638-4fdf-88ae-eed5c80cd06e"), "Odisha" },
                    { new Guid("913dad25-0a5b-427d-9122-eaa2c8d0974f"), "Meghalaya" },
                    { new Guid("970a0f0f-e154-472b-bba7-238628aa7fe8"), "Uttarakhand" },
                    { new Guid("a39d4621-c132-493d-8487-a8ce389d09b8"), "Andhra Pradesh" },
                    { new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd"), "Arunachal Pradesh" },
                    { new Guid("aba44b47-1306-46f5-8389-25abf39a813f"), "Maharashtra" },
                    { new Guid("aeceb63a-0c40-4c36-b250-bfcfecdd8ad1"), "Punjab" },
                    { new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90"), "Kerala" },
                    { new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876"), "Jharkhand" },
                    { new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0"), "Bihar" },
                    { new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f"), "Gujarat" },
                    { new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2"), "Madhya Pradesh" },
                    { new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f"), "Rajasthan" },
                    { new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844"), "Tamil Nadu" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "StateId" },
                values: new object[,]
                {
                    { new Guid("015b877a-0df1-4f2e-8824-210c59995bb4"), "Pakur", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("01d34cdb-c1b4-4080-9f25-42ad9cbd9477"), "Guwahati", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("02a819f7-d685-44ac-9a52-62873de8ca7d"), "Gurgaon", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("039fc486-bb25-4b3f-a71b-c9f0a285b949"), "Moradabad", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("05a92e9b-6c86-4549-b7cb-c24c54cc0adf"), "Siliguri", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("0a11b3c2-b9d3-4535-b0dd-7910c4988e02"), "Solapur", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("0d1bf74f-3d35-4663-8d74-ed9e934f4293"), "Hazaribagh", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("0f2c6ff2-924e-4d56-97d3-598eee55dae3"), "Thrissur", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("0f306eff-4cb4-464d-bde5-a486e29a2f62"), "Durgapur", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("0f7de0ef-f381-478b-979d-a70d375ebf67"), "Malda", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("10a7422e-ee74-4aac-8146-fcda09a0146a"), "Agra", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("1110ecc1-7268-46b6-96bf-cdce2ce4dc7e"), "Surat", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("11211bc5-0bc0-48cc-9498-b766d2a4fc99"), "Kannur", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("1144c7be-93b0-49f5-95d3-cfc0a85e1ed2"), "Belgaum", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("12b3ce35-fbb4-49f3-aed2-e9060e7f9159"), "Faridabad", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("16a5bf3a-a864-417b-874b-b6d2054e7a98"), "Hyderabad", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("19d6414d-d9d8-4cdc-866f-ba4e185d4cb6"), "Visakhapatnam", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("1a032731-535b-490c-81ec-5699e9213921"), "Sagar", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("1a882fc1-1d91-4901-9659-77e4d69b2168"), "Kollam", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("1b146df8-7fb7-47f7-8d7e-56ca4307d2ec"), "Bongaigaon", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("1cff87f3-6f33-400d-a608-72262f9c6341"), "Darjeeling", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("1d9736a5-87d6-465b-a1f7-bec40b5b5d93"), "Jorhat", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("1d9a85b3-4e16-4144-ac1a-1c05d22266e7"), "Kullu", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("1dbefe9b-37da-43d9-9f82-bf689b2675b6"), "Dharamshala", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("2007b7a0-4818-43f2-bd76-78944254ca02"), "Kochi", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("223ffdf8-9f22-4fbd-90dc-a3770a176356"), "Pasighat", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("24492ce4-80bc-40c5-8734-5a3ab6ad2116"), "Madurai", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("27c551aa-7e82-4c07-927a-e08fd613cca2"), "Sangli", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("2859b149-de47-4f7f-885b-61fb640f1453"), "Udaipur", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("298ab6d0-2487-453d-bcd3-ddbaf364a7dd"), "Jaipur", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("2acf1956-7439-472e-b49c-50b8068fb622"), "Hajipur", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("2cdd45ce-cbed-469f-9e01-d8c5d03fe1b7"), "Bharatpur", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("2d0b3832-cb21-43a4-a75f-d6d8147e2d1e"), "Rewa", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("2d8414b9-866f-4256-8d7c-6a6534535132"), "Shimla", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("2f225afd-c6d9-417d-a285-261d6b2997dd"), "Anantapur", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("30715f75-06a8-4acd-80c9-fafe48caef42"), "Mehsana", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("30c8a224-fcb4-4df7-bf41-3697deb8a4a6"), "Margao", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("3102b12a-0349-4650-bea8-a34866b24836"), "Varanasi", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("3157ac04-7413-45de-8370-cd6c0b689d37"), "Vadodara", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("33635015-47da-4df7-8a48-4e365dd3787f"), "Rajahmundry", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("34c013c3-8884-4ca9-aff3-60bb291fb1da"), "Barpeta", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("34c3714a-11b3-4fe3-a8b8-74b20022175c"), "Dibrugarh", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("38e7a134-5122-4aa6-b403-89088f3f99dc"), "Salem", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("3d949990-b434-494f-a244-967d78a52d58"), "Patna", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("3ed7d825-c53a-4b45-b44d-8aa12bf673fc"), "Ghaziabad", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("3f32ead2-2caa-47eb-b275-66ec08f51064"), "Bangalore", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("407ac8ec-afd8-4c9a-b2cb-dc361e00246e"), "Thane", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("418fa19e-a703-47f8-9894-d936d91af79d"), "Ziro", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("41af65a4-1ef3-41e0-a653-7f2dcd60bbbb"), "Jabalpur", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("41c426a0-29e8-4e7d-bef2-e9cace5cfe5c"), "Ambikapur", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("41fb1bce-4e72-4630-a55b-cb4fd516f9c2"), "Bilaspur", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("429baa51-c37c-45cb-b88b-f38fdd3c64dc"), "Deoghar", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("42b6ad89-46bd-4591-8f0e-54401b3c40dc"), "Tezu", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("44f677a0-439b-4808-b6ca-f092dca57065"), "Panipat", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("45e54e63-4156-4d51-855f-6badaa8f5ac1"), "Nagpur", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("46226b94-e087-4240-969a-89d1f232220c"), "Dindigul", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("484b8db3-017d-4cb7-9a03-d6105ceda0ff"), "Bhiwani", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("49c60f8d-e66a-478d-b7a0-f111e1ef4b60"), "Jagdalpur", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("4ac086e1-7e07-40d7-a836-b6b62840c1ce"), "Tirupati", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("4b49e649-22a5-447c-ae99-3457a4eab9c8"), "Mumbai", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("4dfaef38-0cc4-41e2-839d-a4eab27d0132"), "Chhindwara", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("4ec7e0d6-89f0-49a6-a58f-22400f4b4ab7"), "Ramagundam", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("4ef93364-469a-4a8b-b7d6-cc2f2d9d583a"), "Porvorim", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("4fad5693-13cb-475a-8744-6d24642abc48"), "Vellore", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("4fbf7ebd-6ee5-460d-acd1-ad00307f436f"), "Anini", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("506fe318-5582-462a-90f8-b7f3bc7cccf5"), "Rohtak", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("556d8637-4d36-46b8-b0ec-25cfd800e20e"), "Bhopal", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("56963fbb-34ba-4091-bcb5-1b13ed313d2d"), "Adilabad", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("57ec8580-2430-4103-8a95-4f9d3014bd90"), "Guntur", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("5838588d-2301-4d65-813a-ea0876fd175f"), "Mandi", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("5bef9b31-c818-4a3b-a44e-fb3ddcfc1e47"), "Gaya", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("5c02555b-b058-4f08-834b-7548b2de603b"), "Erode", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("5d042d5e-652d-40da-be6c-a3948a5d0f2a"), "Godda", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("6080e45f-9adf-4b24-b0b6-9c1078650606"), "Bilaspur", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("60ff52fe-62ec-4b3f-a805-b1b63604a22a"), "Ramgarh", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("6215b765-1e01-454e-bbb1-6d3c0e6a8074"), "Bhavnagar", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("62da99f6-f69d-46ec-b1de-9eed0e2e8da6"), "Palakkad", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("63044337-68cb-4fb0-8a5a-3465fbdb24f2"), "Pune", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("64c3f02c-eaa4-46c6-ba3c-e05a65ecc890"), "Korba", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("6575463c-9fe2-4631-b89b-f0ce8b9dd94a"), "Aurangabad", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("66229bcc-0c5f-4510-a73b-8e51009de702"), "Mahbubnagar", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("66d6225e-9a59-4e3f-979e-59198794b68c"), "Sikar", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("6726670e-df86-4371-910f-e82907a0478d"), "Vasco da Gama", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("6924cea1-1c75-4b64-9edf-edb6145e503b"), "Navsari", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("6c05574e-0c28-4ba7-9f0d-8e095d642d75"), "Jaisalmer", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("6c349b07-6c93-4746-82d9-8bc63dec6f5f"), "Kanpur", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("6ed443c9-1f40-442c-afa6-decc8773f49a"), "Hisar", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("6f83567f-0b22-4e3f-b0ee-b7953d36790e"), "Thiruvananthapuram", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("6f9bdb16-b941-4110-a298-396005062bab"), "Amravati", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("6fba245f-ef46-4929-ad80-903129ecf0fa"), "Bardhaman", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("7111d06e-fcf4-4e84-8de1-e5c73e89a73d"), "Ratlam", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("71e02a80-a826-4a4e-9ba2-831c331026bc"), "Bellary", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("7265c458-943c-4f0f-aa0c-4baf5e074c9e"), "Durg", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("74834b0c-5a38-4b92-904c-015aaf4e7fd1"), "Bijapur", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("74e4a4a3-a93e-49d0-b59e-18ea26300b9c"), "Gwalior", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("754b66da-d384-42f2-8575-8e988f44634f"), "Dona Paula", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("75c03675-6737-4c42-8226-fc4e02be0ae6"), "Itanagar", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("76aab0dc-9dd4-4f53-bcf0-9c98c16992a8"), "Muzaffarpur", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("78700a09-6370-498d-ada6-1ec44ec8c926"), "Solan", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("79ccd89d-f04f-479c-929a-1231c98183c2"), "Bomdila", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("79d9beb2-9906-4ca9-977f-e37c01cdfa93"), "Tiruchirappalli", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("7acf95d7-fd08-4c78-9180-ae409748f98f"), "Tawang", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("7b8158b4-7039-48b1-8811-693c4ce596f0"), "Ranchi", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("7c6aac9f-ef37-4be2-b153-3c300cc421ed"), "Ponda", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("7de7d0b9-de6b-4b9c-9adc-0f6cb4f4dc5d"), "Coimbatore", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("7deb8bbd-a0df-49ef-9518-97b551f4c7d8"), "Ara", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("7e09451e-568b-40c4-9292-8d04ebfe8bd3"), "Davanagere", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("7eacd343-7dfa-419c-856d-759830b255a7"), "Alappuzha", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("7f11c137-8a6f-4068-9097-2cd1fc84e453"), "Purnia", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("81bbdf1a-1c3f-4917-b65e-6d6252666e4c"), "Rajnandgaon", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("852b511b-cf61-4468-97ad-49191f03465d"), "Tirunelveli", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("85723339-73b1-4d01-befb-6d293b38f0a3"), "Kakinada", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("860ba9a4-7cb4-42bb-beeb-889cbcd3c0eb"), "Mangalore", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("86c2b0c9-ecce-446f-9cf6-2529473a2058"), "Ahmedabad", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("88dfbe47-26ba-489c-8e77-b9985f192f71"), "Sirsa", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("8d883e31-4b6b-4788-a86b-f72b3996c038"), "Sasaram", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("91779aac-d2dc-4e0f-9e0d-03aa9d6e0ec5"), "Ajmer", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("918c04a0-3ae7-4b11-a5a9-12c7a87a55c5"), "Hubli", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("934ee263-76df-495a-ba8d-022f6d58afe8"), "Panaji", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("9965f910-c750-4057-847b-2da75c3b30c6"), "Satna", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("9d399bb0-7c88-43b9-bba3-774a4ffeca2e"), "Dhanbad", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("9ea37df0-1dab-49e3-a60e-29a2b867aa63"), "Mahasamund", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("9ea9a83e-3566-49f6-b8d2-f8298db72ab9"), "Vijayawada", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("9ffb0191-36de-4baf-91fc-1452d51a0af5"), "Kozhikode", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("a1811c38-529c-4eb9-b576-39e4f0759baa"), "Darbhanga", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("a4a01446-9e80-498b-a5cb-21cad6fe4a01"), "Bokaro", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("a4f012b8-53bd-42f9-9524-12fbead99662"), "Anand", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("a5732865-c3c3-4954-9d80-78aa7529cc2b"), "Karimnagar", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("a678b467-7e90-4ff3-be33-9f384e9eb5ce"), "Jamnagar", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("a824ae6d-3b66-4b3e-ab1d-abe79895862b"), "Thoothukudi", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("a8c89ce8-0008-4019-9ecc-cd336d53dedb"), "Mysore", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("aa42dfdc-6c25-4991-8d32-f747abc75571"), "Bikaner", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("aaee77cc-a9cc-444c-9828-f8aad7f609d0"), "Ambala", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("b1a2e1d2-35f6-4b86-b597-29131d325d91"), "Alwar", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("b31092bc-5024-4776-b14c-c3957a49613d"), "Bhilai", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("b4b4c223-a12c-4b3a-88e5-0ec1fe081bf6"), "Idukki", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("b5f67504-f268-4c2b-8079-3eeef0560b4b"), "Ujjain", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("b5f89567-01da-4a76-a0f1-85ee508bb885"), "Raigarh", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("bb1ff29e-4a2f-4958-9dc5-e3f6c0e67dd1"), "Hamirpur", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("bc8a2acd-19e6-479d-ba01-f33696126cee"), "Tezpur", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("be8a3918-ccb0-4f0c-bfc2-4eeca0cb6782"), "Asansol", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("c3783259-8f0b-4140-b11c-b1443f2021c5"), "Gandhinagar", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("c3cf1067-e786-4ff8-88c5-14954f84ab0a"), "Malappuram", new Guid("bc830474-74f0-4463-95b0-9ae8c5532d90") },
                    { new Guid("c491d9fe-d1a1-429c-9a6a-2b734a9e8a29"), "Siddipet", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("c7fcd2f8-d0e0-47b4-b7e8-dc8d58127ccc"), "Rajkot", new Guid("d54b72d0-bd4e-43a4-88a0-6a02e1b4c45f") },
                    { new Guid("c83edb14-5a7b-4821-aa3d-bd28f1d02c79"), "Bicholim", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("c8855613-17e9-479b-aecf-fb8906026c7f"), "Howrah", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("c9fcb9db-c596-4f4e-bbea-71ab02ae96ee"), "Nagaon", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("cb180aed-3b87-4f71-b94e-013726fdf24f"), "Bhagalpur", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("ccb4daca-0641-4321-a3c6-6c6a5cab3123"), "Haldia", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("cd343d63-ed12-4c46-a15e-beda0cf0cea7"), "Nellore", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("cdc09c4a-3559-470f-add5-ac424e0d1c91"), "Kota", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("ce760849-d89c-4ed1-992f-dd4b940efb0f"), "Karnal", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("ceb4b6c7-41eb-4d5d-a7b1-81aac7ff7856"), "Jamshedpur", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("d16f221c-ea1a-422b-9367-7c5bddbc0607"), "Chennai", new Guid("f8e1e68e-6477-4279-ae63-9c2dbd77c844") },
                    { new Guid("d289f479-de69-4043-8675-798a43075a9d"), "Nahan", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("d3ce4dd2-a9c6-4372-b86e-ab97f8a3047b"), "Tinsukia", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("d3ddc091-f351-41a4-ae31-47cc686f0dde"), "Lucknow", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("d4612a47-0684-4700-8e7f-a42999463b91"), "Manali", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("d5ee3aaa-bb2e-4aa5-b049-75c105c534d7"), "Kolkata", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("d69cde13-7a7c-4d4f-9bd0-6d55169f4cbf"), "Chittoor", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("d844fe5f-80c6-4554-abef-ba4d39c73434"), "Nizamabad", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("d86d6fc2-4972-4c54-a37f-71f90997360c"), "Aligarh", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("d90b1fc1-0bcb-4cb2-a254-42803b51e1cd"), "Shimoga", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("d9bc506c-feb5-48b4-a3a5-e076f23b33b2"), "Changlang", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("da58dc1a-f83d-4806-9f6f-84d49b2f035b"), "Allahabad", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("daff6b20-feeb-488c-9494-ecb417782324"), "Along", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("dd24d29c-d1e3-4ad1-a505-3587e55e99be"), "Yamunanagar", new Guid("36551f4f-d528-46c3-a372-9f5dcba9bf3d") },
                    { new Guid("dde0cd34-3e30-43f2-bd0f-8c69bb462495"), "Mancherial", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("de6f31f6-7149-476e-9388-67361e590e5c"), "Udupi", new Guid("315a2463-00c1-4751-94fe-70ebb081c00c") },
                    { new Guid("e239b733-1de1-4be6-afd8-59ce0f07ed25"), "Chamba", new Guid("2308a211-d1e3-4184-be0b-3fb6a388b496") },
                    { new Guid("e266e3a7-c212-4ee0-a652-085d97eaaf74"), "Indore", new Guid("dc2d87ab-6f5e-4b8c-b396-3d4f501d12c2") },
                    { new Guid("e3477ec1-004b-4ba3-8510-c602efe6b2d1"), "Khammam", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("e40b56ef-b4cf-4bf1-b697-884df0ed4d08"), "Warangal", new Guid("3ad852bf-5e9b-4e92-97aa-1bc5734f38ec") },
                    { new Guid("e498dedd-2030-4181-b081-45ce2ed07e2a"), "Silchar", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("e5965de2-2d70-4a63-9993-a0ad7743f067"), "Giridih", new Guid("c15c4637-d5b6-43da-ab0c-ea4fc80ef876") },
                    { new Guid("e61e5e8c-9f54-4359-aeca-2a6e342aae53"), "Jodhpur", new Guid("dc8eeac3-5677-44ec-9f43-638a3e61938f") },
                    { new Guid("ec8766a2-dfc9-4aaf-8a8c-534bc973c05a"), "Kolhapur", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("edc9bea2-b95c-4c24-8f8a-ad979c2db3dd"), "Canacona", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("ee363344-8910-4175-8be0-22347e1b93dd"), "Begusarai", new Guid("c22fe6e6-7dc0-425d-ae46-e4b03a40d8f0") },
                    { new Guid("eec50505-2299-46b6-9d2f-4e80c9af4dac"), "Roing", new Guid("a4fe84dc-f31f-402c-a429-3d6979aa36fd") },
                    { new Guid("eef8c71e-94e0-468a-a099-d82615eeac9a"), "Kadapa", new Guid("a39d4621-c132-493d-8487-a8ce389d09b8") },
                    { new Guid("f00244ec-b099-4e52-be26-8efe0a9c8a72"), "Raipur", new Guid("074e13ee-c86b-45b2-92e3-bfa72a2f267a") },
                    { new Guid("f0dd85b0-bcd0-4883-a089-1ea06a75a7c7"), "Calangute", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("f53c3eb3-19bc-4fc1-a6a7-e9482e101195"), "Jalpaiguri", new Guid("33f6bbb3-f18b-4e1c-a6fa-3556913a2bb5") },
                    { new Guid("f5455393-3228-4d2d-9ac0-034d9e8a4586"), "Mapusa", new Guid("1221c640-eb37-40e4-9aa0-93a8ea46eeac") },
                    { new Guid("f8dfe399-5494-4b0e-a9ad-103002f3453c"), "Nashik", new Guid("aba44b47-1306-46f5-8389-25abf39a813f") },
                    { new Guid("fb006cb4-e91d-4b08-922c-9a5663661d27"), "Goalpara", new Guid("06e34b22-5e8f-4919-a66c-01353c586278") },
                    { new Guid("fbf33150-c985-42be-8b3d-d2ecb9213d54"), "Meerut", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") },
                    { new Guid("fdf0398a-5dd8-4248-bd9f-990a420355c0"), "Bareilly", new Guid("6e5c570f-5c6a-4d1c-8597-2ed590ed4e38") }
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
                name: "IX_InsuranceSchemes_InsurancePlanPlanId",
                table: "InsuranceSchemes",
                column: "InsurancePlanPlanId");

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
