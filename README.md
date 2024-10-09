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

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

    