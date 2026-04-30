# 🏋️ Gym Management System - ASP.NET Core MVC

A full-featured Gym Management System built using **ASP.NET Core MVC** following clean architecture principles and best practices.

---

## 🚀 Overview

This project is a web application designed to manage gym operations including:

* 👤 Members Management
* 🧑‍🏫 Trainers Management
* 📋 Plans & Subscriptions
* 🏋️ Sessions Scheduling
* 📊 Analytics Dashboard

The system is built with a scalable and maintainable architecture using **3-Layer Architecture + MVC Pattern**.

---

## 🧠 Architecture

### 🔹 MVC Pattern

* **Model** → Represents application data
* **View** → User Interface (Razor صفحات)
* **Controller** → Handles requests and responses

---

### 🔹 3-Layer Architecture

1. **Presentation Layer (UI)**

   * ASP.NET Core MVC
   * Controllers + Views

2. **Business Logic Layer (BLL)**

   * Services
   * Business rules & validation

3. **Data Access Layer (DAL)**

   * Repositories
   * Entity Framework Core

---

## 🧱 Design Patterns Used

* ✅ Repository Pattern
* ✅ Generic Repository
* ✅ Unit of Work
* ✅ Dependency Injection
* ✅ AutoMapper



## 🌐 Features

### 👤 Members

* Get all members
* View member details
* Create / Update / Delete member
* Manage health records

---

### 🧑‍🏫 Trainers

* Manage trainers
* View trainer details

---

### 📋 Plans

* Create and manage subscription plans
* Activate / Deactivate plans

---

### 🏋️ Sessions

* Schedule sessions
* Assign trainers
* Manage capacity & availability

---

### 📊 Analytics

* Total Members
* Active Members
* Trainers count
* Sessions statistics

---

### 📎 File Handling

* Upload images/files
* Delete attachments
* Unique file naming using GUID

---

## 🛠️ Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* AutoMapper
* LINQ

---




## 📂 Project Structure

```
GymManagementSystem/
│
├── Presentation (MVC)
├── BusinessLogic (Services)
├── DataAccess (Repositories, DbContext)
├── Models
└── wwwroot
```


## 👨‍💻 Author

Developed by **[Mohamed Gomaa]**
