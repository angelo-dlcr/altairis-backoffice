# Altairis

Sistema de gestión hotelera desarrollado como una aplicación web full-stack, con arquitectura basada en .NET, Angular y SQL Server.

## Tecnologías

### Backend
- C# / .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Frontend
- Angular 22
- TypeScript
- HTML / CSS

### Infraestructura
- Docker
- Docker Compose

## Arquitectura

El proyecto está organizado en los siguientes módulos:

- `Altairis.Api`: API REST.
- `Altairis.Application`: lógica de aplicación y casos de uso.
- `Altairis.Domain`: entidades y reglas de negocio.
- `Altairis.Infrastructure`: persistencia y acceso a datos.
- `frontend/altairis-web`: aplicación Angular.

## Requisitos

- Docker Desktop.
- Docker Compose.
- Git.

## Configuración

1. Clonar el repositorio:

   ```bash
   git clone URL_DEL_REPOSITORIO
   cd Altairis
   ```

2. Crear el archivo de configuración:

   ```bash
   cp .env.example .env
   ```

   En Windows PowerShell:

   ```powershell
   Copy-Item .env.example .env
   ```

3. Configurar la contraseña de SQL Server en `.env`.

4. Levantar los servicios:

   ```bash
   docker compose up -d --build
   ```

## Servicios

| Servicio | Dirección |
|---|---|
| Frontend | http://localhost:4200 |
| Backend API | http://localhost:5212 |
| Swagger | http://localhost:5212/swagger |
| SQL Server | localhost,14330 |

## Detener los servicios

```bash
docker compose down
```

Para eliminar también los datos persistidos de SQL Server:

```bash
docker compose down -v
```

**Advertencia:** el último comando elimina el volumen de datos de la base de datos.

## Estado del proyecto

En desarrollo.
