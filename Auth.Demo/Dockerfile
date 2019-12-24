FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Auth.Demo/Auth.Demo.csproj", "Auth.Demo/"]
RUN dotnet restore "Auth.Demo/Auth.Demo.csproj"
COPY . .
WORKDIR "/src/Auth.Demo"
RUN dotnet build "Auth.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Demo.dll"]