# EventPro API - Backend 🚀

This repository contains the backend API for the **EventPro Platform**. It is a robust and scalable .NET 8 application responsible for handling all business logic, data storage, and role-based user management. It serves as the backbone for the [EventPro Frontend](https://github.com/Simon990723/eventpro-api-frontend.git).

**Live API URL**: https://simon-eventpro.com

---

## 🔗 Corresponding Frontend

This API is designed to work with the official EventPro frontend application.
*   **Frontend Repository**: [https://github.com/Simon990723/eventpro-api-frontend.git](https://github.com/Simon990723/eventpro-api-frontend.git)

---

## ✨ Key Features

- **🎭 Role-Based Access Control (RBAC)**: Secure authentication distinguishing between two main roles:
    - **Event Creators**: Can create, manage, and delete their own events.
    - **Normal Users (Attendees)**: Can browse events, purchase tickets, and manage their registrations.
- **🛠️ Full Event Management for Creators**: Dedicated endpoints allowing Event Creators to perform complete CRUD (Create, Read, Update, Delete) operations for the events they own.
- **🤖 AI-Powered Content Generation**: Integrates with a **Google AI service**, providing an exclusive feature for Event Creators to automatically generate compelling event descriptions and details.
- **🎟️ Ticketing and Invoicing**: Allows Normal Users to purchase tickets for events and download PDF invoices for their records.
- **🔐 Secure JWT Authentication**: Protects API endpoints using JSON Web Tokens (JWT), ensuring that users can only access resources appropriate for their assigned role.
- **📦 Containerized & Scalable**: Fully configured to run within Docker and deployed on AWS App Runner for high availability and scalability.

---

## 💻 Tech Stack

- **Framework**: .NET 9
- **Language**: C#
- **API Style**: RESTful API
- **Database**: PostgreSQL 🐘
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity with JWT Bearer Tokens
- **AI Integration**: Google AI SDK
- **Containerization**: Docker 🐳

---

## 📚 API Documentation

The API is self-documenting using **Swagger (OpenAPI)**. When the application is running locally, you can access the interactive Swagger UI to view and test all available endpoints, including those protected by authentication.

*   **Swagger UI URL**: `http://localhost:5189/swagger`

---

## 🚀 Getting Started

Follow these instructions to get the backend server up and running on your local machine.

### Prerequisites

- .NET 9 SDK
- Docker Desktop (Recommended)
- A PostgreSQL instance
- An IDE like Visual Studio 2022 or VS Code

### Installation

1.  Clone the repository:
    ```sh
    git clone [your-backend-repo-url]
    ```
2.  Navigate to the project directory:
    ```sh
    cd eventpro-api-backend 
    ```
3.  Configure your local secrets. Initialize it first:
    ```sh
    dotnet user-secrets init
    ```
4.  Set your configuration values. You will need the database connection string, JWT settings, and your Google AI API Key:
    ```sh
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=eventpro_db;Username=postgres;Password=yourpassword"
    dotnet user-secrets set "JwtSettings:Key" "A_SUPER_SECRET_AND_LONG_KEY_FOR_JWT_SIGNING"
    dotnet user-secrets set "JwtSettings:Issuer" "EventProAPI"
    dotnet user-secrets set "JwtSettings:Audience" "EventProClient"
    dotnet user-secrets set "GoogleAi:ApiKey" "YOUR_GOOGLE_AI_API_KEY"
    ```
5.  Apply the database migrations to create the schema:
    ```sh
    dotnet ef database update
    ```
6.  Run the application:
    ```sh
    dotnet run
    ```
The API will now be running at `http://localhost:5189`.

---

### 🐳 Running with Docker

A `docker-compose.yml` file is included for easily running the entire backend stack (API + Database).

1.  Ensure Docker Desktop is running.
2.  Create a `.env` file in the root directory and add your Google AI API key:
    ```
    GOOGLE_AI_API_KEY=YOUR_GOOGLE_AI_API_KEY
    ```
3.  From the root of the project directory, run:
    ```sh
    docker-compose up --build
    ```
This command will build the API's Docker image, inject the environment variables, and start both the API and a PostgreSQL container.

---

## ☁️ Deployment

This API is designed for containerized deployments and is currently running on **AWS App Runner**.

The CI/CD pipeline is configured to:
1.  Build a new Docker image upon every push to the `main` branch.
2.  Push the image to **Amazon ECR (Elastic Container Registry)**.
3.  Trigger a new deployment on **AWS App Runner**, which pulls the latest image and updates the service with zero downtime. Environment variables for production are securely managed in the App Runner service configuration.
