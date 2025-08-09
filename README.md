# Student Management System with External SQL Server Database

I've updated the application to use an external SQL Server database instead of the local database. Here's how to set up and use an external SQL Server database with this application:

## 1. Set Up an External SQL Server Database

You have several options for setting up an external SQL Server database:

### Option A: Use Azure SQL Database
1. Go to the [Azure Portal](https://portal.azure.com)
2. Create a new SQL Database resource
3. Configure your server, database name, and admin credentials
4. Make note of the server name, database name, username, and password

### Option B: Use a Self-Hosted SQL Server
1. Install SQL Server on your server (SQL Server Express, Developer, or Standard/Enterprise edition)
2. Configure SQL Server to allow remote connections
3. Create a new database named "StudentManagementDB"
4. Create a SQL login with appropriate permissions to the database

## 2. Update the Connection String

I've already updated the connection string in the `appsettings.json` file to use an external database. You need to replace the placeholder values with your actual SQL Server details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-98OL0T9\\SQLEXPRESS;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

Replace:
- `replace-server-name` with your SQL Server name or IP address
<!-- if your creating the server with windows authentication method then username and password not required. -->
- `your-username` with your SQL Server username
- `your-password` with your SQL Server password

For Azure SQL Database, the server name will be something like: `yourservername.database.windows.net`

## 3. Apply Migrations to Create the Database Schema

Once you've updated the connection string, you need to apply the migrations to create the database schema:

1. Open a command prompt or terminal
2. Navigate to the project directory: `cd C:\Users\<user_name>\Desktop\DevOps_Class\Project SQL\studentmanagmentsystem`
3. Run the following command to apply the migrations:


Installed the dotnet tool dotnet ef
```
dotnet tool install --global dotnet-ef --version 9.*
```

now run the dotnet ef for the project 
```
dotnet ef database update --project studentmanagmentsystem
```

This will create all the necessary tables in your external database and seed the initial data.

## 4. Run the Application

After applying the migrations, you can run the application with the external database:

```
dotnet run
```

The application will now connect to your external SQL Server database instead of the local database.

## Database Schema

The application uses the following database schema:

1. **Students Table**:
   - StudentId (Primary Key)
   - FirstName
   - LastName
   - DateOfBirth
   - Email
   - PhoneNumber
   - Address
   - EnrollmentDate

2. **Courses Table**:
   - CourseId (Primary Key)
   - Title
   - CourseCode
   - Credits
   - Description
   - Department

3. **Enrollments Table**:
   - EnrollmentId (Primary Key)
   - StudentId (Foreign Key)
   - CourseId (Foreign Key)
   - Grade
   - EnrollmentDate

The database includes seed data for 2 students, 3 courses, and 4 enrollments to get you started.

## Additional Configuration

If you need to customize the database configuration further, you can modify:

1. The `StudentDbContext.cs` file to change the data model
2. The `Program.cs` file to change how the database is configured
3. Create new migrations if you change the data model: `dotnet ef migrations add YourMigrationName`


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

# Student Management System

I've successfully created a Student Management System web application using ASP.NET Core and MS SQL Server. Here's a summary of what has been accomplished:

## Key Features

1. __Database Management__:

   - SQL Server database with tables for Students, Courses, and Enrollments
   - Local database configuration using SQL Server LocalDB
   - Database schema with proper relationships between entities

2. __Core Functionality__:

   - Student management (add, view, edit, delete)
   - Course management (add, view, edit, delete)
   - Enrollment management (add, view, edit, delete)
   - Grade tracking for student enrollments

3. __User Interface__:

   - Dashboard with summary statistics
   - Responsive design using Bootstrap
   - Intuitive navigation between different sections
   - Search functionality for students, courses, and enrollments

## Technical Implementation

1. __Backend__:

   - ASP.NET Core MVC architecture
   - Entity Framework Core for database operations
   - Code-first approach with migrations
   - Seed data for testing and demonstration

2. __Frontend__:

   - Razor views with Bootstrap styling
   - Form validation
   - Responsive layout for different screen sizes

## Project Structure

The solution has been renamed from "cline.sln" to "studentmanagmentsystem.sln" as requested.

The application uses the following connection string for the local database:

```javascript
Server=(localdb)\\mssqllocaldb;Database=StudentManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true
```

## Running the Application

To run the application:

1. Ensure SQL Server LocalDB is installed and running

2. Open a command prompt in the project directory

3. Run the following command to apply migrations (if not already applied):

   ```javascript
   dotnet ef database update
   ```

4. Run the application:

   ```javascript
   dotnet run --project StudentManagementSystem
   ```

5. Open a browser and navigate to [](http://localhost:5175)<http://localhost:5175>

The Student Management System provides a complete solution for managing students, courses, and enrollments in an educational institution.



Task 1:

1) CLone the repo
2) in local sql server>  update the appsettings.json file with your local sqlserver name eg: LAPTOP-9GJQ6815\\SQLEXPRESS 
  "ConnectionStrings": {
    "DefaultConnection": "Server=LAPTOP-9GJQ6815\\SQLEXPRESS;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
3) dotnet tool install --global dotnet-ef --version 9.*
4) dotnet ef database update  >> after completion verify in the sql whether db and the table have created
5) dotnet run --project StudentManagementSystem >> http://localhost:5175
6) dotnet publish
7) goto iis create website and give the publish folder path of the artifact
8) give permission to IIS App pool in the sql server
9) iis folder permission for the publish forlder
10) update web.config file with below code , first compare the file 
```
    <?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\StudentManagementSystem.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess">
        <environmentVariables>
          <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Development" />
        </environmentVariables>
      </aspNetCore>
    </system.webServer>
  </location>
</configuration>
```

11) browse and test the application.

Task 2:

1) Create a new azure VM 
2) Istall IIS
3) Install Sql server
4) perform oprerations from Task 


-------------------
my notes for reference
server name: DESKTOP-98OL0T9\SQLEXPRESS

webconfig
<aspNetCore processPath="dotnet" arguments=".\StudentManagementSystem.dll" stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />