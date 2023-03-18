FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoManagment.API/TodoManagment.API.csproj", "TodoManagment.API/"]
RUN dotnet restore "TodoManagment.API/TodoManagment.API.csproj"
COPY . .
WORKDIR "/src/TodoManagment.API"
RUN dotnet build "TodoManagment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoManagment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoManagment.API.dll"]