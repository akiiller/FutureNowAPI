# Future Now Streaming Platform

## Overview

Future Now is a streaming platform that provides access to music tracks, video content, and podcasts. The system consists of a REST API backend built with ASP.NET Core and an Android mobile application that consumes the API. The platform allows users to browse, search, and filter multimedia content across different categories.

**Status**: ✅ MVP Complete (October 6, 2025)
- RESTful API with full CRUD operations for Music, Videos, and Podcasts
- In-memory database seeded with sample content
- Swagger documentation available at http://localhost:5000/
- Android mobile app ready for deployment to Android Studio

## User Preferences

Preferred communication style: Simple, everyday language.

## System Architecture

### Backend Architecture

**Framework**: ASP.NET Core 8.0 Web API
- The backend is implemented using ASP.NET Core's minimal API or MVC pattern
- Configured for both HTTP and HTTPS endpoints (ports 5088 and 7219)
- Swagger/OpenAPI integration for API documentation and testing
- Development and production environment configurations separated

**Data Storage**: Entity Framework Core with In-Memory Database
- **Rationale**: Uses EF Core 9.0.9 with an in-memory database provider for rapid development and testing
- **Pros**: Zero configuration, fast iteration, no external database dependencies
- **Cons**: Data is non-persistent and lost on application restart; not suitable for production
- **Alternative Considered**: The architecture supports eventual migration to a persistent database (SQL Server, PostgreSQL, etc.) with minimal code changes due to EF Core abstraction

**API Design**:
- RESTful endpoints for content retrieval (music, videos, podcasts)
- Support for search and filtering operations
- CORS configuration to allow cross-origin requests from the Android app
- JSON serialization for data transfer

**Logging and Diagnostics**:
- Microsoft.Extensions.Logging for application logging
- Configurable log levels per environment (Development vs Production)
- Diagnostic source support for monitoring and tracing

### Frontend Architecture

**Platform**: Native Android Application (Java)
- Minimum SDK and target SDK configured for modern Android devices
- Material Design components for consistent UI/UX

**UI Pattern**: Tabbed Interface
- MainActivity hosts tab navigation for different content types
- Separate detail view (ContentDetailActivity) for individual content items
- RecyclerView-based lists for efficient scrolling and memory management

**Data Layer**:
- Model classes representing API response structures (music tracks, videos, podcasts)
- Retrofit 2.x as HTTP client for API communication
- JSON deserialization handled by Retrofit converters

**Adapter Pattern**:
- Custom RecyclerView adapters for each content type
- ViewHolder pattern for efficient view recycling

### Communication Between Components

**API Client Configuration**:
- Base URL configured in ApiClient.java
- Emulator default: `http://10.0.2.2:5000/` (maps to host machine localhost)
- Physical device: Requires local network IP address of API host
- Retrofit interface definitions for endpoint contracts

**Data Flow**:
1. Android app makes HTTP requests via Retrofit
2. API processes requests and queries in-memory database via EF Core
3. API returns JSON responses
4. Retrofit deserializes JSON to model objects
5. Adapters render data in RecyclerViews

## External Dependencies

### Backend Dependencies

**NuGet Packages**:
- `Microsoft.AspNetCore.OpenApi` (8.0.18) - OpenAPI specification support
- `Microsoft.EntityFrameworkCore.InMemory` (9.0.9) - In-memory database provider
- `Swashbuckle.AspNetCore` (6.6.2) - Swagger UI and documentation generation
- `Microsoft.Extensions.Caching.Memory` - In-memory caching support
- `Microsoft.Extensions.Logging` - Logging infrastructure

**Runtime**:
- .NET 8.0 runtime
- ASP.NET Core 8.0 runtime

### Android Dependencies

**Libraries**:
- Android SDK (Java-based)
- Retrofit 2 - REST client for Android and Java
- Material Design Components - UI component library
- RecyclerView - List display component

### Infrastructure Requirements

**Development Environment**:
- .NET 8.0 SDK for backend development
- Android Studio for mobile app development
- Emulator or physical Android device for testing

**Network Configuration**:
- Local network connectivity required for physical device testing
- Firewall rules may need adjustment for API accessibility
- CORS configuration on API to accept requests from Android app origin

**Build Tools**:
- MSBuild/.NET CLI for backend compilation
- Gradle for Android app build system