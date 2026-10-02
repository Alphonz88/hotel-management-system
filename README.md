# Hotel Management System

A hotel management web application developed as a practical project during a **Value Added Course (VAC) conducted through MAHAT.AI**, a No-Code/Low-Code application development platform.

> **Note:** This repository contains the project source exported from the development platform. Environment-specific credentials, database passwords, API keys, and deployment configuration have been replaced with placeholders for security.

## Project Overview

The Hotel Management System is designed to support common hotel operations through a web-based administrative application and backend API.

### Main Modules

- Guest management
- Room management
- Reservation management
- Check-in management
- Check-out management
- Payment management
- Invoice management
- Receptionist management
- Housekeeping management
- Feedback management
- User/profile management
- Alert template management
- Mail log management
- Lookup and tenant management
- Role/authentication-related workflows
- AI-assisted alert content generation / image analysis integration

## Architecture

```text
                    ┌─────────────────────────┐
                    │       Admin Web App      │
                    │ ASP.NET Core MVC / Razor │
                    └────────────┬────────────┘
                                 │
                                 │ HTTP / API
                                 ▼
                    ┌─────────────────────────┐
                    │       Web API Layer      │
                    │     ASP.NET Core API     │
                    └────────────┬────────────┘
                                 │
                                 ▼
                    ┌─────────────────────────┐
                    │ Data Access Layer (DAL)  │
                    │        Npgsql            │
                    └────────────┬────────────┘
                                 │
                                 ▼
                    ┌─────────────────────────┐
                    │      PostgreSQL DB       │
                    └─────────────────────────┘
```

## Technology Stack

- MAHAT.AI — No-Code/Low-Code development platform
- C# / .NET Core 3.1
- ASP.NET Core MVC
- ASP.NET Core Web API
- Razor Views
- PostgreSQL
- Npgsql
- Bootstrap / JavaScript / jQuery
- Swagger / OpenAPI
- NLog
- JWT / IdentityServer-related authentication components
- OpenAI/Azure OpenAI integration present in the exported application

## Repository Structure

```text
HotelManagementSystem1/
├── Admin/                         # Web/admin application
├── HotelManagementSystem1.Models/ # Data models
├── HotelManagementSystem1.DAL/    # Database/data-access layer
├── HotelManagementSystem1WebApi/  # Backend REST API
├── PlaywrightTests/               # Automated test project/files
└── HotelManagementSystem1.sln     # Visual Studio solution
```

## Database

The project includes SQL scripts under:

```text
HotelManagementSystem1.DAL/DBScripts/
```

These scripts cover operations for modules such as guests, rooms, reservations, check-in, check-out, payments, invoices, housekeeping, users, feedback, and related entities.

## Configuration

Before running the application, replace the placeholder values in the appropriate `appsettings*.json` files with your own local configuration.

**Never commit:**

- Database passwords
- Email passwords/app passwords
- API keys
- JWT/security keys
- Private deployment credentials
- Production connection strings

For a production deployment, prefer environment variables or a secret-management system rather than storing credentials in source control.

## Educational Context

This project was completed as part of a **MAHAT.AI Value Added Course**, providing practical exposure to No-Code/Low-Code application development and the structure of a multi-layer web application.

## Author

**Alphonse Tony C**

B.Tech – Artificial Intelligence & Data Science

## Disclaimer

This repository is intended for educational and portfolio purposes. Deployment-specific configuration and credentials are intentionally excluded.
