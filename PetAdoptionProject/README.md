# Pet Adoption Project
A clean architecture API for managing pet adoptions with validation, business logic separation, and scalable data persistence.

## Overview
This project is a **Pet Adoption Management System** built with:
- **ASP.NET Core 10** for the API
- **Entity Framework Core** for database access
- **Clean Architecture** to separate concerns
- **Comprehensive validation** and error handling

### What It Does
Users can:
- Manage pets (create, retrieve, update, delete)
- Manage adopters (create, retrieve)
- Process pet adoptions with breed-specific validation rules
- View paginated pet listings

---

## Architecture

The solution is organized into **4 layers**:

### 1. **Domain** (`Domain/`)
The core business logic and rules. Contains:
- **Entities**: `Pet`, `Adopter`, `Adoption` (with validation guards)
- **Enums**: Pet types (Dog, Cat, Hamster)
- **Strategies**: Breed-specific adoption rules for each pet type
- **Factories**: Create adopters with business logic

### 2. **Application** (`Application/`)
Use cases and data flow. Contains:
- **Services**: `PetManagementService`, `AdopterService`, `PetAdoptionService`
- **DTOs**: Request/Response models for API contracts
- **Validators**: FluentValidation rules (auto-validated on API requests)
- **Repository Interfaces**: Abstract database operations

### 3. **Infrastructure** (`Infrastructure/`)
Data persistence and external concerns. Contains:
- **DbContext**: EF Core configuration and migrations
- **Repositories**: Implement data access operations
- **Unit of Work**: Manages transactions across repositories
- **Database Seeding**: Pre-populate test data

### 4. **Presentation** (`PetAdoptionProject/`)
API endpoints and middleware. Contains:
- **Controllers**: Handle HTTP requests
- **Middleware**: Exception handling and error formatting
- **Extensions**: Service registration and Swagger setup
- **Configuration**: Database connection settings

---

## Key Design Patterns Used

### 1. **Strategy Pattern** (Adoption Validation)
Each pet type (Dog, Cat, Hamster) has different adoption rules.
- `IAdoptionStrategy` interface defines validation contract
- `DogAdoptionStrategy`, `CatAdoptionStrategy`, etc., implement specific rules
- `AdoptionStrategyResolver` picks the right strategy at runtime

### 2. **Repository Pattern** (Data Access)
- `IPetRepository`, `IAdopterRepository`, `IAdoptionRepository` define contracts
- Concrete repositories implement actual database queries
- `IUnitOfWork` manages all repositories and transactions

### 3. **Factory Pattern** (Adopter Creation)
- `IAdopterFactory` standardizes adopter creation
- Generates unique `ClientCode` automatically

### 4. **Dependency Injection** (Service Registration)
- Services are registered in `Program.cs` using `AddCustomServices()`
- Controllers receive dependencies through constructors
- 
### 5. **Pagination** (List Performance)
- `PagedResult<T>` returns paginated data efficiently
- Prevents loading millions of records into memory

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (update connection string in `appsettings.json`)

### Setup

1. **Clone the repository**

2. **Update database connection**
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=PetAdoption;Trusted_Connection=true;"
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update --project Infrastructure
   ```

4. **Run the API**

5. **Access Swagger UI**
   Navigate to `https://localhost:xxxx/swagger`

---

## API Endpoints

### Pets
- `POST /api/pets` - Create a new pet
- `GET /api/pets/{id}` - Get pet by ID
- `GET /api/pets?page=1&pageSize=10` - List pets with pagination
- `PUT /api/pets/{id}` - Update a pet
- `DELETE /api/pets/{id}` - Delete a pet

### Adopters
- `POST /api/adopters` - Create a new adopter
- `GET /api/adopters/{id}` - Get adopter by ID
- `GET /api/adopters` - List all adopters

### Adoptions
- `POST /api/adoptions` - Process an adoption (with validation)

---

## Validation

The API uses **FluentValidation** for request validation:
- Automatic model validation on all API requests
- Custom rules in validator classes
- Business rules enforced at adoption time (breed-specific)
- Global exception handling returns user-friendly error messages

---

## Testing

### Unit Tests (`UnitTests/`)
Test individual services and business logic in isolation.

### Integration Tests (`IntegrationTests/`)
Test full workflow: create pet → create adopter → process adoption.


## Error Handling

Errors return structured responses:
```json
{
  "statusCode": 400,
  "message": "ageAtArrival cannot be negative"
}
```

Or:
```json
{
  "statusCode": 404,
  "message": "Pet not found"
}
```

Handled errors:
- ✗ Invalid input (validation) → 400
- ✗ Resource not found → 404
- ✗ Business rule violations (breed restrictions) → 400
- ✗ Server errors → 500

---

## Dependencies

- `Microsoft.EntityFrameworkCore` - ORM
- `FluentValidation` - Request validation
- `SharpGrip.FluentValidation.AutoValidation` - Auto-validate on API
- `Swashbuckle.AspNetCore` - Swagger documentation

---
 