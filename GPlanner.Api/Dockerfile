
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY *.sln .
COPY GPlanner.Api/*.csproj GPlanner.Api/
COPY GPlanner.Core/*.csproj GPlanner.Core/
COPY GPlanner.Data/*.csproj GPlanner.Data/
COPY GPlanner.Maui/*.csproj GPlanner.Maui/  
RUN dotnet restore GPlanner.Api/GPlanner.Api.csproj
COPY . .
WORKDIR /src/GPlanner.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

EXPOSE 8080 

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GPlanner.Api.dll"]