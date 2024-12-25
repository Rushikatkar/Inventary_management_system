# Inventory Management System API

## Overview
The Inventory Management System API is designed to manage the company's inventory efficiently. It provides a set of endpoints to perform CRUD (Create, Read, Update, Delete) operations on inventory items, with features such as pagination, sorting, filtering, and alerts for low stock levels. The system uses token-based authentication and role-based authorization to secure sensitive operations.

## Features
1. **Core Functionality**:
   - Endpoints for CRUD operations on inventory items.
   - Pagination, sorting, and filtering for GET requests.
   - Low inventory alerts.

2. **Security**:
   - Token-based authentication (JWT).
   - Role-based authorization for sensitive endpoints.

3. **Performance**:
   - Optimized database queries using Entity Framework Core.
   - Caching of frequently accessed data (using Redis).

4. **Documentation**:
   - Swagger/OpenAPI for API documentation.


## API Flow
1. **Login**:
   - Users authenticate by providing their credentials.
   - Upon successful login, a JWT token is issued, which contains the user's `UserId`.

2. **Authentication & Authorization**:
   - Every request to protected endpoints requires a valid JWT token.
   - Role-based access control is implemented to restrict certain actions to specific roles (e.g., Admin, User).

3. **Inventory CRUD Operations**:
   - **Create**: Add new inventory items.
   - **Read**: View the list of inventory items with pagination, sorting, and filtering options.
   - **Update**: Update existing inventory items.
   - **Delete**: Remove inventory items from the system.

4. **Low Inventory Alerts**:
   - The system tracks the stock levels of inventory items.
   - When an item's stock reaches a low threshold, an alert is generated and logged.

## API Endpoints

### 1. **Login**
   - **POST /api/auth/login**
   - Request Body:
     ```json
     {
       "username": "user123",
       "password": "password123"
     }
     ```
   - Response:
     ```json
     {
       "token": "jwt_token_here",
       "userId": "123"
     }
     ```

### 2. **Inventory CRUD Operations**

#### Create Inventory Item
   - **POST /api/inventory**
   - Request Body:
     ```json
     {
       "name": "Product A",
       "description": "Description of Product A",
       "quantity": 100,
       "price": 10.50
     }
     ```
   - Response:
     ```json
     {
       "id": 1,
       "name": "Product A",
       "description": "Description of Product A",
       "quantity": 100,
       "price": 10.50
     }
     ```

#### Get Inventory Items
   - **GET /api/inventory**
   - Query Parameters:
     - `page`: Page number for pagination.
     - `limit`: Number of items per page.
     - `sort`: Field to sort by (e.g., name, price).
     - `filter`: Filter criteria (e.g., "low stock").
   - Response:
     ```json
     {
       "items": [
         {
           "id": 1,
           "name": "Product A",
           "quantity": 100,
           "price": 10.50
         }
       ],
       "totalItems": 1,
       "totalPages": 1
     }
     ```

#### Update Inventory Item
   - **PUT /api/inventory/{id}**
   - Request Body:
     ```json
     {
       "name": "Updated Product A",
       "description": "Updated description",
       "quantity": 120,
       "price": 12.00
     }
     ```
   - Response:
     ```json
     {
       "id": 1,
       "name": "Updated Product A",
       "description": "Updated description",
       "quantity": 120,
       "price": 12.00
     }
     ```

#### Delete Inventory Item
   - **DELETE /api/inventory/{id}**
   - Response:
     ```json
     {
       "message": "Inventory item deleted successfully"
     }
     ```

### 3. **Low Inventory Alerts**
   - When an inventory item's stock falls below a predefined threshold, an alert is generated and logged to a file. This file can be accessed by the system administrator or the user.

## Security

1. **Token-Based Authentication (JWT)**:
   - All sensitive API endpoints require a valid JWT token in the `Authorization` header.
   - Example:
     ```
     Authorization: Bearer <your_jwt_token>
     ```

2. **Role-Based Authorization**:
   - **Admin**: Can perform all operations (CRUD operations on inventory items, manage user roles).
   - **User**: Can only view inventory items and update quantities.

## Performance Optimization
1. **Entity Framework Core**:
   - Optimized queries to reduce the load on the database.
   - Pagination and filtering implemented to handle large data sets.

2. **Redis Caching**:
   - Frequently accessed data, such as inventory lists, are cached using Redis for improved performance.

## Unit Tests
- Unit tests have been written for all endpoints to ensure that the API behaves as expected.
- The tests cover positive and negative cases, including validation of input data and error handling.

## Swagger API Documentation
- Swagger is used to generate interactive API documentation. You can view and test all API endpoints directly through the Swagger UI by visiting the `/swagger` endpoint after deploying the application.

## Installation & Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/inventory-management-api.git
