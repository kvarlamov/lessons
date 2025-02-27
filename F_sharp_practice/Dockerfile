FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["F_sharp_practice/F_sharp_practice.fsproj", "F_sharp_practice/"]
RUN dotnet restore "F_sharp_practice/F_sharp_practice.fsproj"
COPY . .
WORKDIR "/src/F_sharp_practice"
RUN dotnet build "F_sharp_practice.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "F_sharp_practice.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F_sharp_practice.dll"]
