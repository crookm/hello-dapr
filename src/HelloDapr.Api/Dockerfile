FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/HelloDapr.Contracts/HelloDapr.Contracts.csproj", "src/HelloDapr.Contracts/"]
COPY ["src/HelloDapr.Api/HelloDapr.Api.csproj", "src/HelloDapr.Api/"]
RUN dotnet restore "src/HelloDapr.Api/HelloDapr.Api.csproj"

COPY . .
WORKDIR "/src/src/HelloDapr.Api"

RUN dotnet build "HelloDapr.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloDapr.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDapr.Api.dll"]
