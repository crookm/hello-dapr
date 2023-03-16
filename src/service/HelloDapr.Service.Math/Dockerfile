FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/HelloDapr.Contracts/HelloDapr.Contracts.csproj", "src/HelloDapr.Contracts/"]
COPY ["src/service/HelloDapr.Service.Math/HelloDapr.Service.Math.csproj", "src/service/HelloDapr.Service.Math/"]
RUN dotnet restore "src/service/HelloDapr.Service.Math/HelloDapr.Service.Math.csproj"

COPY . .
WORKDIR "/src/src/service/HelloDapr.Service.Math"

RUN dotnet build "HelloDapr.Service.Math.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloDapr.Service.Math.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDapr.Service.Math.dll"]
