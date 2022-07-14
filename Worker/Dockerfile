FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ["Worker/Worker.csproj", "Worker/"]
COPY ["Lib/RabbitMQ.Helper.dll", "RabbitMQ.Helper/"]
RUN dotnet restore "Worker/Worker.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Worker"
RUN dotnet publish "Worker.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Worker.dll"]