# NBE Banking System

A full-stack banking web application developed during my **Full-Stack Internship at the National Bank of Egypt (NBE)**. Built with **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**, this project simulates core banking functionalities such as user registration, authentication, account management, and secure transactions — aimed to handle real-world scenarios with a large database and optimized backend logic.

---

## Features

### User Module
- Register as a new banking user (with pending status).
- Login with validation for user status (only accepted users can access).
- Secure password reset via phone number.
- Role-based access for users and admins.

### Admin Dashboard
- Approve or reject new user accounts.
- View user list with statuses and details.
- Manage accounts and monitor activity.

### Account Management
- Create new bank accounts with:
  - Account type (Savings/Checking)
  - Default currency (EGP, USD, EUR, etc.)
  - Starting balance of 1000
- List and view account details with current balance and status.

### Transactions
- Transfer money between accounts with:
  - Real-time balance check
  - Receiver account name confirmation (modal popup)
  - Only allows transactions between **active accounts**
- Animated and interactive form UI with real-time feedback.

### UI/UX
- Clean and modern interface using Bootstrap 5.
- Green-themed branding inspired by NBE’s color identity.
- Hover effects, modal confirmations, and responsive layout.
- Animated transitions for a fluid experience.

---

## Tech Stack

ASP.NET Core MVC – Backend web framework

Entity Framework Core – ORM for database access

SQL Server LocalDB – Development database

Bootstrap 5 – Responsive frontend styling

jQuery & JavaScript – UI interactivity and enhancements

ASP.NET Identity / Custom Auth – Secure user authentication and authorization

---
## Demo

Watch a demo of the website in action:

[Click here to view the demo on Google Drive](https://drive.google.com/your-demo-link)

---

## Getting Started

### Prerequisites
- .NET 8 SDK or later
- SQL Server LocalDB installed

### 1. Configure your connection string

Open `appsettings.json` and update the connection string to match your local SQL Server setup:

### 2. Run the EF Core migrations (if needed):
```bash
dotnet ef database update
```

### 3. Build and run the project using Visual Studio or:
```bash
dotnet run
```
---

## Demo

---

## Contact
Let me know if you have any questions or updates — feel free to email me at **saif_ahmed@aucegypt.edu**.

