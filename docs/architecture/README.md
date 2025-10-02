# Architecture Overview

This repository contains a full-stack application with a TypeScript/React frontend and an ASP.NET Core backend.

Guiding principles:
- Domain-driven boundaries: isolate Domain, Application, Infrastructure, and Presentation concerns.
- Feature-first organization on the frontend for scalability and discoverability.
- Clear contracts: DTOs for API boundaries; mapping between domain and transport.
- Composition over inheritance; functional components and hooks in the frontend.
- Security and reliability: secure defaults, defense in depth, and observable behavior.
- Incremental migration: adopt the target structure in steps without risky big-bangs.

## Target Repository Layout

- frontend/ — React app (feature-first)
- backend/
  - src/
    - EventPro.Api/ (Presentation)
    - EventPro.Application/ (Use cases, interfaces, validation)
    - EventPro.Domain/ (Entities, value objects, domain events)
    - EventPro.Infrastructure/ (EF Core, external services, persistence)
  - tests/
    - EventPro.UnitTests/
    - EventPro.IntegrationTests/
- docs/
  - architecture/
  - conventions/

This structure supports clean separation, testability, and team scaling.
