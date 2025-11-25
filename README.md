# CreditosChevrolet – API de respuestas de crédito automotriz

API REST en .NET para recibir, procesar y consultar respuestas de crédito enviadas por financieras externas, integrándose con un ERP de concesionaria.

## Tecnologías

- .NET 10 (ASP.NET Core Web API)
- C#
- Entity Framework Core + SQL Server
- AutoMapper
- Swagger / OpenAPI

## Requerimiento resuelto

La API implementa:

- `POST /api/creditos/respuesta`
  Recibe la respuesta de la financiera para una solicitud de crédito, valida reglas de negocio, registra la respuesta y genera una notificación al asesor.

- `GET /api/creditos/respuesta/{numeroSolicitud}`
  Devuelve la trazabilidad de las respuestas de crédito para una solicitud, incluyendo información de reintento cuando el estado está en proceso.

### Reglas de negocio

- La solicitud debe existir en `SolicitudesCredito` (se valida por `NumeroSolicitud`).
- Estados permitidos: `APROBADO`, `NEGADO`, `CONDICIONADO`, `REQUIERE_DOCUMENTOS`, `EN_PROCESO`.
- Si el estado es `APROBADO`:
  - `montoAprobado` y `plazoMeses` son obligatorios.
- Si el estado es `NEGADO`, `CONDICIONADO` o `REQUIERE_DOCUMENTOS`:
  - `observacion` es obligatoria.
- Si el estado es `EN_PROCESO`:
  - Se utiliza `MinutosReintento` de la tabla `SolicitudesCredito` para calcular `ProximaConsulta` en la respuesta del GET.
- Se genera una notificación en `NotificacionesAsesor` para cada respuesta procesada.

## Estructura principal del proyecto

- `Data/ApplicationDbContext.cs`
  Contexto de EF Core y definición de DbSet:

  - `SolicitudesCredito`
  - `RespuestasCredito`
  - `NotificacionesAsesor`

- `Models/`
  Entidades de dominio y DTOs.

- `Repository/` y `Repository/IRepository/`
  Repositorios para acceso a datos:

  - `ISolicitudCreditoRepository`
  - `IRespuestaCreditoFinancieraRepository`
  - `INotificacionAsesorRepository`

- `Services/` y `Services/Interfaces/`
  Capa de servicio de dominio:

  - `ICreditoService`
  - `CreditoService`

- `Controllers/CreditosController.cs`
  Controller con los endpoints `POST` y `GET`.

## 1. Configuración y Ejecucion de la API

### Cadena de conexión, debes cambiar con tu {User} y tu {Password}

En `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CreditosChevrolet;User ID=SA;Password=123456;TrustServerCertificate=true;MultipleActiveResultSets=true"
}
```

## 2. Base de datos

### Opcion A) Correr migraciones (No tiene registros)

```json
  dotnet ef database update
```

### Opcion B) Usando script SQL (Esquema con registros prueba)

```json
  sql/01_schema_y_datos.sql
```

## 3. Ejecución de la API

```json
  dotnet run
```

### La API expone Swagger en:

```bash
  http://localhost:5032/swagger/index.html

```

## 4. Ejemplos de prueba

### Datos de prueba rápidos (SQL)

```sql
USE CreditosChevroletDb;
GO

-- Solicitudes de crédito de ejemplo
INSERT INTO SolicitudesCredito (NumeroSolicitud, AsesorId, FechaSolicitud, FinancieraId, MinutosReintento)
VALUES
('SOL-APROBADO-001',      1001, GETDATE(), NULL, 30),
('SOL-NEGADO-001',        1002, GETDATE(), NULL, 30),
('SOL-CONDICIONADO-001',  1003, GETDATE(), NULL, 30),
('SOL-REQDOC-001',        1004, GETDATE(), NULL, 30),
('SOL-PROCESO-001',       1005, GETDATE(), NULL, 30);

-- Solicitud extra para pruebas libres
INSERT INTO SolicitudesCredito (NumeroSolicitud, AsesorId, FechaSolicitud, FinancieraId, MinutosReintento)
VALUES ('SOL-TEST-001', 1010, GETDATE(), NULL, 20);

-- Verificar datos insertados
SELECT * FROM SolicitudesCredito;
```

## 5. Endpoints

### A) Crear respuesta aprobada

```
POST /api/creditos/respuesta
```

Body

```
  {
    "numeroSolicitud": "SOL-APROBADO-001",
    "estado": "APROBADO",
    "montoAprobado": 14500,
    "plazoMeses": 60,
    "tasa": 15.5,
    "observacion": "Cliente califica sin restricciones",
    "fechaRespuesta": "2025-07-10T10:25:00"
  }

```

Respuesta

```
{
  "mensaje": "Respuesta procesada correctamente."
}

```

### B) Consultar trazabilidad de una solicitud

```
GET /api/creditos/respuesta/SOL-APROBADO-001

```

Respuesta

```
  {
  "numeroSolicitud": "SOL-APROBADO-001",
  "asesorId": 1001,
  "fechaSolicitud": "2025-11-24T00:00:00",
  "minutosReintento": 30,
  "proximaConsulta": null,
  "respuestas": [
    {
      "estado": "APROBADO",
      "montoAprobado": 14500,
      "plazoMeses": 60,
      "tasa": 15.5,
      "fechaRespuesta": "2025-07-10T10:25:00",
      "observaciones": "Cliente califica sin restricciones",
      "jsonCompleto": "{ ... }"
    }
  ]
}


```

## 7. Notificación por correo (Gmail)

### Haz un POST válido (por ejemplo con SOL-APROBADO-001).

## 7. Logs en la terminal

### En la misma terminal donde corre el `dotner run` aparecen los logs de los resultados de las ejecuciones.
