# Flexify API

A scalable appointment management system built with clean architecture principles.

## Tech Stack

- **.NET 8** — ASP.NET Core Web API
- **CQRS** — MediatR-based command/query separation
- **Onion Architecture** — strict dependency inversion
- **Redis** — distributed caching
- **Docker** — containerized deployment
- **FluentValidation** — request validation pipelines
- **Serilog** — structured logging

## Architecture

```
src/
├── Flexify.API/            # Entry point, middleware, DI
├── Flexify.Application/    # Commands, queries, DTOs (MediatR)
├── Flexify.Domain/         # Entities, value objects, interfaces
└── Flexify.Infrastructure/ # EF Core, Redis, external services
```

## Features

- Appointment scheduling and management
- Global exception handling middleware
- Validation pipeline with detailed error responses
- Structured logging for production observability
- Fully containerized with Docker

## Getting Started

```bash
git clone https://github.com/enesbilik/Flexify-api.git
cd Flexify-api
docker-compose up
```

API runs at `http://localhost:5000` — Swagger UI at `/swagger`.

## License

MIT
