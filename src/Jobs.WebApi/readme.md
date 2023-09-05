dotnet ef migrations add "AddJobEntity" -s Jobs.WebApi\Jobs.WebApi.csproj -p
Jobs.Infrastructure\Jobs.Infrastructure.csproj --output-dir "EntityFramework/Migrations"
dotnet ef migrations remove -s Jobs.WebApi\Jobs.WebApi.csproj -p Jobs.Infrastructure\Jobs.Infrastructure.csproj