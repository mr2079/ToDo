# Todo Web API

This is a simple Todo Web API built with ASP.NET Core, .NET 8.

## Prerequisites

Before running the project, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

1. **Clone the repository**
   ```sh
   git clone https://github.com/mr2079/ToDo.git
   cd WebAPI
   ```

2. **Restore dependencies**
   ```sh
   dotnet restore
   ```

3. **Configure the database**
   - Open `appsettings.Development.json` and update the `ConnectionStrings.SqlServer` section with your database connection string.

4. **Run the application**
   ```sh
   dotnet run
   ```

5. **Access the API**
   - The API will be available at: `https://localhost:5104`
   - Swagger documentation can be accessed at: `https://localhost:5104/swagger`
