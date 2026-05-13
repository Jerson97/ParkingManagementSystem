# 🚗 Parking Management System

API REST desarrollada en **.NET 8** para la gestión operativa de una cochera.  
Permite administrar ingresos y salidas de vehículos, tickets, espacios, tarifas, abonados, autenticación y roles.

Este proyecto forma parte de un sistema completo compuesto por:

- **Backend:** ParkingManagementSystem
- **Frontend:** ParkingManagementSystemWeb

## 🧩 Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- JWT Authentication
- BCrypt
- Swagger / OpenAPI
- CORS

## ⚙️ Funcionalidades principales

- Autenticación con JWT.
- Roles de usuario: `Admin` y `Attendant`.
- Dashboard con resumen operativo.
- Registro de ingreso de vehículos.
- Generación de ticket de ingreso.
- Cálculo de monto estimado.
- Registro de salida de vehículos.
- Historial de tickets cerrados.
- Gestión de espacios de cochera.
- Gestión de tarifas.
- Gestión de abonados/suscripciones.

## 🧠 Enfoque técnico

El sistema fue estructurado utilizando Clean Architecture, separando responsabilidades en distintas capas:

ParkingManagementSystem.Domain
ParkingManagementSystem.Application
ParkingManagementSystem.Infrastructure
ParkingManagementSystem.WebApi

##
Además, se aplican patrones como:

CQRS
Mediator Pattern
Repository Pattern
Unit of Work
DTOs
Middleware global de errores
Respuestas estandarizadas con MessageResult<T>



👤 Roles del sistema
Admin:
Usuario administrativo con acceso a funcionalidades como:

Gestión de tarifas.
Cancelación de abonados.
Procesamiento de suscripciones vencidas.
Operaciones generales del sistema.

Attendant:
Usuario operativo encargado de la atención diaria de la cochera.

Puede realizar acciones como:

Registrar ingreso de vehículos.
Consultar monto estimado.
Registrar salida.
Buscar tickets.
Consultar historial.
Crear y renovar abonados.
Consultar espacios y tarifas.

📦 Módulos principales

## Parking Entries
Módulo encargado del flujo operativo de vehículos:

Registrar ingreso.
Generar ticket.
Consultar monto estimado.
Registrar salida.
Buscar ticket.
Consultar historial.

## Parking Spaces
Permite consultar el estado de los espacios:

Disponible.
Ocupado.
Reservado.

## Rate Types
Permite administrar tarifas:

Crear tarifa.
Editar tarifa.
Desactivar tarifa.
Listar tarifas activas.

## Subscriptions
Permite gestionar abonados:

Crear abonado.
Renovar suscripción.
Cancelar abonado.
Procesar suscripciones vencidas.
Liberar espacios asociados.