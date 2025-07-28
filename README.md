# Restaurant Order API

A RESTful API for managing restaurant menus and orders, built with ASP.NET Core.

## Features

- Menu Management
  - Create daily menus
  - Update menu items
  - Delete menus
  - Retrieve menus by date
- Order Management
  - Create orders from daily menus
  - Retrieve order details

## Technology Stack

- .NET 9.0
- ASP.NET Core
- C# 13.0
- Swagger/OpenAPI for documentation
- In-memory repositories for data storage

## Getting Started

### Prerequisites

- .NET SDK 9.0 or later
- An IDE (e.g., Visual Studio, JetBrains Rider)

### Installation

1. Clone the repository:
bash git clone [repository-url]

2. Navigate to the project directory:
bash cd RestaurantOrderApi

3. Build the project:
bash dotnet build

4. Run the application:
bash dotnet run

The API will be available at `https://localhost:7212` (HTTPS) or `http://localhost:5114` (HTTP).

## API Documentation

The API documentation is available via Swagger UI when running in development mode. Access it at:
[http://localhost:5114/swagger](http://localhost:5114/swagger)

### Endpoints

#### Menus

- `GET /menus/{date}` - Get menu for a specific date
- `POST /menus` - Create a new menu
- `PUT /menus/{date}` - Update an existing menu
- `DELETE /menus/{date}` - Delete a menu

#### Orders

- `GET /order/{id}` - Get order by ID
- `POST /order` - Create a new order

## API Usage Examples

### Creating a Menu

http POST /menus Content-Type: application/json

`{
  "date": "2025-07-28",
  "menuItems": [
    {
      "name": "Pepperoni Pizza",
      "description": "Pizza with pepperoni",
      "price": 400
    }
  ]
}`

### Creating an Order

http POST /order Content-Type: application/json

`{
  "date": "2025-07-28",
  "itemNames": [
    "Pepperoni Pizza"
  ]
}`

## Development

### Running Tests

bash dotnet test
