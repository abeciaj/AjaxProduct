# DotNet Crud Test

A simple ASP.NET Core MVC web application to manage products (Create, Read, Update, Delete) using Entity Framework Core with SQL Server.  

---

## Features

- **Add Product** – Add new products to the database.  
- **View Products** – List all products with ID, Name, and Price.  
- **Update Product** – Update existing product details.  
- **Delete Product** – Remove products from the database.  
- **Simple UI** – Minimalistic and functional user interface.  

---

## Nuget Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json

## Debugging

###Insert null error
- Make sure the ProductModel has Id set as the primary key with auto-increment:
'''
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // <-- auto-increment
public int Id { get; set; }
'''
- Make sure the database column Id is IDENTITY:
'''
CREATE TABLE tblProduct
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price FLOAT NOT NULL
);
'''
