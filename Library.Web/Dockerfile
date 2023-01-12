FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Library.Web/Library.Web.csproj", "Library.Web/"]
RUN dotnet restore "Library.Web/Library.Web.csproj"
COPY . .
WORKDIR "/src/Library.Web"
RUN dotnet build "Library.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Web.dll"]
