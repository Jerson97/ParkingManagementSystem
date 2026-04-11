# 🚗 Parking Management System

Sistema backend desarrollado para la gestión de una cochera, enfocado en el control de ingresos de vehículos, manejo de abonados y cálculo de tarifas según el tipo de servicio.

El objetivo del proyecto fue implementar una solución estructurada que permita registrar operaciones diarias de manera ordenada, manteniendo trazabilidad de cada ingreso (ticket) y control sobre los espacios disponibles.

## 🧩 Tecnologías utilizadas

* .NET
* Entity Framework Core
* SQL Server
* Clean Architecture

## ⚙️ Funcionalidades principales

* Registro de vehículos
* Generación de tickets al momento de ingreso
* Control de espacios de estacionamiento
* Gestión de tarifas (diario, semanal, mensual)
* Manejo de abonados con espacio asignado
* Cálculo de montos al momento de salida
* Control de estados (ingreso, salida, pago)

## 🧠 Enfoque técnico

El sistema fue estructurado utilizando Clean Architecture, separando responsabilidades en distintas capas:

* **Domain**: entidades y reglas principales del negocio
* **Application**: lógica de aplicación y casos de uso
* **Infrastructure**: acceso a datos con Entity Framework Core
* **WebApi**: exposición de endpoints

Se utilizó Fluent API para la configuración de entidades y relaciones, evitando dependencias en atributos y permitiendo mayor control sobre la base de datos.

Además, se implementó el uso de enums para el manejo de estados, almacenándolos como texto en la base de datos para mejorar la legibilidad y mantenimiento.

## 📌 Consideraciones

El sistema está diseñado para manejar tanto clientes ocasionales como abonados, permitiendo asignación de espacios fijos y control de disponibilidad en tiempo real.

Actualmente el proyecto se encuentra en evolución, con mejoras planificadas en validaciones, lógica de negocio y ampliación de funcionalidades.

---

Proyecto desarrollado como solución backend orientada a escenarios reales de gestión de cocheras.
