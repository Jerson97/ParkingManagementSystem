# CleanTemplate API (.NET)

Plantilla base para el desarrollo de APIs en ASP.NET Core siguiendo los principios de Clean Architecture.

Incluye:

- .NET 8
- MediatR (implementación de CQRS)
- Middleware personalizado para manejo global de excepciones
- Swagger (OpenAPI)
- Respuesta estandarizada mediante MessageResult<T>
- Arquitectura desacoplada de la tecnología de persistencia

---

## 🏗 Arquitectura

La solución está organizada en las siguientes capas:

- CleanTemplate.Domain  
- CleanTemplate.Application  
- CleanTemplate.Infrastructure  
- CleanTemplate.WebApi  

La capa Application define las interfaces y la lógica de negocio.  
La capa Infrastructure puede implementar cualquier tecnología de persistencia (Entity Framework, Dapper, etc.) sin modificar la lógica principal.

El template no está acoplado a ninguna tecnología específica de base de datos.

---

## 🚀 Características Incluidas

- Implementación del patrón CQRS con MediatR
- Middleware global para manejo de excepciones
- Clase ApiException para errores controlados
- Respuesta estandarizada para todas las peticiones (MessageResult<T>)
- BaseController con acceso centralizado a IMediator
- Swagger configurado y listo para integración con JWT
- Endpoint de prueba para validación del pipeline

---

## 🧪 Endpoint de Prueba

GET /api/health/ping

Respuesta esperada:

```json
{
  "code": 1,
  "message": "OK",
  "data": "Pong"
}
