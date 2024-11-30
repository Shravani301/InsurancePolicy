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
                name: "InsuranceSchemes",
                columns: table => new
                {
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SchemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSchemes", x => x.SchemeId);
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
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
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
                name: "InsurancePlanInsuranceScheme",
                columns: table => new
                {
                    PlansPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemesSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlanInsuranceScheme", x => new { x.PlansPlanId, x.SchemesSchemeId });
                    table.ForeignKey(
                        name: "FK_InsurancePlanInsuranceScheme_InsurancePlans_PlansPlanId",
                        column: x => x.PlansPlanId,
                        principalTable: "InsurancePlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePlanInsuranceScheme_InsuranceSchemes_SchemesSchemeId",
                        column: x => x.SchemesSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PremiumType = table.Column<int>(type: "int", nullable: false),
                    PremiumAmount = table.Column<double>(type: "float", nullable: false),
                    SumAssured = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_InsuranceSchemes_InsuranceSchemeId",
                        column: x => x.InsuranceSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeDetails",
                columns: table => new
                {
                    DetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
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
                    InsuranceSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_SchemeDetails_InsuranceSchemes_InsuranceSchemeId",
                        column: x => x.InsuranceSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "SchemeId",
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
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "PaymentPolicy",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoliciesPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPolicy", x => new { x.PaymentId, x.PoliciesPolicyId });
                    table.ForeignKey(
                        name: "FK_PaymentPolicy_InsurancePolicies_PoliciesPolicyId",
                        column: x => x.PoliciesPolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentPolicy_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_Users_UserId",
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
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nominee = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomineeRelation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
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
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPolicy",
                columns: table => new
                {
                    CustomersCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoliciesPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPolicy", x => new { x.CustomersCustomerId, x.PoliciesPolicyId });
                    table.ForeignKey(
                        name: "FK_CustomerPolicy_Customers_CustomersCustomerId",
                        column: x => x.CustomersCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPolicy_InsurancePolicies_PoliciesPolicyId",
                        column: x => x.PoliciesPolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DocumentName = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3f1f49fc-4f3a-4afc-ac82-d698482833f2"), "Employee" },
                    { new Guid("7291d046-6694-4e06-978c-4d225f6704ba"), "Admin" },
                    { new Guid("762b45c3-0caf-455a-976b-c8e89b065837"), "Customer" },
                    { new Guid("d2e65388-63cf-4e0e-a5e1-8a895d4515df"), "Agent" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[,]
                {
                    { new Guid("077d1d42-56a0-4527-add0-bcd332cef7d4"), "Nagaland" },
                    { new Guid("1a5fd8f5-8c02-4b77-83d6-f7e9af50db08"), "Uttarakhand" },
                    { new Guid("23ca4ab2-2792-41e1-843c-0b9d349b500e"), "Punjab" },
                    { new Guid("2e9b9629-6dc1-4c93-ab8d-671d5b34867f"), "Sikkim" },
                    { new Guid("33694ddb-5250-485b-ad64-f43f8006f0fc"), "Odisha" },
                    { new Guid("42a9c32c-b6cd-40b2-b0aa-be58b5ed4447"), "Manipur" },
                    { new Guid("45d6dac4-356d-412c-9290-720ac52cec06"), "Chhattisgarh" },
                    { new Guid("4df48888-8202-446b-a3d9-d51174a53738"), "Karnataka" },
                    { new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8"), "Rajasthan" },
                    { new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4"), "Gujarat" },
                    { new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d"), "Kerala" },
                    { new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3"), "Bihar" },
                    { new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc"), "Assam" },
                    { new Guid("7bb77a43-3de8-4a1b-854e-7f23eece775b"), "Meghalaya" },
                    { new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a"), "Jharkhand" },
                    { new Guid("91787813-237f-498b-a47e-cc26066337c1"), "Tripura" },
                    { new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245"), "Goa" },
                    { new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8"), "Haryana" },
                    { new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad"), "Telangana" },
                    { new Guid("bd54957c-74c8-42e8-8625-4c851f4d5355"), "Mizoram" },
                    { new Guid("ca59345f-670d-4536-8b5e-a71b581faad9"), "Arunachal Pradesh" },
                    { new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b"), "Madhya Pradesh" },
                    { new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f"), "Maharashtra" },
                    { new Guid("e8e16639-e44f-4974-a965-ddfde1611388"), "Uttar Pradesh" },
                    { new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2"), "Himachal Pradesh" },
                    { new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1"), "West Bengal" },
                    { new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd"), "Tamil Nadu" },
                    { new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef"), "Andhra Pradesh" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "StateId" },
                values: new object[,]
                {
                    { new Guid("015f1e4e-6aeb-4223-8665-bd973c7d14a4"), "Tirunelveli", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("015f3947-7eb6-4e03-ae80-9c0e2500ba2b"), "Durg", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("0417815c-7ccf-4f2a-9e91-edb4c297e746"), "Moradabad", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("04bc4e06-1446-4aab-b93b-0cc2f8d6323d"), "Warangal", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("05bb0ba1-3c46-4996-b7c0-34c37a2d245c"), "Bicholim", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("06da1111-77ae-49b2-a515-ca5244fece56"), "Ramgarh", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("06e71347-9b53-4083-aaed-8462c98d1259"), "Coimbatore", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("079ac9d4-4b43-4eb2-9b9b-e141b645dcee"), "Shimoga", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("0881dcb9-7347-42fb-b756-6fca6f7c0aef"), "Mancherial", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("0a737c86-ce36-443f-b256-85d0a79b8b82"), "Udupi", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("0bf45726-f8e1-434b-9b95-bdf0b6fcb268"), "Ranchi", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("0c1775e5-75fd-43c9-9b13-234f376a24de"), "Nizamabad", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("0d7047f0-ff40-45be-80f6-b7dce988d494"), "Darjeeling", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("11df9f07-9270-4160-a99d-b044d1405b83"), "Dhanbad", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("12054b14-7203-4c81-946e-379fb0c27cff"), "Bangalore", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("13f9ace7-65b0-4de0-8049-90f42f2b3e85"), "Hisar", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("1585f1ef-5d22-42db-ad2c-0e393282a1be"), "Malda", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("164f51e5-9a18-4dab-bf4e-6584358723cc"), "Tezpur", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("166e6a3c-d011-4fbf-a168-456060d1d114"), "Dibrugarh", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("19301142-3791-40df-a621-e2e899aedaff"), "Meerut", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("19cef82d-1739-4df3-8ce5-7f4936476f0b"), "Hyderabad", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("1b1a62a0-05bf-4646-a2df-bb807b71f8eb"), "Bomdila", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("1e518281-57fc-4cee-84da-351077b4ee5d"), "Madurai", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("1f29941d-1968-41e4-b2fc-ddf146f80f19"), "Bikaner", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("1f99400b-2eea-48d1-b471-40ea7d1272f3"), "Darbhanga", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("209c0ca2-b526-4aca-8a29-e8dd79a04ed3"), "Davanagere", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("2134dd8c-3d41-4098-8ad1-09eb6a813027"), "Gandhinagar", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("22e0da54-56cc-474e-acd1-b7e45d7a9d17"), "Guntur", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("25b654be-8f1a-4a39-876e-ac2a5854833a"), "Bhavnagar", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("265d659f-19eb-466a-a4b0-18f1aa4ab5a4"), "Gurgaon", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("277ffcb2-ef9e-4446-b224-4757c65dbf58"), "Ujjain", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("2a880119-3207-444b-8ed3-f1329b50469c"), "Rewa", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("2bfad885-33e0-4337-9de0-405165f9d82b"), "Chamba", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("2bfafb6c-30e7-41eb-bb98-5bd96388dd8a"), "Bilaspur", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("2dba4fc3-ccb2-40bb-a1a4-76d5242294db"), "Bongaigaon", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("2e9650eb-d504-48f5-9e22-c9a0a09d8e29"), "Nashik", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("2f5e0ef6-787d-423d-812c-29294cabb7e0"), "Nellore", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("3047dd43-e676-47c6-bed6-ab1e949ddac0"), "Salem", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("30ba9dc1-ff1b-4f2d-a4cf-143c13d5f061"), "Pune", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("313defb6-f8da-4cf8-a60c-5598417bd618"), "Ahmedabad", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("3383a922-269f-47f2-8615-033c8e3ae4fa"), "Tinsukia", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("37e23f90-5ded-4ec2-9968-3c7324d30eae"), "Faridabad", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("384f8016-925d-4793-b7a7-510ae1259041"), "Palakkad", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("39c668b8-8c37-45b0-9311-67414cf7a61c"), "Panipat", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("3bdd4d4a-2cd4-43c3-97ca-95fe8c6f6b1b"), "Anand", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("3c61aa54-baf4-4c95-8aeb-44bceee315e6"), "Ghaziabad", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("41bfb161-8a5b-4355-abf9-631a767b9a9a"), "Sangli", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("42ac90dd-b3d1-4278-8e14-c4b4be3852cf"), "Roing", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("43020f0f-06ab-47ef-b5c1-dbc5be9b7301"), "Kolhapur", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("439f1caf-39e8-4fac-bae3-8d71f2f9d44e"), "Bellary", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("44e93a42-4b3e-45b7-bf49-4fe049c97e10"), "Bhilai", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("44f2425d-e26b-44a5-81bf-a8bfa1182ad4"), "Malappuram", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("44f8e24c-7d69-4e84-9ce9-17a63cd548a9"), "Solan", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("47ff585e-b584-4242-9fdb-794cd8cc6df6"), "Anini", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("48ff1b62-fa31-4007-9cd2-2e633a013767"), "Tezu", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("4930ddb2-2999-4040-a7a7-78d7bc32a538"), "Mangalore", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("4b976794-e5bc-448b-adb3-b3a6bac1d90c"), "Bokaro", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("4cea5502-64d8-4793-87a8-ca80b94da5ad"), "Deoghar", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("4f3c5c3d-d936-419d-976d-159f81815e26"), "Yamunanagar", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("4fa9b5fc-94f0-4a7f-a4ec-85a403ce074d"), "Jodhpur", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("4fbec692-4a76-4e89-825c-b4dd24dfdd35"), "Lucknow", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("50cb5358-df58-4a81-a3f1-c67cb5704733"), "Itanagar", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("51aedcee-992c-46bc-a1ff-56b46a3131ad"), "Goalpara", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("521dd075-64e1-4ea4-be0c-86f17b8a35df"), "Karimnagar", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("52d2e8db-1737-4f74-ab30-b66bf76476d9"), "Siliguri", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("541c0bc6-92d6-4782-90c6-bd1f6295b28a"), "Belgaum", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("55cbb58c-4a54-4a8f-8fc9-179a92519a03"), "Anantapur", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("561d3741-4076-475f-802b-fb70c0e8dd1e"), "Muzaffarpur", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("5677c3a5-6778-443d-9120-be42bd573c6b"), "Ambala", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("5725d33f-1db0-4d33-affb-5dd17a061d06"), "Kadapa", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("5764ccbb-64c6-46ae-9b46-8904c5ecfb29"), "Manali", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("57846f42-ee05-40e9-a6c9-bbeeca45c399"), "Thane", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("58a1f29b-62e7-47b7-8c44-79329c436533"), "Udaipur", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("58cc1ca0-7d16-4b03-b772-dc851a1a29ad"), "Pakur", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("58f352fb-e0a9-4c72-b956-47f3b2be77f1"), "Nagpur", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("5b2963a6-9e06-4bb6-8bf7-920c8ffba15f"), "Panaji", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("5b491cfa-35f0-4173-a104-4b3fed4e68e6"), "Margao", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("5b7b563f-0f8b-4a96-8c46-b039877a78bd"), "Indore", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("5c12b85b-95ad-4092-bac7-a0b9af05d3a8"), "Hubli", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("5c905c0a-f032-45e7-a4ea-6460fb610baf"), "Bhagalpur", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("5e83e319-5ce0-4ca0-9b3b-1a49cc121563"), "Giridih", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("5e8f2d40-b31f-40ba-9b9a-bdf33669cb92"), "Varanasi", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("5ebd3b8f-fc6d-44a5-ac3e-bf4dc082ea6c"), "Thoothukudi", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("60a772a5-bdeb-4ea7-b67f-851c20f5d45b"), "Bilaspur", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("63406c16-5107-4937-838c-0343164f7db8"), "Chittoor", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("63c0fedb-87c8-4b7c-8ea9-9ae00dc98bc9"), "Canacona", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("67413103-fcca-4ee0-9253-ca5350c02b61"), "Vellore", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("6928fa67-9ef0-4aaa-bce4-85496d69080c"), "Solapur", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("6d35f37a-dc9b-4324-9cd1-46ddea627eec"), "Tawang", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("6d429c9b-f230-4c63-8d28-c178ee7ad359"), "Sasaram", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("6e348b50-c3f7-498a-b9b1-2b90b21a21b3"), "Jagdalpur", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("7049f229-1e8b-4733-a8df-ef396bd5ae4c"), "Kullu", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("715005a1-50ab-4eb6-a697-61a8be205d51"), "Jorhat", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("72e2dc0a-49ad-4fa0-98f6-9c661492433d"), "Hamirpur", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("73653f61-05f4-4352-9e53-1213c6cd6c09"), "Rajnandgaon", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("76b7f01d-9705-4994-ba2a-d0ef87660b59"), "Erode", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("77edad38-370e-46f5-b725-2162ee4afdcc"), "Nagaon", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("78d78612-1208-4e09-a008-07c4ee6fb49c"), "Changlang", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("7c6eb7c5-5539-482a-9fb5-1814921693dc"), "Chennai", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("7e33ac8e-f4e3-4a59-9d56-7e4656807e91"), "Mumbai", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("7f9ee2c3-5011-42de-9208-f5bb0cc1ccd7"), "Dindigul", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("7fe362e6-7e87-4a06-87ae-536975cc1612"), "Sirsa", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("7ff436bc-c94a-4e67-8136-b6b3388349a6"), "Jamshedpur", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("81190233-562b-4973-a748-0d9f476debf0"), "Korba", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("82faf966-84fd-4f7c-8923-758f389d4303"), "Pasighat", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("835cddfb-0828-47d7-ad1a-32e321e67fde"), "Bardhaman", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("84c06538-7f67-4e1b-a304-77de2860ecc1"), "Alwar", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("88441e25-f3d7-4c41-99b5-24cd8bc37809"), "Silchar", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("88bcbb43-9457-4998-a63d-4645fc2b35d9"), "Bharatpur", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("8a17e321-c64e-4301-9fbd-68c4199ce8ea"), "Raipur", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("8a64565d-3b76-42e4-9bce-c1b372bd2b6d"), "Rajkot", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("8a6d7ddc-82a1-4c86-9d04-7af231b32b70"), "Vijayawada", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("8b914b51-888b-4a3c-b382-33b2e783d5b7"), "Calangute", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("8bc0b009-842a-45f1-98ea-fd490da12be1"), "Ajmer", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("8ea38249-311b-4cf9-aae5-cca601f15453"), "Rajahmundry", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("8f328036-a828-4d1e-b7a2-73fd2c492359"), "Kozhikode", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("9081cf7e-d3be-4128-9e6e-e021b264dbec"), "Ara", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("937ad25b-86e2-43a2-9222-3f9ba46e0252"), "Purnia", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("95e1f1db-49f7-45de-a393-c80b11e0927b"), "Kakinada", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("95fa4b5b-e915-4dbd-bf1f-dbc521c07646"), "Durgapur", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("9697a9e5-ff6d-457a-b48e-accfba644e43"), "Mysore", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("9698d6f7-ccba-4ad1-801a-657ec7fdfe2c"), "Jabalpur", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("974c7b59-66b4-4885-93aa-3d2bbf2cef29"), "Satna", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("97a11bc5-0cc7-4a8e-b810-b7a329780210"), "Along", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("9a95dcc4-9b04-4bd0-b0e2-67fdca80322e"), "Ponda", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("9cd71183-2df7-4fb9-a1ea-a5d45d2410b4"), "Mahbubnagar", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("9e72b498-b971-46a4-a706-03df9cc1dabf"), "Kochi", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("9eb5e508-dbc2-49e4-8c8f-da085b39937f"), "Kollam", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("a43243f7-9c2b-4887-8d39-35db6ba6a04a"), "Thrissur", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("a624f0d3-2d73-49db-8d4a-1c411d7260d1"), "Godda", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") },
                    { new Guid("a642360e-6955-49e2-be7d-890127bc59cf"), "Hajipur", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("a75292ca-947a-4d19-abdb-4b446591ef6e"), "Haldia", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("a9f051b1-0182-43db-8513-74db9f549bf1"), "Begusarai", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("abda0494-1f1b-4e08-9baf-e4bf37ba96c1"), "Siddipet", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("ada8b6d0-c702-4787-b902-ae4b4efd0f69"), "Gwalior", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("aef4986f-2669-4352-b0b4-1e6e58c20b53"), "Mandi", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("afeb9590-d7bf-49a2-b9c2-b11c69490198"), "Vadodara", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("b153c17d-560a-4939-9936-87b0c2b9a849"), "Jaipur", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("b5497500-dcca-42ce-b11f-e1ab88f41eb3"), "Porvorim", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("b5e40a31-e9fc-47f6-b4db-ba997a21caeb"), "Allahabad", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("b5f9d4ae-cdfe-4beb-b563-64113e489bf3"), "Dona Paula", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("b657ac7c-dabf-409f-ad8e-d93be903a8c9"), "Vasco da Gama", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("b66c7ff0-0e8f-405c-916c-21fb144a6531"), "Howrah", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("b67826bf-6916-4afd-b1d4-08727d195858"), "Kota", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("b7547999-70d8-443c-9f05-51b5b995550b"), "Karnal", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("b815eb60-515c-4828-948a-61582e366e2a"), "Bareilly", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("b8cc061a-5200-41f4-a094-a31c37dad582"), "Raigarh", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("bb600230-24d7-4b73-b75b-9adca6bf967d"), "Tirupati", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("bbc93f2b-9b1e-4c8b-9b4f-163ebf49521e"), "Mehsana", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("c0038929-3a16-45f8-9be1-67d03def1220"), "Jalpaiguri", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("c277b464-b4b3-4e42-9833-55973d143167"), "Aurangabad", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("c86eb902-4ff0-4b1e-8bc0-210f0b2fb6e6"), "Visakhapatnam", new Guid("ff790283-a7dd-4db6-9d17-5b287c4dc2ef") },
                    { new Guid("cc21a8b3-2dac-425a-b4c6-cd388b7d7cb5"), "Sagar", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("cc32820d-d629-423b-8e11-c6e73ce578b8"), "Bhopal", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("ccc34628-93a1-4e76-bf64-63c91a2eac61"), "Ratlam", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("cce49c60-55e9-4555-8907-2f602d2bc2e7"), "Bijapur", new Guid("4df48888-8202-446b-a3d9-d51174a53738") },
                    { new Guid("cdd62457-eb64-4842-a654-79105f58f83b"), "Adilabad", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("cdf7a5ed-d972-42ea-aca5-5ed1790294d4"), "Surat", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("cec91794-e9af-4a1a-820b-c7e6a77401f6"), "Gaya", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("d05e01ef-7c45-49cd-89cd-75327fb61f3b"), "Kannur", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("d0a11de5-c071-4aae-8262-4f095dfb1c55"), "Nahan", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("d0b6773d-c8f9-46b7-858f-a3a3023517d7"), "Kolkata", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("d3b9c5d4-9bd2-45d5-bbf3-48650ffca62d"), "Mahasamund", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("d3bd4dbf-b4c9-4302-832f-5f8c9c80ccc2"), "Jaisalmer", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("d3ed691f-b26c-4d1d-889e-4c920ed54331"), "Guwahati", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("d468cbf5-be9b-45a0-b26c-902997994781"), "Sikar", new Guid("4f5b2aa8-9cc6-4046-bc15-c808010b52d8") },
                    { new Guid("d61996a1-a14c-467e-a789-bcdd8d07dace"), "Khammam", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("d9622704-57b0-40a6-a75d-1cee06f28e98"), "Barpeta", new Guid("76acffda-92b2-4fdc-b8cc-3eb78281e1fc") },
                    { new Guid("db0cf04b-bfed-4ae8-8cea-de55997dc982"), "Bhiwani", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("ddadc997-4328-4636-bf10-f6dc407f8951"), "Ramagundam", new Guid("b6886c43-1545-4ac8-b441-68bfe6e619ad") },
                    { new Guid("e183cbd4-8fb7-4458-b7dd-a5948263e2e0"), "Kanpur", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("e5eac272-b008-4feb-a7de-d44f5b3ba373"), "Rohtak", new Guid("ad61f74f-efa5-41f5-bb98-5ca0d89778c8") },
                    { new Guid("e6d056f5-6003-4c6a-a703-86537adc91a1"), "Ambikapur", new Guid("45d6dac4-356d-412c-9290-720ac52cec06") },
                    { new Guid("e7d78951-4de6-4379-97c0-09e608b8bf51"), "Navsari", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("e815cca0-ac2a-4db3-9f69-c8e7305a430c"), "Thiruvananthapuram", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("e99b73ad-cdd5-491e-ba8f-ba522603fe90"), "Idukki", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("ebb79057-3776-41ef-8ba1-8e87d7725d31"), "Amravati", new Guid("e12f6cad-eed1-4d26-a17c-737afe19c94f") },
                    { new Guid("ec6ff146-cdfc-4da2-9d81-82b9a8738265"), "Aligarh", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("ee262744-595c-4bde-a21b-61a4e3a47816"), "Chhindwara", new Guid("d8bfef23-508d-4c5b-acc1-b4388b39bc7b") },
                    { new Guid("f172ba6c-8af4-4954-af70-9de0f2df6537"), "Tiruchirappalli", new Guid("fab9319b-19ce-4029-a86e-7517eecba3dd") },
                    { new Guid("f1f77d92-5f11-444a-a917-e1a31272804d"), "Shimla", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("f2f455e7-a950-4a68-bd5d-2f0031e8d8a2"), "Ziro", new Guid("ca59345f-670d-4536-8b5e-a71b581faad9") },
                    { new Guid("f3194163-b618-4d37-8323-e9cacde6643b"), "Dharamshala", new Guid("ef4f21a3-110b-4826-8e48-4cebfea255b2") },
                    { new Guid("f5b30fa8-d862-43ba-9b36-40ed9c779871"), "Patna", new Guid("700b8d6b-ace2-4d89-8eb6-35ae1009a7f3") },
                    { new Guid("f5dd889b-2d72-4a57-bddb-d76dc55fd4f8"), "Jamnagar", new Guid("60e8c0fe-f916-4917-94ad-5a486d5703a4") },
                    { new Guid("f7bccc25-0d45-487b-9362-b6cf9e0152e8"), "Asansol", new Guid("f438e5e8-1802-48a9-9d01-d4dd249eb5d1") },
                    { new Guid("fb4f6c9b-0149-4d02-a9d5-9103d983b84e"), "Agra", new Guid("e8e16639-e44f-4974-a965-ddfde1611388") },
                    { new Guid("fcc583eb-6507-4638-a79e-e1e0a8914ca3"), "Mapusa", new Guid("aa9dfba4-af52-4b09-b3ee-bcd036bdc245") },
                    { new Guid("ff0de4f9-ad31-42eb-9e2e-0c654a0d910c"), "Alappuzha", new Guid("6cd8dcf5-c85c-4fd7-bba8-fc28fcc4852d") },
                    { new Guid("ffb5275b-a6e4-4d1a-b668-69c22c724785"), "Hazaribagh", new Guid("8c9c47ad-b2ed-46fc-abe7-16948ab15a0a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CustomerId",
                table: "Claims",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPolicy_PoliciesPolicyId",
                table: "CustomerPolicy",
                column: "PoliciesPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AgentId",
                table: "Customers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CustomerId",
                table: "Documents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePlanInsuranceScheme_SchemesSchemeId",
                table: "InsurancePlanInsuranceScheme",
                column: "SchemesSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceSchemeId",
                table: "InsurancePolicies",
                column: "InsuranceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPolicy_PoliciesPolicyId",
                table: "PaymentPolicy",
                column: "PoliciesPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeDetails_InsuranceSchemeId",
                table: "SchemeDetails",
                column: "InsuranceSchemeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "CustomerPolicy");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "InsurancePlanInsuranceScheme");

            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.DropTable(
                name: "SchemeDetails");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InsurancePlans");

            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "InsuranceSchemes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
