# Hangfire Demo API

This is a demo project to show how to use Hangfire in a .NET 8 Web API project.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## NuGet Packages

The following NuGet packages are used in this project:

- [Hangfire](https://www.nuget.org/packages/Hangfire) (Version 1.8.14)
- [Hangfire.AspNetCore](https://www.nuget.org/packages/Hangfire.AspNetCore) (Version 1.8.14)
- [Hangfire.MemoryStorage](https://www.nuget.org/packages/Hangfire.MemoryStorage) (Version 1.8.1.1)
- [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) (Version 8.0.8)
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) (Version 8.0.10)
- [Microsoft.EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory) (Version 8.0.10)
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore) (Version 6.8.1)

### Unit Testing Packages

- [AutoFixture](https://www.nuget.org/packages/AutoFixture) (Version 4.18.1)
- [AutoFixture.AutoNSubstitute](https://www.nuget.org/packages/AutoFixture.AutoNSubstitute) (Version 4.18.1)
- [AutoFixture.Xunit2](https://www.nuget.org/packages/AutoFixture.Xunit2) (Version 4.18.1)
- [coverlet.collector](https://www.nuget.org/packages/coverlet.collector) (Version 6.0.2)
- [FluentAssertions](https://www.nuget.org/packages/FluentAssertions) (Version 6.12.1)
- [MockQueryable.NSubstitute](https://www.nuget.org/packages/MockQueryable.NSubstitute) (Version 7.0.3)
- [NSubstitute](https://www.nuget.org/packages/NSubstitute) (Version 5.1.0)
- [xunit](https://www.nuget.org/packages/xunit) (Version 2.9.2)
- [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio) (Version 2.8.2)

## Getting Started

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/HangfireDemoApi.git
   cd HangfireDemoApi
   ```

2. Open the solution in Visual Studio 2022.

3. Restore the NuGet packages: `donet restore`

4. Run the application: `dotnet run`


## Usage

### Hangfire Dashboard

The Hangfire Dashboard can be accessed at `/hangfire`. It provides a web interface to manage and monitor background jobs.

### Swagger UI

The Swagger UI can be accessed at `/swagger`. It provides a web interface to explore and test the API endpoints.

## Project Structure

- `Controllers/`: Contains the API controllers.
- `Models/`: Contains the data models.
- `Services/`: Contains the business logic and background job services.
- `Program.cs`: The entry point of the application.
- `Startup.cs`: Configures the application services and middleware.



## Unit Testing

This project uses several libraries to facilitate the creation and execution of unit tests. Below is a description of each:

### xUnit

[xUnit](https://xunit.net/) is a unit testing framework for .NET. It is used to define and run unit tests in the project.

### NSubstitute

[NSubstitute](https://nsubstitute.github.io/) is a library for creating mocks and stubs in unit tests. It allows simulating the behavior of dependencies and verifying interactions.

### AutoFixture

[AutoFixture](https://github.com/AutoFixture/AutoFixture) is a library for automatically generating test data. It simplifies the creation of objects with predefined data, reducing repetitive code in tests.

#### AutoFixture.AutoNSubstitute

[AutoFixture.AutoNSubstitute](https://github.com/AutoFixture/AutoFixture) is an extension of AutoFixture that integrates NSubstitute. It allows automatic creation of mocks using NSubstitute.

#### AutoFixture.Xunit2

[AutoFixture.Xunit2](https://github.com/AutoFixture/AutoFixture) is an extension of AutoFixture that integrates xUnit. It allows automatic injection of test data into test methods.

### MockQueryable.NSubstitute

[MockQueryable.NSubstitute](https://github.com/romantitov/MockQueryable) is a library that facilitates the creation of mocks for LINQ queries over `IQueryable` using NSubstitute. It is useful for simulating database queries in unit tests.

## Usage Example

Below is an example of how to use these libraries in a unit test:

```
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using FluentAssertions
using NSubstitute;
using Xunit;

public class MyServiceTests
{
    [Theory, AutoData]
    public void MyTest( [Frozen] IMyDependency dependency, MyService service)
    { 
        // Arrange 
        var expectedValue = "expected value";
        dependency.GetValue().Returns(expectedValue);

        // Act
        var result = service.GetValue();

        // Assert
        result.Should().Be(expectedValue);
     }
}
```

In this example:

- `AutoData` from AutoFixture.Xunit2 is used to automatically generate test data.
- `IMyDependency` is a dependency that is mocked using NSubstitute.
- `MyService` is the class under test.
- `Frozen` is used to freeze the instance of `IMyDependency` so that the same instance is used throughout the test.

These libraries combined allow writing unit tests more efficiently and with less repetitive code.