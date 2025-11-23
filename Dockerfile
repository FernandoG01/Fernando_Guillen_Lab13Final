# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar todo el proyecto
COPY . ./

# Restaurar dependencias
RUN dotnet restore

# Publicar en modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar archivos publicados
COPY --from=build /app/publish .

# Exponer el puerto (Render usa 10000 o el PORT env)
EXPOSE 8080

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "Fernando_Guillen_Lab13Final.dll"]
