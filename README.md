# WorkplaceOps

WorkplaceOps is an early-stage full-stack workplace operations platform focused on helping Quebec businesses turn operational and regulatory information into structured, actionable work.

The long-term goal is to model the characteristics of a business, determine which requirements and operational rules apply, generate relevant workflows, and maintain a clear history of why actions were created and how they were completed.

> **Status:** Active early-stage development. The core architecture and first end-to-end business-management features are implemented, but the platform is not yet production-ready.

---

## Current Implementation

The project already includes several working vertical slices across the backend, database, and React client.

### Business Management

Businesses can currently be:

- Created through the API and React interface
- Listed
- Retrieved by ID
- Persisted in SQL Server

A business currently contains information such as:

- Legal name
- Operating name
- Quebec Enterprise Number
- Employee count
- Creation timestamp

### Business Operational Profiles

Each business can have an operational profile describing characteristics that future regulatory rules can evaluate.

Current profile data includes:

- Industry
- Number of locations
- Whether the business has remote employees
- Whether the business has unionized employees

The React client can create, retrieve, and display these profiles.

### Regulatory Rules

The initial regulatory-rule domain and persistence layer are implemented.

A rule currently contains:

- Title
- Description
- Version
- Active status
- Creation timestamp

The API currently supports:

- Creating rules
- Listing rules
- Retrieving individual rules

Rule persistence is implemented with Entity Framework Core and SQL Server.

The actual rule-evaluation engine that determines which rules apply to a business is a future development phase.

---

## Architecture

WorkplaceOps uses a layered full-stack architecture:

```text
React Client
     |
     | HTTP / JSON
     v
ASP.NET Core Web API
     |
     v
Application Layer
     |
     v
Domain Layer
     ^
     |
Infrastructure Layer
     |
     v
Entity Framework Core
     |
     v
Microsoft SQL Server
```

### Solution Structure

```text
WorkplaceOps
|
|-- WorkplaceOps.Api
|-- WorkplaceOps.Application
|-- WorkplaceOps.Client
|-- WorkplaceOps.Domain
|-- WorkplaceOps.Infrastructure
|-- WorkplaceOps.Tests
`-- WorkplaceOps.slnx
```

### `WorkplaceOps.Domain`

Contains the core business entities and domain concepts.

Current domain concepts include:

```text
Business
BusinessOperationalProfile
Rule
```

### `WorkplaceOps.Application`

Contains application services, use cases, request models, and repository abstractions.

### `WorkplaceOps.Infrastructure`

Contains persistence implementations and Entity Framework Core integration.

Current infrastructure includes:

- SQL Server persistence
- Entity Framework Core
- Repository implementations
- Database migrations

### `WorkplaceOps.Api`

ASP.NET Core Web API exposing the application through REST endpoints.

Current controllers include:

- Businesses
- Business operational profiles
- Regulatory rules

### `WorkplaceOps.Client`

React and JavaScript frontend responsible for the current user interface and communication with the backend API.

### `WorkplaceOps.Tests`

Reserved for automated domain and application testing as the project develops.

Meaningful automated test coverage has not yet been implemented.

---

## Technology Stack

### Backend

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- REST APIs
- Swagger / OpenAPI

### Frontend

- React
- JavaScript
- Vite
- HTML5
- CSS3
- ESLint

### Database

- Microsoft SQL Server
- Entity Framework Core
- LINQ
- SQL
- EF Core migrations

### Development Tools

- Git
- GitHub
- Swagger
- Postman
- Visual Studio
- npm

---

## Database Migrations

The project currently includes migrations for:

- Initial business persistence
- Business operational profiles
- Regulatory rules

Entity Framework Core maintains the current relational model through the Infrastructure project.

---

## Current Frontend

The React client currently supports:

- Creating businesses
- Listing businesses
- Selecting and viewing business details
- Creating business operational profiles
- Loading and displaying existing operational profiles
- Form validation and loading/error feedback

The frontend currently communicates with the API through the local development environment.

---

## Local Development

### Prerequisites

You will need:

- .NET 10 SDK
- Node.js and npm
- Microsoft SQL Server

The current development configuration uses a local SQL Server connection with Windows trusted authentication.

### Run the Backend

From the repository root:

```bash
dotnet run --project WorkplaceOps.Api
```

The local HTTP API is configured at:

```text
http://localhost:5158
```

Swagger is available when the API is running in the Development environment.

### Run the Frontend

From the repository root:

```bash
cd WorkplaceOps.Client
npm install
npm run dev
```

The Vite development server is configured at:

```text
http://localhost:64178
```

### Build the Solution

```bash
dotnet build WorkplaceOps.slnx
```

### Build the Frontend

```bash
cd WorkplaceOps.Client
npm run build
```

### Lint the Frontend

```bash
npm run lint
```

---

## Product Direction

The broader WorkplaceOps concept is designed around a progression such as:

```text
Business Profile
      |
      v
Business Events
      |
      v
Regulatory / Operational Rules
      |
      v
Rule Evaluation
      |
      v
Operational Impact
      |
      v
Workflows and Actions
      |
      v
Evidence and History
```

The current project is building the foundation required for that model.

---

## Planned Development

Major future areas include:

- Regulatory rule evaluation
- Rule applicability based on business characteristics
- Business events
- Operational impact determination
- Workflow and action generation
- Employee and manager tasks
- Authentication and authorization
- Multi-tenant architecture
- Notifications
- Evidence and audit history
- Background processing
- Expanded validation
- Automated test coverage
- Production configuration
- CI/CD
- Deployment

These features are part of the roadmap and should not be considered implemented yet.

---

## Development Purpose

WorkplaceOps is being developed both as a potential product concept and as a substantial full-stack software engineering project.

The project is being used to deepen practical experience with:

- Domain modeling
- Layered application architecture
- C# and ASP.NET Core
- Entity Framework Core
- SQL Server
- REST API design
- React
- Relational database design
- Rule-based systems
- Testing
- Application security
- Production architecture

---

## Disclaimer

WorkplaceOps is intended to support workplace operations and administrative organization.

It is not intended to provide legal, accounting, tax, or other professional advice.

---

## License

No open-source license is currently provided for this repository.

The source code is publicly available for portfolio and review purposes. No permission for redistribution, modification, or commercial reuse is granted unless explicitly stated otherwise.
