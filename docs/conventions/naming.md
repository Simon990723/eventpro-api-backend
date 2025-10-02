# Naming Conventions

General
- Use clear, intent-revealing names; avoid abbreviations.
- Prefer nouns for classes and components, verbs for functions/methods.

C# (Backend)
- Projects: EventPro.Api, EventPro.Application, EventPro.Domain, EventPro.Infrastructure
- Namespaces mirror folders (PascalCase).
- Classes/Interfaces: PascalCase; interfaces start with `I` (e.g., `IEventService`).
- Methods/Properties: PascalCase (e.g., `GetByIdAsync`, `UserId`).
- Private fields: `_camelCase`, constants: `PascalCase`.
- Async methods end with `Async`.
- DTOs end with `Dto`; Requests/Responses suffix where meaningful.
- Enums: singular PascalCase members.
- Files: one public class/interface per file; file name matches type.

TypeScript (Frontend)
- Components: PascalCase (EventCard.tsx)
- Hooks: `useXxx` (useEvent, useAuth)
- Types/interfaces: PascalCase; type-only files end with `.types.ts` or colocate in `types.ts`
- API modules: `xxx.api.ts`, service contracts `xxx.service.ts`
- Constants: `UPPER_SNAKE_CASE`
- Utils/helpers: `camelCase`
- File & folder names: kebab-case except React components in PascalCase
- Test files: `*.test.ts(x)` or `*.spec.ts(x)` colocated with source

Git and Commits
- Conventional commits: `feat:`, `fix:`, `refactor:`, `chore:`, `docs:`, `test:`, `perf:`, `build:`, `ci:`
