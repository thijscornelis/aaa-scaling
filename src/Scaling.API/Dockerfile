#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Scaling.API/Scaling.API.csproj", "Scaling.API/"]
RUN dotnet restore "Scaling.API/Scaling.API.csproj"
COPY . .
WORKDIR "/src/Scaling.API"
RUN dotnet build "Scaling.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scaling.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scaling.API.dll"]