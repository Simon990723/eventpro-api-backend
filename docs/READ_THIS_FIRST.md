# Read This First: Migration Checklist

Goal: Move to a scalable, modern structure with professional naming—incrementally and safely.

1) Repository Hygiene
- Adopt .editorconfig (already added) and ensure your IDE honors it.
- Ensure consistent LF line endings and UTF-8 encoding.

2) Frontend: Feature-First
- Create `src/app/router/` with centralized route config and lazy-loaded pages.
- Introduce path aliases (`@/*`) in tsconfig and update imports gradually.
- Move shared presentational components to `src/shared/ui`.
- Colocate feature code: `features/events` and `features/auth`.
- Add barrel files (`index.ts`) per folder to simplify imports.
- Migrate page-level components to `features/*/pages` with lazy imports at router level.

3) Backend: Clean Architecture
- Split current backend into projects: Api, Application, Domain, Infrastructure.
- Move EF Core DbContext and migrations to Infrastructure; register in Api.
- Move DTOs and use-case services into Application; expose interfaces for Infrastructure implementations.
- Keep Controllers thin; inject use-cases from Application.
- Centralize configuration binding into options classes (JWT, CORS, external services).
- Add global exception handling middleware and validation pipeline.

4) Observability and Ops
- Add structured logging (Serilog recommended).
- Add `/health` and `/ready` endpoints.
- Add request logging and correlation IDs.

5) Tests
- Add unit tests for Domain and Application.
- Add integration tests for API using a disposable PostgreSQL (Testcontainers).

6) CI Essentials
- Add lint and type-check steps for frontend; build and test both layers.
- Add dotnet format and analyzers check in CI.

Tip: Migrate module-by-module; keep PRs small and reversible.
