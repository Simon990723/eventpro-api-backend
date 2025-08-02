# Stage 1: Build the application
# This stage uses the full .NET SDK to build your project.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["EventProRegistration/EventProRegistration.csproj", "EventProRegistration/"]
RUN dotnet restore "EventProRegistration/EventProRegistration.csproj"
COPY . .
WORKDIR "/src/EventProRegistration"
RUN dotnet build "EventProRegistration.csproj" -c Release -o /app/build

# Stage 2: Publish the application
# This stage takes the built code and prepares it for deployment.
FROM build AS publish
RUN dotnet publish "EventProRegistration.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final runtime image
# This stage creates a smaller, more secure image with only the .NET runtime
# and your published application files.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
# The port ASP.NET Core will listen on inside the container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "EventProRegistration.dll"]