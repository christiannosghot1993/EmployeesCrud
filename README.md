# Build an Employee Management System with Authentication, Authorization, Clean Architecture, TDD, and Angular 22

# Context

The organization requires a demonstration application that showcases modern .NET development practices, including Clean Architecture, RESTful APIs, Authentication and Authorization, database integration, frontend development, and Test-Driven Development (TDD).

Although the assessment refers to generic data records, this solution will implement those records as **Employees**, providing a realistic Employee Management System where authenticated users can perform CRUD operations on employee information exclusively through the frontend application. The backend controllers are protected with authentication and authorization.

The solution must demonstrate a clear separation of concerns across all layers of the application while following Clean Architecture principles, SOLID principles, and modern development best practices.

---

# Domain Clarification

For the purpose of this application, the generic term **Record** referenced throughout the technical requirements will be implemented as an **Employee** entity.

All CRUD operations mentioned in the assignment (Create, Read, Update, Delete) will therefore apply to employee records, allowing authenticated users to manage employee information within the system.

---

# User Story

**As an authenticated user,**

**I want** to create, view, update, and delete employee records,

**So that** I can efficiently manage employee information within a centralized and secure system.

---

# Functional Requirements

## Employee Management

The application shall allow authenticated users to:

- Create employees.
- Retrieve all employees.
- Retrieve employee details.
- Update employee information.
- Delete employees.

### Employee Entity

- Unique Identifier (Id)
- First Name
- Last Name
- Email
- Position
- Hire Date

## User Management

The application shall allow users to:

- Register a new account.
- Log in using valid credentials.
- Receive a JWT access token after successful authentication.
- Access protected resources only when authenticated.
- Have their information securely stored in the database.

### User Entity

- Unique Identifier (Id)
- Username
- Email
- PasswordHash

---

# Business Rules

## Employee Rules

1. First Name is required.
2. Last Name is required.
3. Email is required.
4. Email must be unique.
5. Position is required.
6. Hire Date cannot be a future date.
7. Every employee must have a unique identifier.
8. Deleted employees shall no longer be accessible through the API.
9. Employee information must pass validation before being persisted.
10. Only authenticated users may perform Employee operations.

## User Rules

1. Username must be unique.
2. Email must be unique.
3. Passwords must never be stored in plain text.
4. Passwords must be securely hashed before being stored.
5. JWT Authentication shall be used for securing the application.
6. Authorization shall be enforced using ASP.NET Core Authorization.
7. Only authenticated users may access Employee endpoints.
8. Invalid credentials shall return an Unauthorized response.
9. Requests made without a valid JWT token shall return HTTP 401 Unauthorized.
10. User information must be persisted in the database during registration.

---

# Technical Requirements

## Architecture

### Domain Layer
- Entities
- Domain Rules
- Domain Interfaces
- Core Business Logic

### Application Layer
- Use Cases
- CQRS Commands and Queries
- DTOs
- Validation Logic
- Application Services
- Business Rules

### Infrastructure Layer
- Entity Framework Core
- SQL Server Integration
- Code First Approach
- Entity Configurations
- Database Migrations
- Repository Implementations
- Authentication Services
- JWT Token Generation
- Persistence Layer

### Database Strategy

The database schema shall be generated using Entity Framework Core Code First.

All database objects, including tables, constraints, indexes, and relationships, shall be managed through Entity Framework Core Migrations.

### Presentation Layer
- .NET Web API
- Controllers
- Middleware
- Dependency Injection Configuration
- Authentication and Authorization Configuration
- API Documentation

---

# Database Requirements

The solution shall use SQL Server as the primary database.

The database shall be generated and maintained using Entity Framework Core Code First Migrations.

## Tables

### Employees
Id, FirstName, LastName, Email, Position, HireDate

### Users
Id, Username, Email, PasswordHash

---

# API Requirements

## Authentication API

POST /api/auth/register

POST /api/auth/login

GET /api/auth/profile

## Employees API

All Employee endpoints require authentication and authorization.

GET /api/employees

GET /api/employees/{id}

POST /api/employees

PUT /api/employees/{id}

DELETE /api/employees/{id}

---

# Frontend Requirements

The frontend application shall be developed using Angular 22.

The application shall leverage RxJS and Angular Signals.

The application shall use Angular Standalone Components.

## Frontend Technology Stack

- Angular 22
- TypeScript
- Angular Standalone Components
- Angular Signals
- RxJS
- Angular Router
- Angular HttpClient
- Reactive Forms
- JWT Authentication


# Testing Requirements

The solution must be developed following Test-Driven Development (TDD) principles.

## Backend Unit Tests using XUnit

- Domain Layer
- Application Layer
- Infrastructure Layer
- API Layer

## Frontend Unit Tests using Jest

- Angular Standalone Components
- Angular Services
- Route Guards
- Reactive Forms
- Authentication Flows
- Employee CRUD Operations

---

# Acceptance Criteria

1. User Registration
2. User Login
3. Access Protected Endpoint Without Authentication returns 401.
4. Access Protected Endpoint With Authentication succeeds.
5. Create Employee.
6. Retrieve Employees.
7. Retrieve Employee Details.
8. Update Employee.
9. Delete Employee.
10. Validation Failure handling.

---

# Definition of Done

- Clean Architecture implemented.
- NET Web API completed.
- SQL Server database created and configured.
- Entity Framework Core configured using Code First.
- Database schema managed through Entity Framework Core Migrations.
- Angular 22 frontend implemented.
- Angular Standalone Components implemented.
- Reactive Programming implemented using RxJS.
- User authentication implemented using JWT.
- ASP.NET Core Authorization configured.
- All Employee endpoints protected using authorization.
- Employee CRUD functionality fully operational.
- Backend unit tests implemented and passing.
- Frontend unit tests implemented and passing.
- README documentation included with setup instructions.
- Application can be executed locally using seeded credentials.
- Code follows SOLID principles and clean coding standards.
