# 🏗️ The Gate Web App
## 📊 Database Schema
![ERD](https://i.suar.me/npOlM/l)
## 📌 Project Overview
Built a **web application** to manage and display **categories, subcategories, and products**.  
The project follows **Clean Architecture** principles and implements:
- **MVC Pattern**
- **Repository Pattern**
- **Clean Code principles**
- **Entity Framework + LINQ** for efficient data access.
- **OTP (One Time Password)** for email confirmation during user registration.

---

## 🛠️ Technologies Used
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **LINQ**
- **C#**
- **SQL Server**
- **HTML, CSS, Bootstrap** (for views)

---

## 🏛️ Architecture (Clean Architecture)

The solution is divided into **4 layers**:

1. **Domain Layer**
   - Contains entities, DTOs, and business rules.
   - Independent from other layers.

2. **Application Layer**
   - Contains interfaces and services.
   - Defines the contracts for repositories and application logic.

3. **Persistence Layer**
   - Contains the implementation of repositories.
   - Responsible for database interactions using **Entity Framework**.

4. **Presentation Layer**
   - Contains the **ASP.NET Core MVC Controllers** and Views.
   - Handles UI and request/response cycle.

---

## 📂 Project Structure


---

## 🚀 Features

### 🔑 Admin
- Login / Logout
- Manage Admin Accounts (Add, Edit, Delete, View)
- Dashboard (Home Page)

### 📂 Category Management
- Add, Edit, Delete categories
- Upload category images
- Toggle Active/Inactive status

### 🗂️ SubCategory Management
- Add, Edit, Delete subcategories
- Upload images
- Linked to categories

### 🛒 Product Management
- Add, Edit, Delete products
- Upload product images
- Linked to categories & subcategories
- Price management

### 👤 User Management
- Register with Email & Password
- Login / Logout
- Email Confirmation using **OTP**
- Browse Categories, Subcategories, and Products

---

## 🔐 OTP Email Confirmation
When a user registers:
1. A **verification code** is sent to their email.
2. User must enter the OTP to activate their account.
3. Only confirmed users can log in.

---

## 📸 Screenshots (Optional)
_Add screenshots here if you want to show the UI._

---

## 📌 Future Improvements
- Add **JWT Authentication** for secure API access.
- Implement **Unit Tests** for Services & Controllers.
- Add **Role-Based Authorization** (Admin vs User).
- Improve UI with a modern frontend framework (e.g., Angular/React).

---

## ⚙️ How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/the-gate.git
👨‍💻 Author
Walid Gamal
Backend Developer | ASP.NET Core Enthusiast
