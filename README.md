
# FrutiLogic Platform API #

FrutiLogic Platform API Application is made with Microsoft C#, ASP.NET Core, Entity Framework Core and MongoDB persistence. It also illustrates open-api documentation configuration and integration with Swagger UI.

## Summary ##

This application contains the following key features:

- Inventory management tailored for liquor stores.
- Real-time tracking of stock levels.
- Automated alerts for low stock items.
- Sales order management for liquor products.
- Supplier management and order tracking.
- Efficient management of liquor stock across multiple warehouses.
- Integration with suppliers for seamless order management.
- Support for multiple warehouses and multiple products.

## Technologies Used ##

For the development of the FrutiLogic Platform API, the following technologies and tools were utilized:

- **Microsoft C#**: The primary programming language used for developing the backend of the application.
- **ASP.NET Core**: The web framework used to build the API, providing a robust and scalable architecture.
- **Entity Framework Core**: An Object-Relational Mapper (ORM) used for database interactions, simplifying data access and manipulation.
- **MongoDB**: A NoSQL database used for storing inventory data, providing flexibility and scalability.
- **Swagger UI**: For API documentation and testing, allowing developers to easily explore and interact with the API endpoints.
- **OpenAPI**: For defining the API specifications, ensuring clear and consistent communication between the frontend and backend.
- **Domain-Driven Design (DDD)**: To structure the application around the core business domain, enhancing maintainability and scalability.

The application also incorporates several advanced features to enhance its functionality and user experience:

- **Asynchronous Programming**: To improve performance and responsiveness of the API.
- **Unit Testing**: To ensure code quality and reliability through automated tests.
- **Cloud Deployment**: Hosting the application on cloud platforms for scalability and availability on Azure.
- **Error Handling**: Robust mechanisms to handle exceptions and provide meaningful error messages.
- **API Versioning**: To manage changes and updates to the API without disrupting existing clients.
- **Documentation**: Comprehensive documentation for developers and users to understand the API functionalities and usage.

## Documentation

FrutiLogic Platform API includes its own documentation and it's available in the `docs` folder. It includes the following documentation:

- User Stories: related user stories to backend development. Is available in [docs/user-stories](docs/user-stories.md).
- C4 Model Software Architecture Diagram: illustrating the architecture of the application. Is available in [docs/software-architecture.dsl](docs/software-architecture.dsl).
- Class Diagram: illustrating the main classes and relationships in the application. Is available in [docs/class-diagram.puml](docs/class-diagram.puml).

## Bounded Context Divided System ##

This version of FrutiLogic Platform is divided into seven bounded contexts: Authentication, Profile Management, Payments and Subscriptions, Inventory Management, Alerts and Notifications, Procurement Ordering and Order Monitoring.

### Authentication Context

The Authentication Context is responsible for registering of new users and authentication of them in order to login in the application. Also, it handles the management of user roles (admin, liquor store owners, workers and providers).
This context includes the following features:

- User registration and login (sign-in and sign-up).
- Role-based access control.
- Password management (reset, change).
- Token-based authentication with JWT.

### Profile Management Context

The Profile Management Context is responsible for the capability of showing the information of the registered user and the ability of modify the information (name, email, location).
This context includes the following features:

- User profile creation and updating.
- Profile picture management.
- View and edit personal information.
- Manage user settings and preferences.

### Payments and Subscriptions Context

This context handles payment processing and subscription management, allowing users to manage their billing and subscription plans along with their accounts. It includes the following features:

- Creation of a new account with a subscription plan.
- Management of existing subscription plans (upgrade, downgrade, cancel).
- Payment processing for subscriptions with PayPal and MercadoPago.

### Inventory Management Context

This context focuses on managing product stock across multiple warehouses, allowing users to track stock levels, add new products, and manage inventory efficiently. It includes the following features:

- Warehouse management (create new warehouse, update information and delete empty warehouses).
- Product management (register new products, update information and delete zero-stock products).
- Stock tracking (view current stock levels and update stock).
- Product exits and entries management.
- Product expiration date management.
- Product care guides management.
- Product trasnferal between warehouses.
- Catalog management for liquor providers (view available products and search products).

### Alerts and Notification Context

This context manages real-time stock updates and notifications, ensuring users are always informed about their inventory status. It includes the following features:

- Stock level alerts.
- Expiration date notifications.
- The creation of alerts for low stock levels.

### Procurement Ordering Context

This context provides the capability of generating new orders to suppliers when stock levels are low, ensuring that liquor stores can maintain optimal inventory levels. It includes the following features:

- Cart management (add products to cart, view cart, update quantities, remove products).
- Order creation (place orders to suppliers).
- Order history (view past orders and their statuses).
- Order status tracking (view current status of orders).

### Order Monitoring Context

This context focuses on managing orders and operations, allowing liquor suppliers to track orders and ensure smooth operations. It includes the following features:

For suppliers:

- Supplier management (view orders, update order status).
- Order tracking and history.
- Inventory management (view and manage available products).