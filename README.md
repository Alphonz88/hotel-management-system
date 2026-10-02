# 🏨 Hotel Management System

A web-based **Hotel Management System** developed as a practical project during a **Value Added Course (VAC)** conducted through **MAHAT.AI**, a No-Code/Low-Code application development platform.

The system manages core hotel operations such as guests, rooms, reservations, check-in/check-out, payments, invoices, housekeeping, receptionists, feedback, users, and administrative activities.

## 📌 Project Overview

### Main Modules

- 👤 Guest Management
- 🛏️ Room Management
- 📅 Reservation Management
- 🛎️ Check-In Management
- 🚪 Check-Out Management
- 💳 Payment Management
- 🧾 Invoice Management
- 🧹 Housekeeping Management
- 👨‍💼 Receptionist Management
- ⭐ Feedback Management
- 👥 User Management
- 🔐 User Lockout & Security
- 🔔 Alert Templates
- 📧 Mail Logs
- 🏢 Tenant Management
- 🔎 Lookup / Master Data Management

## ✨ Key Features

### 👤 Guest Management
- Add and manage guest information
- Maintain guest records
- Retrieve guest details through the application
- Support guest information required for reservations and hotel operations

### 🛏️ Room Management
- Maintain room information
- Manage room-related details and availability
- Support room allocation during reservations and check-in
- Organize room data through the database layer

### 📅 Reservation Management
- Create and manage reservations
- Store reservation-related guest and room information
- Support reservation status and related hotel operations
- Connect reservations with check-in and payment workflows

### 🛎️ Check-In Management
- Manage guest check-in details
- Connect guest, reservation, and room information
- Maintain check-in records

### 🚪 Check-Out Management
- Manage guest check-out
- Maintain checkout information
- Support payment and invoice-related operations

### 💳 Payment Management
- Store payment information
- Associate payments with hotel transactions
- Support payment-related processing

### 🧾 Invoice Management
- Maintain invoice records
- Connect invoices with guest and payment information
- Support hotel billing operations

### 🧹 Housekeeping Management
- Manage housekeeping-related records
- Track room housekeeping activities
- Support hotel room maintenance workflows

### 👨‍💼 Receptionist Management
- Maintain receptionist information
- Support front-desk related operations
- Integrate receptionist information with the hotel management workflow

### ⭐ Feedback Management
- Store guest feedback
- Manage feedback records
- Support collection of guest experience information

### 👥 User Management
- Manage application users
- Support authentication-related user operations
- Provide user administration features

### 🔐 User Lockout
- Support user lockout functionality
- Help manage restricted user access scenarios
- Maintain lockout-related information

### 🔔 Alert Templates
- Maintain reusable alert templates
- Support application notifications and alert-related workflows

### 📧 Mail Logs
- Maintain records related to application emails
- Support tracking of email activity

### 🤖 OpenAI Integration

The project contains an `OpenAIController` within the Web API layer for OpenAI-related functionality.

This provides a foundation for integrating AI-powered features into the hotel management workflow.

> **Note:** API keys and other sensitive credentials must never be committed to the public repository.

# 🏗️ System Architecture

The project follows a layered architecture that separates the web application, API, models, data access, and database responsibilities.

```text
                         ┌──────────────────────┐
                         │        User          │
                         │  Admin / Reception   │
                         └──────────┬───────────┘
                                    │
                                    ▼
                     ┌───────────────────────────┐
                     │      Admin Web App        │
                     │ ASP.NET MVC / Razor Views │
                     └─────────────┬─────────────┘
                                   │
                                   ▼
                     ┌───────────────────────────┐
                     │       Web API Layer       │
                     │       ASP.NET Core        │
                     └─────────────┬─────────────┘
                                   │
                    ┌──────────────┴──────────────┐
                    ▼                             ▼
          ┌──────────────────┐          ┌──────────────────┐
          │  Models Layer    │          │  Data Access     │
          │ C# Domain Models │          │      Layer       │
          └──────────────────┘          └────────┬─────────┘
                                                  │
                                                  ▼
                                      ┌────────────────────┐
                                      │    PostgreSQL      │
                                      │      Database      │
                                      └────────────────────┘
```

**Architecture Flow:** User → Admin Web App → Web API → Models / DAL → PostgreSQL

# 📂 Project Structure

```text
HotelManagementSystem1-GitHub/
│
├── Admin/
│   ├── Controllers/
│   │   ├── AlertTemplates/
│   │   ├── CheckIN/
│   │   ├── Checkout/
│   │   ├── Feedback/
│   │   ├── Guest/
│   │   ├── Housekeeping/
│   │   ├── Invoice/
│   │   ├── MailLogs/
│   │   ├── OpenAI/
│   │   ├── Payment/
│   │   ├── Receptionist/
│   │   ├── Reservation/
│   │   ├── Room/
│   │   ├── System/
│   │   ├── tenant/
│   │   ├── userlockout/
│   │   └── users/
│   ├── Views/
│   ├── wwwroot/
│   └── ...
│
├── HotelManagementSystem1WebApi/
│   ├── Controllers/
│   ├── Program.cs
│   ├── Startup.cs
│   ├── ConnectionSettings.cs
│   ├── ExternalSystemUtility.cs
│   ├── ErrorLoggingMiddleware.cs
│   ├── NLog.config
│   ├── appsettings.json
│   └── ...
│
├── HotelManagementSystem1.Models/
│   ├── Guest/
│   ├── Room/
│   ├── Reservation/
│   ├── CheckIN/
│   ├── Checkout/
│   ├── Payment/
│   ├── Invoice/
│   ├── Housekeeping/
│   ├── Receptionist/
│   ├── Feedback/
│   ├── users/
│   └── ...
│
├── HotelManagementSystem1.DAL/
│   ├── DBScripts/
│   │   ├── AlertTemplates/
│   │   ├── CheckIN/
│   │   ├── Checkout/
│   │   ├── Feedback/
│   │   ├── Guest/
│   │   ├── Housekeeping/
│   │   ├── Invoice/
│   │   ├── MailLogs/
│   │   ├── Payment/
│   │   ├── Receptionist/
│   │   ├── Reservation/
│   │   ├── Room/
│   │   ├── tenant/
│   │   └── users/
│   ├── ModelConverter.cs
│   └── ...
│
├── README.md
└── ...
```

# 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| **C#** | Application development |
| **ASP.NET Core** | Web application / backend framework |
| **ASP.NET Core Web API** | REST API layer |
| **ASP.NET MVC / Razor Views** | Admin web interface |
| **PostgreSQL** | Relational database |
| **SQL** | Database queries and scripts |
| **Data Access Layer (DAL)** | Database access and operations |
| **NLog** | Application logging |
| **OpenAI API Integration** | AI-related functionality |
| **MAHAT.AI** | No-Code/Low-Code platform used in the VAC context |
| **Git** | Version control |
| **GitHub** | Source-code hosting and collaboration |

# 🗄️ Database Layer

The system uses **PostgreSQL** as the relational database.

```text
HotelManagementSystem1.DAL/
└── DBScripts/
    ├── AlertTemplates/
    ├── CheckIN/
    ├── Checkout/
    ├── Feedback/
    ├── Guest/
    ├── Housekeeping/
    ├── Invoice/
    ├── MailLogs/
    ├── Payment/
    ├── Receptionist/
    ├── Reservation/
    ├── Room/
    ├── tenant/
    └── users/
```

# 🔄 Application Workflow

```text
Guest
  │
  ▼
Reservation
  │
  ▼
Room Allocation
  │
  ▼
Check-In
  │
  ▼
Hotel Stay
  │
  ├──────────────► Housekeeping
  │
  ▼
Payment
  │
  ▼
Invoice
  │
  ▼
Check-Out
  │
  ▼
Feedback
```

# 🔒 Security

The project contains configuration areas that may require sensitive values such as:

- Database credentials
- Email credentials
- API keys
- OpenAI keys
- Azure/service credentials
- Security keys
- Connection strings

**Never commit real credentials, API keys, passwords, or private connection strings to GitHub.** Use environment variables, secure configuration, or deployment-specific secret management for production systems.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_DATABASE_CONNECTION_STRING"
  }
}
```

# 🚀 Getting Started

## Prerequisites

- Visual Studio
- .NET SDK compatible with the project
- PostgreSQL
- Git
- Internet connection for required package restoration
- Appropriate local configuration values

## 1. Clone the Repository

```bash
git clone https://github.com/Alphonz88/hotel-management-system.git
cd hotel-management-system
```

## 2. Open the Solution

Open the project/solution in **Visual Studio** and review the available projects:

```text
Admin
HotelManagementSystem1WebApi
HotelManagementSystem1.Models
HotelManagementSystem1.DAL
```

## 3. Configure PostgreSQL

Create/configure the PostgreSQL database required by the application. Update the local configuration with your own database connection information. Do not commit your actual database password or private connection string.

## 4. Restore Dependencies

```bash
dotnet restore
```

## 5. Build the Project

```bash
dotnet build
```

## 6. Run the Application

Launch the application through Visual Studio using the configured startup project. Check the API and application configuration before starting the application.

# 📦 Main Projects

## `Admin`

The web-facing administration application responsible for the user interface and hotel management screens, including guests, rooms, reservations, check-in/check-out, payments, invoices, housekeeping, feedback, and administrative operations.

## `HotelManagementSystem1WebApi`

The backend API layer responsible for API endpoints, application/business processing, controller operations, external system utilities, error logging middleware, OpenAI-related integration, and communication between the web application and data layer.

Important files include:

```text
Program.cs
Startup.cs
ConnectionSettings.cs
ExternalSystemUtility.cs
ErrorLoggingMiddleware.cs
NLog.config
appsettings.json
```

## `HotelManagementSystem1.Models`

Contains C# models representing the application's domain entities, including Guest, Room, Reservation, Check-In, Check-Out, Payment, Invoice, Housekeeping, Receptionist, Feedback, and Users.

## `HotelManagementSystem1.DAL`

The Data Access Layer responsible for database operations, SQL scripts, data access logic, model conversion, and module-specific database operations.

# 🧩 Functional Modules

```text
                    HOTEL MANAGEMENT SYSTEM
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
   Guest & Room         Reservations          User Management
        │                     │                     │
        ▼                     ▼                     ▼
  Check-In / Out        Payments & Billing     Security
        │                     │                     │
        ▼                     ▼                     ▼
  Housekeeping             Invoice             Feedback
        │
        ▼
  Alerts / Mail Logs
        │
        ▼
   AI Integration
```

# 🎓 Academic Context

This project was developed as a practical software project during a **Value Added Course (VAC)** conducted through **MAHAT.AI**.

The project provided practical exposure to application development, No-Code/Low-Code development concepts, C# and .NET application structure, ASP.NET Core Web API, database-driven applications, PostgreSQL, Data Access Layer design, authentication and user management, logging, API integration, AI integration, and Git/GitHub collaboration.

# 📚 Learning Outcomes

1. **Software Architecture** — Understanding how different application layers communicate.
2. **Backend Development** — Working with C#, ASP.NET Core, and Web APIs.
3. **Database Management** — Working with PostgreSQL and SQL-based data operations.
4. **Data Access** — Understanding how a DAL separates database operations from other application components.
5. **API Integration** — Understanding communication between frontend/admin applications and backend APIs.
6. **Authentication & Security** — Understanding user management and access-related functionality.
7. **Logging & Error Handling** — Using logging and middleware for application monitoring and error handling.
8. **AI Integration** — Exploring OpenAI-based functionality within a software application.
9. **Version Control** — Using Git and GitHub for source-code management and collaboration.


# 👥 Team Members

| Name / GitHub          | Role                            | GitHub Profile                                                             |
| ---------------------- | ------------------------------- | -------------------------------------------------------------------------- |
| **Alphonz Tony C**     | Developer / Project Contributor | [@Alphonz88](https://github.com/Alphonz88)                                 |
| **Asmithabanu**        | Developer / Project Contributor | [@Asmithabanu27](https://github.com/Asmithabanu27)                         |
| **Stefin Surya**       | Developer / Project Contributor | [@stefinsurya1225-debug](https://github.com/stefinsurya1225-debug)         |
| **Madhumitha Vadivel** | Developer / Project Contributor | [@madhumithavadivel20-debug](https://github.com/madhumithavadivel20-debug) |
| **Dharshini Sathiya**  | Developer / Project Contributor | [@dharshinisathiya491-blip](https://github.com/dharshinisathiya491-blip)   |


# 🤝 Collaboration

Create a separate branch for development:

```bash
git checkout -b feature/new-module
git add .
git commit -m "Add new module"
git push origin feature/new-module
```

Then create a Pull Request on GitHub for review and merging.

# 🖼️ Screenshots

Screenshots can be added here to demonstrate the application's interface.

Suggested screenshots:

```text
docs/
├── dashboard.png
├── guest-management.png
├── room-management.png
├── reservation.png
├── check-in.png
├── check-out.png
├── payment.png
├── invoice.png
└── housekeeping.png
```

Example:

```markdown
![Dashboard](docs/dashboard.png)
```

# 🌟 Project Highlights

- 🏨 Complete hotel management workflow
- 👤 Guest and user management
- 🛏️ Room management
- 📅 Reservation management
- 🛎️ Check-in and check-out management
- 💳 Payment management
- 🧾 Invoice management
- 🧹 Housekeeping management
- ⭐ Guest feedback management
- 📧 Mail logging
- 🔔 Alert template management
- 🔐 User lockout functionality
- 🤖 OpenAI integration foundation
- 🗄️ PostgreSQL database
- 🧱 Layered application architecture
- 📝 NLog-based logging
- 🔧 Modular Data Access Layer
- 👥 GitHub-based collaboration

# 🔮 Future Improvements

Possible future enhancements include:

- 📱 Responsive/mobile-friendly interface
- 📊 Advanced hotel analytics dashboard
- 📈 Revenue and occupancy reports
- 🧠 More AI-powered hotel assistance
- 💬 AI-based guest support chatbot
- 📧 Automated email notifications
- 📲 SMS/WhatsApp notification integration
- 💳 Online payment gateway integration
- 📅 Advanced room availability calendar
- 🧹 Automated housekeeping scheduling
- 🔍 Advanced reporting and filtering
- ☁️ Cloud deployment
- 🐳 Docker-based deployment
- 🔐 Improved production security and secret management
- 🧪 Automated unit and integration testing

# 📄 License

This project is intended primarily for **academic, educational, and demonstration purposes**.

If the project is later released under a specific open-source license, replace this section with the selected license information.

# 🙏 Acknowledgement

Special thanks to the organizers and mentors involved in the **Value Added Course conducted through MAHAT.AI** for providing the opportunity to work on a practical Hotel Management System project.

The project provided valuable hands-on exposure to application development, database management, API architecture, AI integration, and collaborative software development.

# ⭐ Support

If you find this project useful for learning or reference, consider giving the repository a ⭐ on GitHub.

Repository: [Alphonz88/hotel-management-system](https://github.com/Alphonz88/hotel-management-system)
