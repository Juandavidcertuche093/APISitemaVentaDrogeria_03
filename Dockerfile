# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los .csproj
COPY ["SistemaVenta.API/SistemaVenta.API.csproj", "SistemaVenta.API/"]
COPY ["SistemaVenta.BLL/SistemaVenta.BLL.csproj", "SistemaVenta.BLL/"]
COPY ["SistemaVenta.DAL/SistemaVenta.DAL.csproj", "SistemaVenta.DAL/"]
COPY ["SistemaVenta.DTO/SistemaVenta.DTO.csproj", "SistemaVenta.DTO/"]
COPY ["SistemaVenta.IOC/SistemaVenta.IOC.csproj", "SistemaVenta.IOC/"]
COPY ["SistemaVenta.Model/SistemaVenta.Model.csproj", "SistemaVenta.Model/"]
COPY ["SistemaVenta.Utility/SistemaVenta.Utility.csproj", "SistemaVenta.Utility/"]

# Restaurar dependencias
RUN dotnet restore "SistemaVenta.API/SistemaVenta.API.csproj"

# Copiar el resto del código fuente
COPY . .

# Publicar
WORKDIR "/src/SistemaVenta.API"
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
