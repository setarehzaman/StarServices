


🛠️ StarServices Platform   ------->      StarServices.ir

A full-featured platform that connects clients with expert professionals for various home service needs. Designed with scalability, modularity, and real-world usability in mind.

🌟 Features :

👤 Customer : 

•	Registration

•	Edit profile

•	Order registration

•	View and search orders

•	View details of each order

•	View the offers provided for each order

•	View details of each suggestion

•	Display the comments of the expert offering the offer

•	Accept one of the suggestions



🧑‍🔧 Expert :

•	Registration

•	Edit profile

•	Add new skill to profile

•	Search the list of customer orders based on skills

•	View details of each order

•	Submit an offer to an order

•	View details of each offer




🛡️ Admin :

•	Access to admin dashboard

•	View reports in dashboard

•	Register users (Customer, Expert, and Admin)

•	Manage users

•	Manage orders

•	Manage suggestions

•	Manage comments

•	Manage home services

⚙️ Background Services


Hangfire Integration: For background job processing (e.g., scheduling, email/SMS reminders)

SMS Sending Service: Used for order status notifications


🧱 Architecture : 

Pattern:  Onion Architecture

Layers:

•	Domain

•	Infrastructure

•	Endpoint (Web/UI)

Designed for clear separation of concerns and high testability.



💻 Technologies : 

•	Framework: .NET 9 (ASP.NET Core Razor Pages)

•	Authentication: Cookie-based Auth

•	Database: SQL Server

•	ORM: Entity Framework Core

•	Background Jobs: Hangfire

•	Messaging: SMS Service Integration

•	Frontend: HTML, CSS (Bootstrap)

•	Design Patterns: Repository Pattern, Service Layer

