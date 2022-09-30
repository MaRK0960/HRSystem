Technologies used:
Visual Studio 2022
ASP.NET Core MVC 6
EF Core 6
SQL Server LocalDB (Generated on build with code first approach in EF)

Notes:
* Data Annotation is used for Model validation and Fluent API is used when more complex schema is required.
* Database is deleted and created with sample data on every build, comment the concerned block in Main to stop this behaviour.
* Models/DBModels contains the models that correspond to the database.
* Models/BusinessModels is for models used in the application business logic.
* Logs are assumed to be written by third party system, like a gate with card reader or fingerprint.