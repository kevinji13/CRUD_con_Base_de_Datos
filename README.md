# Proyecto de Gestión de Clientes, Bodegas y Productos

## Descripción
Este proyecto es una aplicación de escritorio desarrollada en **C# (.NET Framework 4.7.2)** que permite la gestión completa de **clientes, bodegas y productos**, incluyendo todas las operaciones de un **CRUD** (Crear, Leer, Actualizar y Eliminar).  

Cuenta con un **apartado de usuario y contraseña**, lo que permite controlar el acceso a la aplicación y mantener la seguridad de los datos.  

La aplicación se conecta a una **base de datos SQL Server**, utilizando **Entity Framework 6** para la gestión de datos de manera eficiente y segura.

---

## Funcionalidades principales

- Gestión de **Clientes**
  - Crear, leer, actualizar y eliminar clientes.
- Gestión de **Bodegas**
  - Crear, leer, actualizar y eliminar bodegas.
- Gestión de **Productos**
  - Crear, leer, actualizar y eliminar productos.
  - Configuración de estado (`Activo`/`Inactivo`) y parámetros como **IVA** y **PVP**.
- Gestión de **Usuarios**
  - Inicio de sesión mediante usuario y contraseña.
  - Control de acceso a la aplicación.
- Integración con **SQL Server**
  - Uso de **Entity Framework 6** para operaciones con la base de datos.
  - Configuración de conexión segura mediante `Integrated Security`.

---

## Tecnologías utilizadas

- **Lenguaje:** C#  
- **Plataforma:** .NET Framework 4.7.2  
- **Interfaz de usuario:** WinForms / WPF (dependiendo de la versión)  
- **Base de datos:** SQL Server  
- **ORM:** Entity Framework 6  

---

## Requisitos

- Visual Studio 2019 o superior  
- SQL Server (Express o versión completa)  
- .NET Framework 4.7.2  
- Permisos de lectura/escritura en la base de datos  
