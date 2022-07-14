FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers

WORKDIR /src
COPY ["Producer/Producer.csproj", "Producer/"]
COPY ["Resto.Common/Resto.Common.csproj", "Resto.Common/"]

RUN dotnet restore "Producer/Producer.csproj"
COPY . .
WORKDIR "/src/Producer"

# Copy everything else and build
RUN dotnet publish "Producer.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Producer.dll"]