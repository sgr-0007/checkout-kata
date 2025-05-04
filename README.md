# Checkout Kata

## Project Overview

This project implements a supermarket checkout system that calculates the total price of items based on individual prices and special offers. The implementation has been refactored to follow clean coding principles, with a focus on:

- Simplified class structure
- Dependency injection
- Testability
- Maintainability

## Key Features

- **Unified Pricing Rule Model**: A single `PricingRule` class that handles both unit pricing and special offers
- **Dependency Injection**: Clean DI implementation using Microsoft.Extensions.DependencyInjection
- **Testable Design**: All components are designed for easy testing with mocks and stubs
- **Clean Architecture**: Clear separation of concerns with interfaces and implementations

## Architecture

The project is structured around the following components:

### Core Components

- **Checkout**: Handles scanning items and calculating the total price
- **PricingRuleProvider**: Provides pricing rules for specific SKUs
- **PricingRule**: Represents a pricing rule with unit price and optional special offers

### Interfaces

- **ICheckout**: Defines methods for scanning items and getting the total price
- **IPricingRuleProvider**: Defines methods for getting pricing rules by SKU
- **IPricingRule**: Defines properties and methods for pricing rules

### Dependency Injection

The project uses constructor injection throughout, with extension methods for registering services:

```csharp
// Register services with specific pricing rules
services.AddCheckoutWithRules(pricingRules);
```

## Testing

The project includes comprehensive tests that verify:

- Basic scanning functionality
- Unit pricing calculations
- Special offer applications
- Edge cases

Tests use the DI container to create properly configured instances:

```csharp
var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
var checkout = serviceProvider.GetRequiredService<ICheckout>();
```

## Refactoring Journey

This project has undergone several refactoring phases:

1. **Initial Implementation**: Basic checkout functionality with separate pricing rule classes
2. **Model Simplification**: Unified the pricing rule model to a single implementation
3. **Dependency Injection**: Added proper DI support with constructor injection
4. **Code Cleanup**: Removed redundant and unused code elements
5. **Documentation**: Improved XML documentation and README

## Getting Started

### Prerequisites

- .NET 8 SDK

### Building and Running

```bash
# Clean the solution
dotnet clean

# Build the solution
dotnet build

# Run the tests
dotnet test
```

## License

This project is open source and available under the MIT License.
