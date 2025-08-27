# ASP.NET User Management API Demo

This project is a **demo User Management API** built with **ASP.NET Core Web API**.  

The API supports **CRUD operations** for managing users and demonstrates how to implement **custom middleware** for:  
- ✅ Request/response logging  
- ✅ Centralized error handling  
- ✅ Token-based authentication (`Authorization: Bearer dummy-token`)  

---

## 🚀 Features
- **User CRUD Endpoints** (Create, Read, Update, Delete)  
- **Input validation** with `[Required]`, `[MaxLength]`, `[EmailAddress]`  
- **In-memory repository** (no external DB required)  
- **Middleware Pipeline**:  
  1. Global Error Handling  
  2. Authentication  
  3. Request/Response Logging  

---

## 🛠️ Tech Stack
- **.NET 9 / ASP.NET Core Web API**  
- **Minimal Hosting Model (`Program.cs`)**  

---

## ▶️ Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)

### Clone & Run
```bash
git clone https://github.com/adamt-eng/aspnet-user-management-api-demo.git
cd aspnet-user-management-api-demo
dotnet run
````

---

## 📡 API Endpoints

### Authentication

All requests require a **Bearer token** in the header:

```
Authorization: Bearer dummy-token
```

### Example Requests (HTTP file for VS Code REST Client)

```http
### Create User
POST http://localhost:5200/api/users
Authorization: Bearer dummy-token
Content-Type: application/json

{
  "firstName": "Layla",
  "lastName": "Hassan",
  "email": "layla.hassan@techhive.local",
  "department": "HR",
  "role": "Coordinator",
  "isActive": true
}

### List Users
GET http://localhost:5200/api/users
Authorization: Bearer dummy-token
```

More test cases can be found in the [`UserManagementAPI.http`](./UserManagementAPI.http) file.

---

## 🧪 Testing the Middleware

* **Valid Token** → Requests succeed
* **Missing/Invalid Token** → `401 Unauthorized`
* **Invalid Input** → `400 Bad Request`
* **Non-existent User** → `404 Not Found`
* **Server Exception** → `500 Internal Server Error` (JSON response)

---

## ⚠️ Disclaimer

This is a **demo project** and **not intended for production use**.

* Token validation uses a static dummy token (`dummy-token`)
* Data is stored in-memory and resets on each run

