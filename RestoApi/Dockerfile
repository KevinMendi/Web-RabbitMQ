FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers

WORKDIR /src
COPY ["RestoApi/RestoApi.csproj", "RestoApi/"]
COPY ["Resto.Common/Resto.Common.csproj", "Resto.Common/"]
COPY ["Lib/RabbitMQ.Helper.dll", "RabbitMQ.Helper/"]
RUN dotnet restore "RestoApi/RestoApi.csproj"
COPY . .
WORKDIR "/src/RestoApi"

# Copy everything else and build
RUN dotnet publish "RestoApi.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "RestoApi.dll", "--urls", "http://*:5000"]