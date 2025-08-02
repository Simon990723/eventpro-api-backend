# EventPro Platform - Backend API

A full-stack, role-based event management platform built with .NET 9 and React 19, featuring an AI assistant and a transactional PDF invoicing system, deployed on AWS.

**Note:** This is the backend API component. The corresponding frontend can be found at: `[LINK TO YOUR FRONTEND REPO WILL GO HERE]`

---

## Key Features

*   **Secure, Role-Based Authentication & Authorization:** Built with ASP.NET Core Identity and JWTs for secure, stateless API communication.
*   **Distinct User Roles ("Creator" vs. "User"):** Provides a tailored API experience with granular permissions, ensuring users can only access and manage their own data.
*   **AI-Powered Content Generation:** Integrates with the Google Gemini API to provide an "AI Event Assistant" that can generate event details from a simple prompt.
*   **Dynamic PDF Invoice/Receipt Generation:** A dedicated API endpoint that generates professional PDF documents on demand using the QuestPDF library, demonstrating experience with file stream-based responses.
*   **Professional API Architecture:** Designed with a clean, scalable structure using Data Transfer Objects (DTOs) to prevent common serialization errors and ensure a secure and efficient data flow between the client and server.

## Tech Stack

*   **Framework:** .NET 9, ASP.NET Core Web API
*   **Database:** PostgreSQL
*   **Object-Relational Mapper (ORM):** Entity Framework Core 9
*   **Authentication:** ASP.NET Core Identity, JSON Web Tokens (JWT)
*   **Document Generation:** QuestPDF
*   **API Integration:** Google Gemini API (via HttpClient)

## Architectural Highlights & Challenges Solved

### 1. The DTO Pattern for API Security and Stability

*   **Challenge:** A classic challenge in APIs with related data is the "infinite loop" during JSON serialization (e.g., an `Event` has `Registrants`, and a `Registrant` has an `Event`).
*   **Solution:** I solved this by implementing the professional **Data Transfer Object (DTO)** pattern. Instead of returning raw database models, the API endpoints return clean, simple DTOs (like `RegistrantResponseDto`) that contain only the necessary data and have no circular references. This makes the API more secure, more efficient, and prevents critical runtime crashes.

### 2. Role-Based Access Control (RBAC)

*   **Challenge:** The application required two different user experiences: a management dashboard for "Creators" and a public discovery platform for "Users".
*   **Solution:** I implemented a full RBAC system using ASP.NET Core Identity Roles. API endpoints are secured with `[Authorize]` attributes, and the logic within each endpoint is further secured to check for ownership (`e.g., event.UserId == currentUserId`), ensuring that a user can never access or modify data that does not belong to them.

## Setup & How to Run Locally

1.  **Clone the repository:** `git clone [your-repo-url]`
2.  **Configure User Secrets:**
    *   Navigate to the `EventProRegistration` project folder.
    *   Initialize user secrets: `dotnet user-secrets init`
    *   Set your Google API Key: `dotnet user-secrets set "GoogleApiKey" "YOUR_GOOGLE_API_KEY"`
3.  **Configure Database Connection:**
    *   In `appsettings.Development.json`, update the `DefaultConnection` string with your local PostgreSQL credentials.
4.  **Apply Migrations:**
    *   Run `dotnet ef database update` to create the database and apply the schema.
5.  **Run the project:**
    *   Run the application from your IDE or with `dotnet run`. The API will be available at `http://localhost:5189`.
