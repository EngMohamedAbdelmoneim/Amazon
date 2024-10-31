# 🛒 Amazon Clone - Basic E-commerce Platform

This project is a basic Amazon clone that includes essential e-commerce features like user authentication, product listings, shopping cart functionality, checkout with Stripe, and a simple Seller Central account for vendor management.

## 📌 Features
- **User Authentication**: Account registration and login.
- **Product Listings**: Category browsing, basic search, and filtering.
- **Product Details**: Product description, images, and essential information.
- **Shopping Cart**: Add and manage items.
- **Checkout & Payments**: Secure checkout with Stripe.
- **Order Tracking**: Order status and history.
- **Admin Dashboard**: Basic user, product, and order management.
- **Seller Central Account**: Seller operations, and some informations for seller sales.
- **Responsive Design**: Mobile-friendly UI.

## ⚙️ Tech Stack
- **Frontend**: Angular
- **Backend**: 
  - **Admin**: .NET MVC
  - **Website API**: ASP.NET
- **Database**: SQL Server with Redis for caching
- **Payments**: Stripe API for payment processing

---

## 🚀 Getting Started

### Prerequisites
- **Node.js** and **npm**
- **Angular CLI** for the frontend
- **.NET SDK** for backend services
- **SQL Server** and **Redis** installed locally or on a server
- **Stripe** account for API keys

🛠️ Project Structure

/Amazon.Solution
├── AdminWebApplication/      # .NET MVC project for Admin Dashboard
├── Amazon.API/               # ASP.NET API for backend services
├── Amazon.Core/              # Core logic and business models
├── Amazon.Services/          # Service layer for business operations
├── Amazon.Infrastructure/    # Infrastructure layer, handling data access
└── README.md                 # Project documentation
