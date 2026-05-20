# Project Management System API

A clean and scalable ASP.NET Core Web API for managing projects and tasks with JWT Authentication, built using Clean Architecture principles.

---

# 🚀 Features

## Authentication & Authorization

* User Registration
* User Login
* JWT Authentication
* Protected Endpoints using `[Authorize]`

---

## Projects Management

* Create Project
* Get All User Projects
* Get Project By Id
* Update Project
* Delete Project

---

## Tasks Management

* Create Task
* Get Tasks By Project
* Update Task Status
* Delete Task

---

## Security Features

* JWT Token Validation
* Ownership Validation
* Users can only access their own projects/tasks

---

## Validation & Error Handling

* FluentValidation
* Global Exception Handling Middleware
* Standardized API Responses

---

# 🛠️ Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* JWT Authentication
* FluentValidation
* Swagger / OpenAPI

---

# 🧱 Architecture

The project follows Clean Architecture principles and is divided into the following layers:

## Domain

Contains:

* Entities
* Enums

---

## Application

Contains:

* DTOs
* Interfaces
* Validators
* Common Models

---

## Infrastructure

Contains:

* Database Context
* Identity
* JWT Services
* Service Implementations
* Entity Configurations

---

## API

Contains:

* Controllers
* Middlewares
* Extensions
* Program Configuration

---

# 📦 Project Structure

```bash
ProjectManagementSystem
│
├── API
├── Application
├── Domain
├── Infrastructure
```

---

# ⚙️ Setup Instructions

## 1️⃣ Clone Repository

```bash
git clone <your-repository-url>
```

---

## 2️⃣ Update Connection String

Open:

```bash
API/appsettings.json
```

Update:

```json
"ConnectionStrings": {
  "DefaultConnection": "YOUR_CONNECTION_STRING"
}
```

---

## 3️⃣ Apply Migrations

Run:

```bash
dotnet ef database update --project Infrastructure --startup-project API
```

---

## 4️⃣ Run The Project

```bash
dotnet run --project API
```

---

# 🔐 Authentication

This API uses JWT Authentication.

After login/register, copy the returned token and use it in Swagger Authorize section:

```text
Bearer YOUR_TOKEN
```

---

# 📌 API Endpoints

# Auth

| Method | Endpoint           | Description   |
| ------ | ------------------ | ------------- |
| POST   | /api/auth/register | Register User |
| POST   | /api/auth/login    | Login User    |

---

# Projects

| Method | Endpoint           |
| ------ | ------------------ |
| POST   | /api/projects      |
| GET    | /api/projects      |
| GET    | /api/projects/{id} |
| PUT    | /api/projects/{id} |
| DELETE | /api/projects/{id} |

---

# Tasks

| Method | Endpoint                       |
| ------ | ------------------------------ |
| POST   | /api/tasks                     |
| GET    | /api/tasks/project/{projectId} |
| PUT    | /api/tasks/{taskId}/status     |
| DELETE | /api/tasks/{taskId}            |

---

# ✅ Validation

The project uses FluentValidation for request validation.

Example validation rules:

* Required fields
* Email validation
* Password validation
* Future DueDate validation

---

# ⚠️ Error Handling

Global Exception Handling Middleware is implemented to provide clean and consistent error responses.

---

# 🔒 Ownership Validation

Users can only:

* Access their own projects
* Access tasks belonging to their own projects

---

# 📖 Swagger

Swagger UI is enabled for API testing and documentation.

When running locally:

```bash
https://localhost:{port}/
```

---

# 👨‍💻 Author

Eslam Ahmed

Backend Developer (.NET / ASP.NET Core)
