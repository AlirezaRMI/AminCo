
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Web/Web.csproj", "Web/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "Web/Web.csproj"

COPY . .

WORKDIR "/Web"
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Web.dll"]