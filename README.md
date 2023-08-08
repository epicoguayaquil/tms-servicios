# tms-servicios

## ORM Configuración 

* Con  para la configuración de los entities se genera con el siguiente comando: 

    **Code:**

    Scaffold-DbContext "Data Source=db.cloudtek.ec;Database=TMS_DB;uid=sa;pwd=DeveloperDB1;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -force

Scaffold-DbContext "Data Source=db.cloudtek.ec;Database=Datec_DB;uid=sa;pwd=DeveloperDB1.;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -force

## Detalles del desarrollo
- Microsoft Visual Studio Community 2022 (64 bits) - Current  - Versión 17.4.1
- C#
- Navicat 16.1.2 - para manejo del modelo de base de datos
- SQL Server 15.00.2095