# Altairis Backoffice

**Plataforma de gestión operativa hotelera y administración de reservas.**

Altairis Backoffice es una aplicación Full Stack diseñada para centralizar las operaciones de gestión hotelera, proporcionando una interfaz unificada para administrar información de hoteles, tipos de habitaciones, inventario diario y reservas.

El proyecto está enfocado en la mantenibilidad, la separación de responsabilidades, el despliegue mediante contenedores y una arquitectura preparada para futuras extensiones funcionales.

## Tecnologías

| Capa | Tecnologías |
|---|---|
| Frontend | Angular, TypeScript, Bootstrap |
| Backend | C#, .NET 10, ASP.NET Core Web API |
| Arquitectura | Clean Architecture |
| ORM | Entity Framework Core |
| Base de datos | Microsoft SQL Server |
| Documentación API | OpenAPI / Swagger |
| Contenerización | Docker, Docker Compose |
| Control de versiones | Git |

## Arquitectura

El backend sigue un enfoque de Clean Architecture, separando la lógica de negocio, las responsabilidades de aplicación, la infraestructura y la capa de presentación mediante API.

```text
Altairis/
│
├── backend/
│   ├── Altairis.sln
│   └── src/
│       ├── Altairis.Api/             # API REST y punto de entrada
│       ├── Altairis.Application/     # Casos de uso y lógica de aplicación
│       ├── Altairis.Domain/          # Entidades y reglas de negocio
│       └── Altairis.Infrastructure/  # EF Core, persistencia y configuraciones
│
├── frontend/
│   └── altairis-web/                 # Aplicación Angular
│
├── docker-compose.yml
├── .env.example
└── .gitignore
```

### Responsabilidades de los proyectos del backend

- **Altairis.Domain:** Contiene las entidades principales, enumeraciones y reglas de negocio. No depende de componentes de infraestructura.
- **Altairis.Application:** Gestiona los servicios de aplicación y la orquestación de los casos de uso.
- **Altairis.Infrastructure:** Implementa el contexto de base de datos, las configuraciones de Entity Framework Core, las migraciones y los mecanismos de persistencia.
- **Altairis.Api:** Gestiona los endpoints HTTP, la inyección de dependencias, los middleware y el inicio de la aplicación.

## Funcionalidades principales

- **Gestión de hoteles:** Registro, consulta y búsqueda de hoteles.
- **Gestión de tipos de habitaciones:** Creación y administración de habitaciones asociadas a cada hotel.
- **Gestión de inventario:** Registro de disponibilidad diaria de habitaciones y consulta de inventario.
- **Gestión de reservas:** Registro, consulta y seguimiento de reservas.
- **Visión operativa:** Acceso centralizado a la información necesaria para la gestión diaria de las operaciones hoteleras.

## Instalación y ejecución

### Requisitos previos

Para ejecutar el proyecto se necesitan las siguientes herramientas:

- [Git](https://git-scm.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

La aplicación se ejecuta íntegramente mediante Docker Compose. No es necesario instalar localmente .NET SDK, Node.js, Angular CLI ni SQL Server para ejecutar el sistema completo.

### 1. Clonar el repositorio

```bash
git clone -b develop https://github.com/angelo-dlcr/altairis-backoffice.git
cd Altairis
```

Si el repositorio ya está clonado, cambiar a la rama de desarrollo:

```bash
git checkout develop
git pull origin develop
```

### 2. Configurar las variables de entorno

Crear un archivo `.env` en la raíz del repositorio utilizando `.env.example` como plantilla.

**Windows (PowerShell):**

```powershell
Copy-Item .env.example .env
```

**Linux / macOS:**

```bash
cp .env.example .env
```

Configurar las credenciales de SQL Server:

```env
MSSQL_SA_PASSWORD=Altairis_Dev2026!SqlServer
```

La contraseña debe cumplir los requisitos de complejidad establecidos por SQL Server.

### 3. Construir e iniciar la aplicación

Desde la raíz del repositorio, ejecutar:

```bash
docker compose up -d --build
```

Docker Compose construirá las imágenes de la aplicación, creará los contenedores necesarios e iniciará los servicios.

### 4. Acceder a la aplicación

| Servicio | URL |
|---|---|
| Frontend Angular | http://localhost:4200 |
| API ASP.NET Core | http://localhost:5212 |
| Swagger UI | http://localhost:5212/swagger |

Swagger UI estará disponible cuando la API se ejecute en el entorno Development y el middleware correspondiente esté habilitado.

## Base de datos y migraciones

La aplicación utiliza Entity Framework Core con Microsoft SQL Server como proveedor de base de datos relacional.

La gestión del esquema de la base de datos se realiza mediante migraciones de Entity Framework Core bajo el enfoque Code First.

Las migraciones pendientes se aplican automáticamente durante el inicio de la API mediante:

```csharp
await dbContext.Database.MigrateAsync();
```

Este mecanismo permite inicializar y actualizar el esquema de la base de datos sin necesidad de ejecutar comandos de migración manualmente durante el despliegue.

Los datos de SQL Server se almacenan mediante un volumen persistente de Docker, evitando su pérdida al recrear los contenedores.

## Servicios Docker

El archivo `docker-compose.yml` orquesta los siguientes servicios:

| Contenedor | Descripción | Puerto interno | Puerto expuesto |
|---|---|---:|---:|
| `altairis-frontend` | Aplicación Angular | 4200 | 4200 |
| `altairis-api` | API ASP.NET Core | 5212 | 5212 |
| `altairis-sqlserver` | Microsoft SQL Server | 1433 | 14330 |

La disponibilidad de SQL Server se controla mediante un *healthcheck* de Docker. La API depende de que el servicio de base de datos alcance un estado saludable antes de iniciar.

## Desarrollo y mantenimiento

### Consultar el estado de los contenedores

```bash
docker compose ps
```

### Consultar los registros de la aplicación

```bash
docker compose logs -f
```

Consultar los registros de un servicio específico:

```bash
docker compose logs api --tail 100
docker compose logs frontend --tail 100
docker compose logs sqlserver --tail 100
```

### Detener la aplicación

```bash
docker compose down
```

### Reconstruir e iniciar los servicios

```bash
docker compose up -d --build
```

### Persistencia de la base de datos

Los archivos de la base de datos se almacenan en el volumen Docker `altairis_sqlserver_data`.

Para detener la aplicación conservando los datos:

```bash
docker compose down
```

Para eliminar los contenedores, las redes y los volúmenes de la base de datos:

```bash
docker compose down -v
```

**Advertencia:** La eliminación de los volúmenes implica la pérdida permanente de los datos almacenados en SQL Server.

## Consideraciones técnicas

- Separación de responsabilidades mediante principios de Clean Architecture.
- Inyección de dependencias para los servicios de aplicación e infraestructura.
- Persistencia de datos mediante Entity Framework Core Code First.
- Modelado relacional utilizando Microsoft SQL Server.
- Entorno de desarrollo y despliegue contenerizado.
- Configuración mediante variables de entorno para información sensible.
- Inicialización y actualización automática del esquema de base de datos.
- Persistencia de información mediante volúmenes Docker.

## Licencia

Proyecto desarrollado por Angelo De La Cruz como demostración técnica para FDSA.
