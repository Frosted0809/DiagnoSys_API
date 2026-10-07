
# 🏥 DiagnoSys API

A robust, relational backend RESTful API built with **ASP.NET Core 8**, designed to manage a clinical laboratory system. It handles core data operations for patients, laboratory test orders, medical test results (CBC, Urinalysis, Fecalysis), and billing/payments.

This project serves as the backend foundation, providing structured, validated, and relational data endpoints that a frontend application can consume.

---

## 🛠️ Technology Stack

* **Framework:** ASP.NET Core Web API (.NET 8)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Database-First Approach)
* **Object Mapping:** AutoMapper (Clean separation between DB models and API responses)
* **API Documentation:** Swagger / OpenAPI
* **IDE:** Visual Studio 2026

---

## 📂 Project Structure

The project follows a clean, layered architecture for maintainability:

* **`Controllers/`**: Contains the API endpoints (GET, POST, PUT, DELETE) for each entity.
* **`DTOs/`**: Data Transfer Objects organized by domain (Users, Patients, LabTests, Results, Payments). Defines exactly what data the API accepts and returns.
* **`Models/`** *(or `Data/`)*: Auto-generated C# classes mapping directly to the SQL Server database tables via EF Core.
* **`Mapping/`**: Contains the AutoMapper profile for seamless conversion between Models and DTOs.

---

## 🚀 Setup & Installation

Follow these steps to get the API running locally on your machine:

### 1. Clone the Repository
```bash
git clone https://github.com/YOUR_USERNAME/DiagnoSys_API.git
cd DiagnoSys_APIvm4.md…]()

# 🏥 DiagnoSys API

A robust, relational backend RESTful API built with **ASP.NET Core 8**, designed to manage a clinical laboratory system. It handles core data operations for patients, laboratory test orders, medical test results (CBC, Urinalysis, Fecalysis), and billing/payments.

This project serves as the backend foundation, providing structured, validated, and relational data endpoints that a frontend application can consume.

---

## 🛠️ Technology Stack

* **Framework:** ASP.NET Core Web API (.NET 8)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Database-First Approach)
* **Object Mapping:** AutoMapper (Clean separation between DB models and API responses)
* **API Documentation:** Swagger / OpenAPI
* **IDE:** Visual Studio 2026

---

## 📂 Project Structure

The project follows a clean, layered architecture for maintainability:

* **`Controllers/`**: Contains the API endpoints (GET, POST, PUT, DELETE) for each entity.
* **`DTOs/`**: Data Transfer Objects organized by domain (Users, Patients, LabTests, Results, Payments). Defines exactly what data the API accepts and returns.
* **`Models/`** *(or `Data/`)*: Auto-generated C# classes mapping directly to the SQL Server database tables via EF Core.
* **`Mapping/`**: Contains the AutoMapper profile for seamless conversion between Models and DTOs.

---

## 🚀 Setup & Installation

Follow these steps to get the API running locally on your machine:

### 1. Clone the Repository
```bash
git clone https://github.com/YOUR_USERNAME/DiagnoSys_API.git
cd DiagnoSys_API

2. Database Setup
Open SQL Server Management Studio (SSMS) and connect to your local instance (e.g., (localdb)\MSSQLLocalDB).
Open and execute the DiagnoSys_Schema_SqlServer.sql script to create the DiagnoSysDB database and all 8 tables.
Open and execute the DiagnoSys_Seed_SqlServer.sql script to populate lookup tables with sample data.

3. Configure Connection String
Open the project in Visual Studio.
Open appsettings.json.
Update the DefaultConnection string to match your local SQL Server instance:
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DiagnoSysDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }

4. Run the Application
Restore NuGet packages (Visual Studio usually does this automatically on build).
Press F5 or click the Play button to run the API.
Your browser will automatically open to the Swagger UI (https://localhost:<port>/swagger), where you can interactively test all endpoints.
```


<h1>API Endpoints overview </h1>

| Controller | Route | Description |
| :--- | :--- | :--- |
| **Users** | `/api/users` | Manage system users (Admin, Staff, Patient). |
| **ClinicPatients** | `/api/clinicpatients` | Register and manage patient demographics. |
| **LabTestCatalog** | `/api/labtestcatalog` | View available lab tests and their standard prices. |
| **LabTests** | `/api/labtests` | Create and manage lab test orders (Links Patient + Test). |
| **Cbc** | `/api/cbc` | Record and retrieve Complete Blood Count results. |
| **Urinalysi** | `/api/urinalysi` | Record and retrieve Urinalysis results. |
| **Fecalysi** | `/api/fecalysi` | Record and retrieve Fecalysis results. |
| **Payments** | `/api/payments` | Process partial or full payments for lab orders. |

<h1>🧪 Recommended Testing Workflow </h1>

To see the relational power of the API, test this real-world clinic scenario in Swagger or Postman:
POST /api/clinicpatients → Create a new patient (e.g., PatientId: "PAT-999").
POST /api/labtests → Create an order for that patient (e.g., testId: 1). Note the returned orderId.
POST /api/cbc → Add CBC results linked to that orderId. (Try posting twice to see the 409 Conflict protection!)
POST /api/payments → Add a payment linked to that orderId.
GET /api/payments/{id} → View the payment. Notice how the JSON response beautifully nests the Order details, Patient name, and Test name all in one response!


<h1>⚠️ Important Notes for Developers </h1>

Password Storage: For the scope of this simplified backend, user passwords are stored as plain text. In a production environment, this must be upgraded to use BCrypt or ASP.NET Core Identity hashing.
Validation: The API heavily utilizes Data Annotations ([Required], [EmailAddress], [RegularExpression]). Invalid payloads will return a 400 Bad Request with specific error messages.
Foreign Key Protection: Controllers actively validate that referenced IDs (like PatientId or OrderId) exist before saving, preventing orphaned records and database crashes.
Git Ignore: The bin/, obj/, and .vs/ folders are excluded via .gitignore. Visual Studio will automatically regenerate these when you build the project on your machine.




