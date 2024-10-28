## Table Structure

The `dbo.User` table has the following structure:

- **Id** (INT, Primary Key, Identity): Unique identifier for each user.
- **Username** (VARCHAR(255), NOT NULL): Username of the user.
- **Password** (VARCHAR(255), NOT NULL): Password of the user.
- **FirstName** (VARCHAR(255), NOT NULL): First name of the user.
- **LastName** (VARCHAR(255), NOT NULL): Last name of the user.
- **Company** (VARCHAR(255), NOT NULL): Company associated with the user.
- **Role** (INT, NULL): Role identifier for the user, where values Admin = 1 and User = 2

## SQL Script
```sql
IF NOT EXISTS (
    SELECT 1 FROM sysobjects WHERE name = 'User' AND xtype = 'U'
)
BEGIN
    CREATE TABLE [dbo].[User]
    (
        [Id] INT NOT NULL IDENTITY PRIMARY KEY,
        [Username] VARCHAR(255) NOT NULL,
        [Password] VARCHAR(255) NOT NULL,
        [FirstName] VARCHAR(255) NOT NULL,
        [Lastname] VARCHAR(255) NOT NULL,
        [Company] VARCHAR(255) NOT NULL,
        [Role] INT NULL
    );
END;



-- Sample Data

INSERT INTO [dbo].[User] (Username, Password, FirstName, Lastname, Company, Role) VALUES
    ('john.doe', 'password123', 'John', 'Doe', 'Acme Corp', 1),
    ('jane.smith', 'password456', 'Jane', 'Smith', 'Acme Corp', 2),
    ('alice.johnson', 'password789', 'Alice', 'Johnson', 'Tech Innovations', 1),
    ('bob.brown', 'password101', 'Bob', 'Brown', 'Tech Innovations', 2),
    ('charlie.white', 'password202', 'Charlie', 'White', 'Global Solutions', 1),
    ('dave.green', 'password303', 'Dave', 'Green', 'Global Solutions', 2),
    ('eve.black', 'password404', 'Eve', 'Black', 'Acme Corp', 1),
    ('frank.gray', 'password505', 'Frank', 'Gray', 'Tech Innovations', 2),
    ('george.purple', 'password606', 'George', 'Purple', 'Global Solutions', 1),
    ('hannah.red', 'password707', 'Hannah', 'Red', 'Acme Corp', 2);
```
## Requirements

You’re developing the first stage of the API which will be further extended with more business logic. Your application will be deployed to the Staging environment once the work on stage 1 is completed.

Create a web API with a JWT authentication. It should allow to log in and list/create/update/delete company users. The companies are added outside of the API. Each user belongs to only one of the companies. Users can be Admins or flat Users.
- Admins can log in, list all users of the company and create/update/delete them.
- Flat users can just log in and list non-admin users of their company.

Also create a throttling middleware, that would disallow to make > 10 requests from 1 user per minute.

Please pay attention at the security and insulation of the company data.

### Technical stack:
- .NET 6 or higher
- Data access: Dapper.NET
- DB: MSSQL (data storage, simple modules for CRUD operations with filtering where needed)

### Sources to be provided:
- A git repository with the API solution
- A git repository with the DB solution