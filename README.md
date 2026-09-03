# WorkplaceOps

WorkplaceOps is a Quebec-first workplace operations platform designed to help
small and medium-sized businesses turn regulatory requirements, business
changes, and recurring administrative responsibilities into clear,
actionable workflows.

The long-term goal is to reduce administrative friction for business owners,
managers, and employees by determining what actions are relevant, assigning
them to the appropriate people, tracking completion, and maintaining an
auditable history of the work performed.

> **Status:** Early development / architecture phase.

---

## Product Vision

Small and medium-sized businesses often rely on a combination of spreadsheets,
calendars, emails, government websites, documents, and manual reminders to
manage workplace responsibilities.

WorkplaceOps aims to provide a centralized operational layer that can
eventually:

- Model important information about a business and its workplaces
- Detect relevant business events and operational changes
- Evaluate applicable rules and requirements
- Generate actionable workflows
- Assign work to owners, managers, and employees
- Track recurring responsibilities and deadlines
- Monitor important business thresholds
- Maintain evidence and historical records
- Explain why specific actions were generated
- Help teams understand what changed and what they need to do next

The objective is not simply to display regulatory information.

WorkplaceOps is intended to transform relevant business and regulatory
information into work that people can actually complete.

---

## Core Product Direction

The platform is being designed around several interconnected concepts:

```text
Business Digital Twin
        |
        v
Business Events
        |
        v
Rules Engine
        |
        v
Operational Impact
        |
        v
Workflows
        |
        v
Employee / Manager Actions
        |
        v
Evidence & History
```

Examples of business events may eventually include:

- Employee hired
- Employee changes role
- Employee begins a new activity
- Employee certification approaches expiry
- Workforce size crosses an important threshold
- Payroll approaches a threshold
- A new establishment opens
- Company operations change
- A policy changes
- A relevant external requirement changes

The rules engine will evaluate these events against the characteristics of a
business and determine which operational actions should be generated.

---

## Architecture

WorkplaceOps uses a modular full-stack architecture with a separately
developed frontend and backend.

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
`-- WorkplaceOps.Tests
```

### Layer Responsibilities

#### `WorkplaceOps.Domain`

Contains the core business concepts and business rules.

The Domain layer is intended to remain independent of persistence,
presentation, and infrastructure technologies.

#### `WorkplaceOps.Application`

Contains application use cases and coordinates operations involving the
domain.

#### `WorkplaceOps.Infrastructure`

Contains infrastructure concerns such as:

- Entity Framework Core
- SQL Server persistence
- External service implementations
- File storage
- Notifications
- Other infrastructure integrations

#### `WorkplaceOps.Api`

ASP.NET Core Web API responsible for exposing the backend through HTTP
endpoints.

#### `WorkplaceOps.Client`

React and JavaScript frontend responsible for the user interface and
interaction with the backend API.

#### `WorkplaceOps.Tests`

Automated tests for domain and application behavior.

---

## Backend

The backend is built with the Microsoft .NET ecosystem.

### Current Backend Technologies

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- REST APIs
- Swagger / OpenAPI
- xUnit

As the application evolves, the backend is expected to include concepts such
as:

- Dependency injection
- Authentication
- Authorization
- Validation
- Background processing
- Rule evaluation
- Domain events
- Notifications
- Audit logging
- Multi-tenancy
- Application security

---

## Frontend

The frontend is built as an independent React application.

### Current Frontend Technologies

- React
- JavaScript
- Vite
- HTML5
- CSS3
- ESLint

Bootstrap may be introduced where reusable components and responsive
utilities provide value, while custom CSS will be used to establish the
product's own visual identity.

The frontend communicates with the ASP.NET Core backend through HTTP and JSON.

---

## Database

WorkplaceOps uses Microsoft SQL Server as its relational database platform.

### Current Database Technologies

- Microsoft SQL Server 2022
- Entity Framework Core
- LINQ
- SQL

The project will intentionally combine Entity Framework Core development with
direct SQL practice to maintain a strong understanding of the underlying
relational database model and generated queries.

Database development is expected to include:

- Relational modeling
- Primary and foreign keys
- Constraints
- Joins
- Indexes
- Transactions
- Migrations
- Query optimization
- Concurrency
- Data integrity

---

## API Development and Testing

The API development workflow uses:

- Swagger
- OpenAPI
- Postman

Postman will be used for manual API testing, including:

- Request and response validation
- Authentication flows
- Authorization scenarios
- Error handling
- Endpoint collections
- Integration testing during development

---

## Planned Domain Concepts

The domain model is expected to evolve around concepts such as:

```text
Business
Establishment
Employee
BusinessEvent
Rule
Obligation
Workflow
Action
EvidenceRecord
Notification
```

These concepts will evolve as product discovery and implementation continue.

---

## Development Goals

WorkplaceOps is being developed both as a potential commercial SaaS product
and as a serious full-stack software engineering project.

The project is intended to provide deeper practical experience with:

- JavaScript
- React
- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- SQL
- REST API design
- Relational database design
- Authentication and authorization
- Multi-tenant SaaS architecture
- Rule engines
- Background processing
- Testing
- Application security
- Git and GitHub
- CI/CD
- Production deployment

---

## Current Development Status

The initial solution architecture has been established.

Current foundation:

- ASP.NET Core backend solution
- Domain layer
- Application layer
- Infrastructure layer
- React / JavaScript frontend
- xUnit test project
- Successful backend build
- Successful frontend production build

The next development phase will focus on:

1. Designing the initial business domain model
2. Configuring Entity Framework Core
3. Connecting Microsoft SQL Server 2022
4. Creating the first database migration
5. Implementing the first API vertical slice
6. Testing the API with Postman
7. Connecting the React frontend to the backend

---

## Disclaimer

WorkplaceOps is intended to support workplace operations and administrative
organization.

It is not intended to provide legal, accounting, tax, or other professional
advice.

---

## License

This project is currently private and proprietary.

No license for redistribution, modification, or commercial use is granted at
this stage.
