<div align="center">

# 🛒 E-Commerce API

**A RESTful e-commerce backend in ASP.NET Core 8, built with Clean Architecture: products, Redis basket, JWT identity, orders and Stripe payments.**

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-8-6DB33F?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white)
![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)

</div>

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [API Endpoints](#-api-endpoints)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Learning Journey](#-learning-journey)
- [Author](#-author)

---

## 🌟 Overview

**E-Commerce API** is the backend for an online store. It exposes REST endpoints for the product catalog, shopping basket, customer accounts, orders and payments, so any frontend (Angular, React, mobile) can use it.

The project was built step by step, one module per session. **Each module lives on its own branch**, and the [`session-08`](../../tree/session-08) branch has the complete project. See [Learning Journey](#-learning-journey).

---

## ✨ Features

| Module | What it does |
|---|---|
| 🛍️ **Products** | List products, get one by id, list brands and types; filtering, searching, sorting and pagination through the **Specification pattern** |
| 🧺 **Basket** | Customer basket stored in **Redis** (create/update, get, delete) |
| 🔐 **Identity** | Register, log in, check if an email exists, get the current user, get/update address; secured with **JWT** |
| 📦 **Orders** | Create an order from a basket, list my orders, get one order, list delivery methods |
| 💳 **Payments** | Create or update a **Stripe** payment intent for a basket |

**Under the hood**

- ✅ **Clean Architecture** (Domain, Application, Infrastructure, API)
- ✅ Generic Repository + **Unit of Work**
- ✅ **Specification pattern** for reusable queries
- ✅ **Result pattern** for consistent responses and errors
- ✅ **AutoMapper** with a custom resolver for full picture URLs
- ✅ **Data seeding** from JSON files (brands, types, products)
- ✅ Separate **Identity database**
- ✅ **Swagger** documentation

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | .NET 8, ASP.NET Core Web API |
| **ORM** | Entity Framework Core 8 (Code-First + Migrations) |
| **Database** | SQL Server (store + identity) |
| **Cache** | Redis (`StackExchange.Redis`) for the basket |
| **Auth** | ASP.NET Core Identity + JWT Bearer |
| **Payments** | Stripe (`Stripe.net`) |
| **Mapping** | AutoMapper |
| **Docs** | Swagger (Swashbuckle) |

---

## 🏛 Architecture

```mermaid
flowchart TD
    C([Client app]) --> API

    subgraph API["🌐 E_Commerce.API"]
        CT[Controllers] --> EXT[Extensions / Middleware]
    end

    subgraph APP["⚙️ E_Commerce.Application"]
        S[Services] --> DTO[DTOs]
        S --> MAP[AutoMapper Profiles]
        S --> SPEC[Specifications]
    end

    subgraph DOM["🧩 E_Commerce.Domain"]
        M[Entities] --- CON[Contracts]
    end

    subgraph INF["🗄️ E_Commerce.Infrastructure"]
        R[Repositories + Unit of Work] --> CTX[DbContexts]
        BR[Basket Repository]
        SVC[Auth / Token / Order / Payment services]
    end

    API --> APP
    API --> INF
    APP --> DOM
    INF --> APP
    INF --> DOM
    CTX --> SQL[(SQL Server)]
    BR --> RD[(Redis)]
    SVC --> ST[(Stripe)]
```

---

## 🔌 API Endpoints

All routes start with `/api`. 🔒 means a JWT token is required.

<details open>
<summary><b>Products</b>: <code>/api/Product</code></summary>

| Method | Route | Description |
|---|---|---|
| GET | `/api/Product` | List products (filter, search, sort, paginate) |
| GET | `/api/Product/{id}` | Get one product |
| GET | `/api/Product/Brands` | List brands |
| GET | `/api/Product/Types` | List types |

</details>

<details>
<summary><b>Basket</b>: <code>/api/Baskets</code></summary>

| Method | Route | Description |
|---|---|---|
| GET | `/api/Baskets` | Get a basket |
| POST | `/api/Baskets` | Create or update a basket |
| DELETE | `/api/Baskets` | Delete a basket |

</details>

<details>
<summary><b>Authentication</b>: <code>/api/Authentication</code></summary>

| Method | Route | Description |
|---|---|---|
| POST | `/api/Authentication/Login` | Log in and get a token |
| POST | `/api/Authentication/Register` | Create an account |
| GET | `/api/Authentication/emailexists` | Check if an email is taken |
| GET 🔒 | `/api/Authentication/CurrentUser` | Get the logged-in user |
| GET 🔒 | `/api/Authentication/Address` | Get my address |
| PUT 🔒 | `/api/Authentication/Address` | Update my address |

</details>

<details>
<summary><b>Orders</b>: <code>/api/Orders</code> 🔒</summary>

| Method | Route | Description |
|---|---|---|
| POST | `/api/Orders` | Create an order |
| GET | `/api/Orders` | List my orders |
| GET | `/api/Orders/{id}` | Get one order |
| GET | `/api/Orders/deliveryMethods` | List delivery methods |

</details>

<details>
<summary><b>Payments</b>: <code>/api/Payment</code> 🔒</summary>

| Method | Route | Description |
|---|---|---|
| POST | `/api/Payment/{basketId}` | Create or update the Stripe payment intent |

</details>

---

## 📂 Project Structure

```
E_Commerce/
├── E_Commerce.slnx
├── E_Commerce.API/              # 🌐 Controllers, Program.cs, extensions
├── E_Commerce.Application/      # ⚙️ Services, DTOs, AutoMapper profiles, specifications
├── E_Commerce.Domain/           # 🧩 Entities and contracts (no dependencies)
└── E_Commerce.Infrastructure/   # 🗄️ EF Core, repositories, Redis, Identity, Stripe, seeding
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server
- Redis (from session 04 onward), e.g. `docker run -d -p 6379:6379 redis`
- A [Stripe](https://stripe.com) test account (session 08)

### Run

1. **Clone the repository and switch to the complete version**
   ```bash
   git clone https://github.com/ahmedramzyelsheikh/E_Commerce.git
   cd E_Commerce
   git checkout session-08
   ```

2. **Configure `E_Commerce.API/appsettings.json`**: `DefaultConnection`, `IdentityConnection`, `Redis`, the JWT `Token` section and `StripeSettings`.

3. **Apply the migrations**
   ```bash
   dotnet ef database update --project E_Commerce.Infrastructure --startup-project E_Commerce.API
   ```

4. **Run the API**
   ```bash
   dotnet run --project E_Commerce.API
   ```

5. Open **Swagger** at `https://localhost:<port>/swagger`.

---

## 🧭 Learning Journey

Each branch adds one module on top of the previous one:

| Branch | Milestone |
|---|---|
| `main` | 🏗️ Clean Architecture setup + Products module (repository, unit of work, seeding, Result pattern) |
| `session-03API` | 🔎 Specification pattern + picture URL resolver |
| `session-04` | 🧺 Basket module with **Redis** + sorting and pagination specifications |
| `session-05` | 🔐 **Identity**: register, login, JWT, address |
| `session-07` | 📦 **Orders** module with delivery methods |
| `session-08` | 💳 **Stripe payments** (complete project) |

---

## 👨‍💻 Author

<div align="center">

**Ahmed Ramzy**

[![GitHub](https://img.shields.io/badge/GitHub-ahmedramzyelsheikh-181717?style=for-the-badge&logo=github)](https://github.com/ahmedramzyelsheikh)

⭐ If you like this project, give it a star!

</div>
