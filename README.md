# WorkplaceOps

WorkplaceOps is an early-stage, full-stack workplace operations platform focused on helping businesses in Québec organize business information, evaluate structured operational and regulatory rules, and ultimately turn applicable requirements into actionable workflows.

The long-term product vision is a traceable flow from **business profile → business event → rule evaluation → obligations and actions → tasks → evidence and audit history**.

> **Project status (November 2026):** Active development; not production-ready. Business management, operational profiles, persisted rules and rule conditions, and the first deterministic rule-evaluation engine are implemented. Rule evaluation is currently available in application code and automated tests, **not yet through a public API endpoint or the React UI**.

---

## Implemented Features

### Business Management

- Create and list businesses through the API and React client.
- Retrieve individual business details.
- Persist data in Microsoft SQL Server using Entity Framework Core.

Each business currently stores a legal name, optional operating name, optional Québec Enterprise Number (NEQ), employee count, and creation timestamp.

### Business Operational Profiles

A business can have an associated operational profile containing:

- **Industry** (text)
- **LocationCount** (integer)
- **HasRemoteEmployees** (Boolean)
- **HasUnionizedEmployees** (Boolean)

Profiles can be created, retrieved, and displayed through the API and React client.

### Regulatory Rules and Conditions

Rules are stored in SQL Server and include a title, optional description, version, active status, and creation timestamp.

Each rule can contain multiple persisted `RuleCondition` records. A condition specifies:

- **Field:** the business or profile property to evaluate
- **Operator:** the comparison to perform
- **ComparisonValue:** the expected value, stored as text and parsed as appropriate

The API supports creating rules, listing rules, retrieving a rule with its conditions, and adding conditions to an existing rule. Rule conditions are loaded with their parent rules through EF Core.

### Deterministic Rule Evaluation — OWPS-13 (In Progress)

`RuleEvaluator` in `WorkplaceOps.Application` evaluates an in-memory `Rule` against a `Business` and an optional `BusinessOperationalProfile`.

**Supported fields**

| Field | Type | Supported operators |
| --- | --- | --- |
| `EmployeeCount` | Integer | `Equal`, `NotEqual`, `GreaterThan`, `GreaterThanOrEqual`, `LessThan`, `LessThanOrEqual` |
| `LocationCount` | Integer | All six integer operators |
| `HasRemoteEmployees` | Boolean | `Equal`, `NotEqual` |
| `HasUnionizedEmployees` | Boolean | `Equal`, `NotEqual` |
| `Industry` | String | `Equal`, `NotEqual` (case-insensitive) |

**Current evaluation behavior**

- An inactive rule or a rule with no conditions returns `false`.
- All conditions are combined with **AND** logic; every condition must pass.
- Missing operational profiles, invalid comparison values, and unsupported operators cause the affected condition to evaluate to `false`.
- Industry comparisons use `StringComparison.OrdinalIgnoreCase`.
- Evaluation uses deterministic C# logic; **no AI is involved**.
- The current return type is `bool` (`true` for a matching rule, `false` otherwise). Explicit `Applicable` / `NotApplicable` results and API integration are next steps.

For example, a rule requiring `EmployeeCount >= 20`, `Industry == "Retail"`, and `HasRemoteEmployees == true` matches only when all three conditions are satisfied.

> Rule definitions and examples in this project are development fixtures; they should not be interpreted as verified legal requirements for Québec businesses.

---

## Automated Tests

`WorkplaceOps.Tests` uses **xUnit** to test the rule evaluator independently from the API, frontend, and database.

The current test suite includes **38 passing test cases** (last verified during local development in November 2026), covering:

- Employee count and location count comparisons
- Remote and unionized employee Boolean comparisons
- Case-insensitive industry matching
- Multiple conditions with AND semantics
- Inactive rules and rules without conditions
- Missing operational profiles
- Invalid comparison values and unsupported operators
- All six integer comparison operators, including boundary cases

Run the tests from the repository root:

```powershell
dotnet test .\WorkplaceOps.Tests\WorkplaceOps.Tests.csproj
```

The test count is a development snapshot, not a claim of complete application or integration-test coverage.

---

## Architecture

WorkplaceOps is organized into separate projects with clear responsibilities:

```text
WorkplaceOps.Client (React + Vite)
              |
          HTTP / JSON
              v
WorkplaceOps.Api (ASP.NET Core)
              |
              v
WorkplaceOps.Application (services, use cases, rule evaluation)
         |                             |
         v                             v
WorkplaceOps.Domain              Repository interfaces
(entities and enums)                    ^
                                       |
                            WorkplaceOps.Infrastructure
                            (EF Core repositories, migrations)
                                       |
                                       v
                              Microsoft SQL Server

WorkplaceOps.Tests -> Domain + Application
```

### Solution Structure

```text
WorkplaceOps/
├── WorkplaceOps.Api/
├── WorkplaceOps.Application/
├── WorkplaceOps.Client/
├── WorkplaceOps.Domain/
├── WorkplaceOps.Infrastructure/
├── WorkplaceOps.Tests/
└── WorkplaceOps.slnx
```

| Project | Responsibility |
| --- | --- |
| `WorkplaceOps.Domain` | Business, operational-profile, rule, and rule-condition models and enums |
| `WorkplaceOps.Application` | Application services, request models, repository contracts, and `RuleEvaluator` |
| `WorkplaceOps.Infrastructure` | EF Core `AppDbContext`, SQL Server repositories, and database migrations |
| `WorkplaceOps.Api` | REST controllers, dependency injection, Swagger/OpenAPI configuration |
| `WorkplaceOps.Client` | React interface for businesses and operational profiles |
| `WorkplaceOps.Tests` | xUnit tests for application and domain behavior |

### Technology Stack

- **Backend:** C#, .NET 10, ASP.NET Core Web API, Entity Framework Core 10, LINQ
- **Frontend:** React 19, JavaScript, Vite 8, HTML, CSS, ESLint
- **Database:** Microsoft SQL Server, SQL, EF Core migrations
- **Testing:** xUnit, Microsoft.NET.Test.Sdk, coverlet collector
- **Development:** Visual Studio, Git/GitHub, PowerShell, Swagger/OpenAPI, npm

---

## Current API Surface

These endpoints are implemented:

| Method | Endpoint | Purpose |
| --- | --- | --- |
| `POST` | `/api/Businesses` | Create a business |
| `GET` | `/api/Businesses` | List businesses |
| `GET` | `/api/Businesses/{id}` | Retrieve a business |
| `POST` | `/api/BusinessOperationalProfiles` | Create a profile |
| `GET` | `/api/BusinessOperationalProfiles/business/{businessId}` | Retrieve a profile by business |
| `POST` | `/api/Rules` | Create a rule |
| `GET` | `/api/Rules` | List rules |
| `GET` | `/api/Rules/{id}` | Retrieve a rule, including conditions |
| `POST` | `/api/Rules/{ruleId}/conditions` | Add a rule condition |

**Not implemented yet:** An API endpoint that invokes `RuleEvaluator` for a stored rule and business; an `Applicable` / `NotApplicable` response model; and a corresponding React rule-evaluation interface.

---

## Local Development

### Prerequisites

- .NET 10 SDK
- Node.js and npm
- Microsoft SQL Server
- A configured local SQL Server connection string for `WorkplaceOps.Api`

The current development setup uses local SQL Server with Windows authentication. Ensure SQL Server is running and the configured connection is accessible.

### Run the Backend

From the repository root:

```powershell
dotnet run --project .\WorkplaceOps.Api\WorkplaceOps.Api.csproj
```

Default local HTTP address: **http://localhost:5158**

Swagger UI (Development environment): **http://localhost:5158/swagger**

### Run the Frontend

Open a second terminal at the repository root:

```powershell
cd .\WorkplaceOps.Client
npm install
npm run dev
```

Configured Vite address: **http://localhost:64178**

The API's current development CORS policy allows requests from `http://localhost:64178`. If the frontend runs on a different port, the API CORS configuration must be updated accordingly.

### Build and Test

From the repository root:

```powershell
dotnet build .\WorkplaceOps.slnx
dotnet test .\WorkplaceOps.Tests\WorkplaceOps.Tests.csproj
```

From `WorkplaceOps.Client`:

```powershell
npm run build
npm run lint
```

### Database

The project uses EF Core migrations for businesses, operational profiles, rules, and rule conditions. The database is not provisioned automatically by starting the React client. Apply the relevant migrations to your local SQL Server database before using persistence-backed endpoints.

---

## Development Roadmap

### Rules Engine

- **OWPS-10 — Define Rule model:** Complete
- **OWPS-11 — Persist Rules:** Complete
- **OWPS-12 — Define Rule conditions:** Complete
- **OWPS-13 — Evaluate Rule against Business profile:** Core engine and unit tests implemented; explicit applicability result and API integration remain
- **OWPS-14 — Explain Rule evaluation:** Planned

### Broader Platform

Planned capabilities include:

- Business events and operational-impact determination
- Obligations, actions, workflows, and task assignment
- Evidence collection and audit trails
- Threshold monitoring
- Authentication, authorization, and tenant isolation
- Notifications and background processing
- Additional validation and integration tests
- Security hardening, CI/CD, production configuration, and deployment

These features remain on the roadmap and are **not** represented as currently implemented.

---

## Development Purpose

WorkplaceOps is being developed as both a potential product and a substantial software engineering project. It provides hands-on experience in domain modeling, layered architecture, REST API design, EF Core, SQL Server, React, deterministic rule engines, and automated testing.

## Disclaimer

WorkplaceOps is intended to support workplace operations and administrative organization. It is not a source of legal, accounting, tax, or other professional advice, and it is not currently production-ready.

## License

No open-source license is currently provided for this repository.

The source code is publicly available for portfolio and review purposes. No permission for redistribution, modification, or commercial reuse is granted unless explicitly stated otherwise.
