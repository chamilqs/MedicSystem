### Proyecto Gestor de Pacientes
- **Objetivo General**: Crear un sistema gestor de pacientes con **ASP.NET Core 8 MVC** y arquitectura **ONION**.

- **Funcionalidades Principales**:
  - **Login y Registro**: Autenticación de usuarios y registro de administradores.
  - **Menú Principal**: Acceso a diferentes módulos de mantenimiento según el rol del usuario (Administrador o Asistente).
  - **Usuarios**: Gestión de usuarios con opciones para crear, editar y eliminar.
  - **Médicos**: Administración de médicos, incluyendo la carga de fotos y datos personales.
  - **Pruebas de Laboratorio**: Creación y edición de pruebas de laboratorio.
  - **Pacientes**: Registro y actualización de pacientes, con manejo de información médica relevante.
  - **Resultados de Laboratorio**: Registro y actualización de resultados de pruebas de laboratorio.
  - **Citas**: Programación y gestión de citas médicas.

### Tecnologías y Herramientas Utilizadas
- **ASP.NET Core 6, 7 o 8 MVC**: Para la construcción del backend y las vistas del sistema.
- **Arquitectura ONION**: Para garantizar la separación de intereses y la escalabilidad.
- **Entity Framework con Code First**: Para la persistencia de datos²[2].
- **Bootstrap**: Para el diseño responsivo y visual del sistema.
- **ViewModels**: Para la validación de datos y la lógica de presentación.
- **Repositorio y Servicio Genéricos**: Para abstraer la lógica de negocio y acceso a datos.
