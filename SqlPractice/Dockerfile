FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SqlPractice/SqlPractice.csproj", "SqlPractice/"]
RUN dotnet restore "SqlPractice/SqlPractice.csproj"
COPY . .
WORKDIR "/src/SqlPractice"
RUN dotnet build "SqlPractice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SqlPractice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SqlPractice.dll"]
