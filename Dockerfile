# syntax=docker/dockerfile:1

# ===== Publish stage =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish
WORKDIR /src

# copy csproj first to leverage restore caching
COPY EventProRegistration/EventProRegistration.csproj EventProRegistration/
RUN dotnet restore EventProRegistration/EventProRegistration.csproj

# copy the rest
COPY . .
WORKDIR /src/EventProRegistration

# publish with analyzers off and warnings NOT treated as errors
RUN dotnet publish EventProRegistration.csproj -c Release -o /app/publish \
    -p:UseAppHost=false \
    -p:EnableNETAnalyzers=false \
    -p:AnalysisLevel=none \
    -p:TreatWarningsAsErrors=false

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Cloud Run listens on 8080 by default (and sets $PORT=8080)
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "EventProRegistration.dll"]
