# UKG BE ASSIGNMENT - HCM

## About the project

This is Human Capital Management Application (HCM) application developed for the UKG .NET Software Engineer assignment.
The application manages employee records with role-based access control.

Project includes:
	- Role authentication (HRAdmin, Manager, Employee)
	- Managing people(Create, View, Update, Delete)
	- Unit and Integration tests
	- UI using Razor pages


## Technologies

-ASP.NET Core 8.0
-Entity Framework Core (In-Memory)
-xUnit for unit/integration tests
-Razor Pages


## Roles

Employee - Can view their own profile only
Manager - Can view and edit all people in the department
HRAdmin - Can manage all people records


## How to run the Project

1. Open the solution "HCM_Project.sln" in Visual Studio
2. Right-click on "People.API" -> Set Auth.API, People.API, People.Web as "Startup projects"
3. Start the project (Ctrl + F5).


## Users

Username		Password		Role		Department 
petar			12345			HRAdmin		HR
martin			12345			Manager		IT
stefan			12345			Employee	IT


## Unit Tests:

- Validation logic (e.g. salary, required fields)
- Role-based logic (HR can create only)


## Integration Tests:

- 'POST /api/people' (HRAdmin can create person)
- 'PUT /api/people{id}' (Manager can update within department)


## Web Interface

The interface is created with Razor Pages UI.
- Login page
- Dashboard per role
- People list
- Create/Edit/Delete forms
- Logout and back navigation buttons

## Notes
- The project uses an 'in-memory database', so no external setup is required.
- Authentication is based on mock system with a static list of predefined users.
- Newly created users cannot log in fia the login form.
"# HCM_Project" 
