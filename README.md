# 📦 Finbit API - Backend

### 🔧 Technologies Used
- ASP.NET 8 (Minimal hosting model)
- EF Core + SQLite (real persistence and migrations)
- JWT Authentication with roles (`Admin` and `User`)
- Swagger (API documentation)
- CORS enabled for Angular (`http://localhost:4200`)
- xUnit + Moq (unit testing)

---

### 🧱 General Architecture
Structured using **layered MVC**, separating responsibilities:

```
Finbit.Api/
├── Controllers/
├── Models/
├── Data/
├── Services/
│   └── Interfaces/
├── Repositories/
│   └── Interfaces/
├── Auth/
│   ├── JwtTokenService.cs
│   ├── Models/
│   └── Interfaces/
```

🔁 Implements **Repository + Service** pattern, decoupling business logic and enabling effective unit testing.

---

### 🔐 JWT Authentication

- Real authentication using signed JWT tokens.
- Tokens include `username` and `role`.
- **Mocked login** for quick development and testing (`admin/1234`, `user/1234`).
- Available roles: `User`, `Admin`.

**Token is required to access any secure endpoint.**

---

### 👤 Roles and Authorization

#### 👤 End User (`User`)
- ✅ View their own transactions
- ✅ Create new transactions
- 🚫 Cannot view or delete other users' transactions

#### 👑 Administrator (`Admin`)
- ✅ View all transactions
- ✅ Delete any transaction

---

### 📘 Available Endpoints

| Endpoint                  | Method | Required Role | Description                                   |
|--------------------------|--------|----------------|-----------------------------------------------|
| `/api/Auth/login`        | POST   | Public         | Authentication (mock login)                   |
| `/api/Transaction/mine`  | GET    | `User`         | View personal transactions                    |
| `/api/Transaction`       | POST   | `User`         | Create personal transaction                   |
| `/api/Transaction/all`   | GET    | `Admin`        | View all transactions                         |
| `/api/Transaction/{id}`  | DELETE | `Admin`        | Delete a specific transaction                 |

---

### 🧪 Unit Testing (Finbit.Tests)

✅ A functional unit test is included using `xUnit` and `Moq`.

- **Test**: `CreateAsync_ShouldReturnCreatedTransaction`
- **Coverage**: successful service flow when creating a transaction
- **Mock Used**: simulated `ITransactionRepository`
- **Expected Result**: service returns the inserted data correctly

---

### 🧑‍🔬 Technical Decision Justification

- Prioritized a **clean and decoupled structure**, ready for scaling or real user integration.
- Although login is mocked, the system **generates and validates real JWT tokens** with embedded roles.
- Used `SQLite` for real persistence with EF Core and generated the initial migration.
- Seed data added in `Program.cs` for controlled testing.
- Role usage clearly separates permissions between users and admins.

---

### ⚠️ Final Notes

> The Angular frontend integration was not completed due to time constraints from my professional schedule. However, the backend exposes a fully functional JWT-authenticated API, ready for seamless HTTP integration.