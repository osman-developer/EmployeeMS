# Employee Management System

## Overview

The **Employee Management System** is a project built using **.NET 8.0 & Angular 19.0.6**, following a clean layered architecture. This design ensures maintainability, scalability, and separation of concerns, making it easy to extend and modify in the future. The project implements a **Generic Repository** pattern, allowing for reusable data access logic across different services. The core components of the system are separated into distinct projects with specific responsibilities.

Additionally, the system allows users to **upload images**, **IDs**, and **attachments** related to employees, making it suitable for comprehensive employee data management.

## Project Structure

- **Domain**:
  - Contains the **Entities**, **DTOs** (Data Transfer Objects), **Interfaces**, and the **Pagination** structure.
  - The entities represent the core business objects like `Employee`, and the DTOs provide the structure for transferring data between layers.
  - The pagination structure allows for easy handling of data in a paginated manner.

- **Infrastructure**:
  - Contains the **Database Context**, configuration for **Database Tables**, and the **Implementation of the Generic Repository**.
  - This layer is responsible for interacting with the database, providing necessary CRUD operations, and ensuring that the data access layer is reusable across the application.
  - Includes support for uploading and storing employee **images**, **IDs**, and **attachments**.

- **Services**:
  - Contains the **Business Logic** and **Service Implementations**, including the **Filter Builder Service**.
  - The **Filter Builder Service** helps in filtering entities dynamically based on user input.
  - The services layer ensures the separation of business logic from data access.
  - Includes the functionality for managing and retrieving employee-related files (images, IDs, attachments).

- **Pagination**:
  - The pagination functionality is **generic** and can be easily implemented in any service that requires paginated results. The pagination is done on the level of the database to ensure optimal query performance.
    
- **Lazy Loading**:
  - The Angular app has the Lazy Loading feature in which the EmployeeCard and EmployeeDashboard are lazy loaded on demand.

- **Angular Pagination**:
  - The pagination functionality in the angular app is **generic** and can be easily implemented in any component that requires paginated results.

## Features

- **Clean Layered Architecture**: Ensures separation of concerns, making the system maintainable and easy to scale.
- **Generic Repository**: Implements reusable data access methods to minimize code duplication and maintain consistency.
- **Dynamic Filtering**: Allows easy filtering of entities with the help of the Filter Builder service.
- **Generic Pagination**: Can be implemented easily across all services, providing a flexible approach to paginate results.
- **File Upload Support**: Allows users to upload employee-related files such as images, identification documents, and other attachments.

## Requirements

- **.NET 8.0** or higher & **Angular 19.0.6** or Higher
- A database system (SQL Server, PostgreSQL, etc.) for storing employee data.
- File storage mechanism (local storage or cloud storage) for handling employee uploads.

## Getting Started

1. Clone the repository.
2. Set up the database and configure the connection string in the `appsettings.json` file.
3. Run the `update-database` command by heading into the Infrastructure project to make the Database.
4. Build and run the application using Visual Studio or the .NET CLI.
5. Navigate into the EmployeeMSClient app and run **npm install** to install the modules listed in the package.json file.

## Conclusion

This project is a robust, scalable, and easy-to-extend **Employee Management System** built with a modern .NET framework. With its clean architecture, reusable repository pattern, flexible pagination, and support for file uploads, it provides a comprehensive solution for managing employee data and associated documents.
