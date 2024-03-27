# ATEC WEBMES VERSION 2
 
 <!-- @format -->

## Package need to install

- Microsoft.EntityFrameworkCore 7.0.14
- Microsoft.EntityFrameWorkCore.SqlServer 7.0.14
- Microsoft.EntityFrameWorkCore.Design 7.0.14
- Microsoft.EntityFrameworkCore.Tools 7.0.14 (for commands)
- AutoMapper.Extensions.Microsoft.DependencyInjection 11.0.0
- BCrypt.Net-Core 1.6.0
- Microsoft.Data.SqlClient 5.2.0
- Dapper 2.1.35
  
# Dapper Installation in PMC
 - Install Dapper from Nuget
 - Install SQL Client from Nuget

 ## In Package Manager
 ``````bash

 PM> Install-Package Dapper

 ``````

  ``````bash

 PM> Install-Package Microsoft.Data.SqlClient

 ``````


## Script for DB First Migration

```bash
-Scaffold-DbContext "Server=DESKTOP-92T9PSH;Database=DatabaseName;User Id=ServerUserName;Password=ServerPassword;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ScaffoldContextModel -f
```

## Script for DB First Migration for selected Table

```bash
-Scaffold-DbContext "Server=DESKTOP-92T9PSH;Database=DatabaseName;User Id=ServerUserName;Password=ServerPassword;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ScaffoldContextModel -f
-table table1 , table2
```

## Securing your Connection String

- include this to appsettings.json

  ### Code

  ```bash
  "ConnectionStrings": {
  "DefaultConnection": ""Server=DESKTOP-92T9PSH;Database=DatabaseName;User Id=ServerUserName;Password=ServerPassword;TrustServerCertificate=True;"
   },
  ```

- in your context inject IConfifuration
  ### Code
        private readonly IConfiguration _configuration;

```bash
          public StudentContext(DbContextOptions<Your Context here> options, IConfiguration configuration)
              : base(options)
          {
              _configuration = configuration;
          }

          #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
           => optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
```

## In Program.cs

- add your context before build

### Code

     #### Register DB CONTEXT
     builder.Services.AddDbContext<Your Context>();

     or

     builder.Services.AddDbContext<BookContext>(options =>
      {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });

     var app = builder.Build();

# DEVELOPMENT

### Intitial Migration

```bash
 -add-migration <Message>
```

### Update Database

```bash
-update-database
```

### if you want to Roll Back

```bash
-dotnet ef database update <PreviousMigrationName>
```
