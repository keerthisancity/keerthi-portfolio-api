# Portfolio API (ASP.NET Core, .NET 8)

ASP.NET Core Web API for a personal portfolio — exposes Bio, Projects, Skills, Experience, Education and Achievements. The API uses Entity Framework Core with SQL Server.

Tech stack
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- SQL Server

Repository layout
- PortfolioApi/ — API project
- deploy.ps1 — helper script to publish and zip-deploy to Azure Web App
- appsettings.Sample.json — sample configuration (do NOT store real credentials)
- db/schema.sql — SQL schema for creating the database tables

Prerequisites
- .NET 8 SDK
- SQL Server (local or Azure SQL)
- Azure CLI (if deploying to Azure)

Local development
1. Copy appsettings.Sample.json to appsettings.json OR set environment variable DEFAULTCONNECTION with your connection string.
   - Example (PowerShell):
	 $env:DEFAULTCONNECTION = 'Server=.;Database=PortfolioDb;Trusted_Connection=True;'

2. Restore and run:
   dotnet restore
   dotnet build
   dotnet run

3. Open Swagger UI to explore the API:
   https://localhost:44358/swagger/index.html

API endpoints (summary)
- GET /api/bio — Get the primary bio record (FullName, ShortName, Title, Email, AboutMe, etc.)
- GET /api/projects — Get list of projects
- GET /api/skills — Get list of skills
- GET /api/experience — Get work experience
- GET /api/education — Get education entries
- GET /api/achievements — Get achievements

Example curl (bio)
curl -s https://localhost:44358/api/bio | jq

Database schema
Below is the SQL to create the necessary tables used by the API. You can run this script against a new database (or generate migrations using EF Core).

-- db/schema.sql
-- Run this script in your SQL Server to create tables used by the API

CREATE TABLE Bio (
	BioID INT IDENTITY(1,1) PRIMARY KEY,
	FullName NVARCHAR(256) NULL,
	Title NVARCHAR(256) NULL,
	Email NVARCHAR(256) NULL,
	Phone NVARCHAR(50) NULL,
	ShortName NVARCHAR(50) NULL,
	AboutMe NVARCHAR(MAX) NULL,
	Summary NVARCHAR(MAX) NULL,
	LinkedIn NVARCHAR(512) NULL,
	GitHub NVARCHAR(512) NULL,
	PortfolioURL NVARCHAR(512) NULL,
	Location NVARCHAR(256) NULL
);

CREATE TABLE Projects (
	ProjectID INT IDENTITY(1,1) PRIMARY KEY,
	BioID INT NOT NULL,
	Title NVARCHAR(256) NULL,
	Description NVARCHAR(MAX) NULL,
	TechStack NVARCHAR(512) NULL,
	StartDate DATETIME2 NULL,
	EndDate DATETIME2 NULL,
	ProjectURL NVARCHAR(512) NULL,
	RepoURL NVARCHAR(512) NULL,
	CONSTRAINT FK_Projects_Bio FOREIGN KEY (BioID) REFERENCES Bio(BioID) ON DELETE CASCADE
);

CREATE TABLE Skills (
	SkillID INT IDENTITY(1,1) PRIMARY KEY,
	BioID INT NOT NULL,
	SkillName NVARCHAR(256) NULL,
	Category NVARCHAR(128) NULL,
	ProficiencyLevel NVARCHAR(128) NULL,
	CONSTRAINT FK_Skills_Bio FOREIGN KEY (BioID) REFERENCES Bio(BioID) ON DELETE CASCADE
);

CREATE TABLE Experience (
	ExperienceID INT IDENTITY(1,1) PRIMARY KEY,
	BioID INT NOT NULL,
	Company NVARCHAR(256) NULL,
	Role NVARCHAR(256) NULL,
	StartDate DATETIME2 NULL,
	EndDate DATETIME2 NULL,
	Responsibilities NVARCHAR(MAX) NULL,
	CONSTRAINT FK_Experience_Bio FOREIGN KEY (BioID) REFERENCES Bio(BioID) ON DELETE CASCADE
);

CREATE TABLE Education (
	EducationID INT IDENTITY(1,1) PRIMARY KEY,
	BioID INT NOT NULL,
	Institution NVARCHAR(256) NULL,
	Degree NVARCHAR(256) NULL,
	FieldOfStudy NVARCHAR(256) NULL,
	StartDate DATETIME2 NULL,
	EndDate DATETIME2 NULL,
	Description NVARCHAR(MAX) NULL,
	CONSTRAINT FK_Education_Bio FOREIGN KEY (BioID) REFERENCES Bio(BioID) ON DELETE CASCADE
);

CREATE TABLE Achievement (
	AchievementID INT IDENTITY(1,1) PRIMARY KEY,
	BioID INT NOT NULL,
	Title NVARCHAR(256) NULL,
	Issuer NVARCHAR(256) NULL,
	DateAchieved DATETIME2 NULL,
	Description NVARCHAR(MAX) NULL,
	CONSTRAINT FK_Achievement_Bio FOREIGN KEY (BioID) REFERENCES Bio(BioID) ON DELETE CASCADE
);

CREATE TABLE ProjectSkills (
	ProjectID INT NOT NULL,
	SkillID INT NOT NULL,
	CONSTRAINT PK_ProjectSkills PRIMARY KEY (ProjectID, SkillID),
	CONSTRAINT FK_ProjectSkills_Project FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
	CONSTRAINT FK_ProjectSkills_Skill FOREIGN KEY (SkillID) REFERENCES Skills(SkillID) ON DELETE CASCADE
);

How to add screenshots
- To include screenshots from your local Swagger UI (https://localhost:44358/swagger/index.html):
  1. Open the Swagger URL in your browser while the API is running.
  2. Take screenshots (Windows: Win+Shift+S or Snipping Tool).
  3. Save files under `docs/screenshots/` in the repository (create the folder if missing). Example: `docs/screenshots/swagger.png`.
  4. Reference the image in this README using: `![Swagger UI](docs/screenshots/swagger.png)`.

Deployment
- Use deploy.ps1 to build a runtime-specific publish folder and zip-deploy to Azure App Service. Example:
  .\deploy.ps1 -ResourceGroup PortfolioRG -AppName keerthiportfolioapi -Runtime linux-x64

Security
- Do NOT commit appsettings.json with real credentials. Use appsettings.Sample.json as a template and set environment variable DEFAULTCONNECTION or configure connection strings in your cloud provider.

Contributing and License
- This repo is suitable for personal portfolio use. Add CONTRIBUTING.md if you accept external contributions. Consider adding an open-source license like MIT.
