# DatabaseManagementForShop
- Author: Tomanová Vilma
- This project is console application based on creating, editing and deleting commissions.

## Requirements
- .NET SDK (latest version)
- Microsoft SQL Server Management Studio 20
- Requried NuGet packages
    - `Microsoft.Data.SqlClient`
    
## How to configure db
- Go into `config.json` in `build-db1`
- If you are using `Windows Authentification`, use this ->
  ```
  {"ConnectionString": "Server=localhost\\SQLEXPRESS;Database=shop;Trusted_Connection=True;TrustServerCertificate=True;"}
  ```
- If you are using `SQL server authentification`, use this ->
```
- {"ConnectionString": "Server=YOUR_SERVER_NAME;Database=shop;User Id=USER_EXAMPLE;Password=PASSWORD_EXAMPLE;TrustServerCertificate=True;"}
```

## Usage - database
1. Open MSSql Studio
2. Open `export.sql` and run

## Usage - importing data
If you want to import data into database through files, do these steps
1. Create two csv files in `build-db1` folder (you need to have two and correctly written or the code won´t work)
- There are already 2 csv (`artifacts.csv` and `races.csv`), you can edit data in them (recommended)
2. For importing, just turn on the program and write y

## Usage - program
- To create order, you must add a user



