FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TheoryFunc1/TheoryFunc1.csproj", "TheoryFunc1/"]
RUN dotnet restore "TheoryFunc1/TheoryFunc1.csproj"
COPY . .
WORKDIR "/src/TheoryFunc1"
RUN dotnet build "TheoryFunc1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheoryFunc1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheoryFunc1.dll"]
