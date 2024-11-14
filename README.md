# UserIdentifierService Project

This project is a sample ASP.NET Core Web API designed to handle user profile image requests. The backend API applies a series of rules to determine and return the appropriate avatar image URL based on a provided user identifier. This project follows enterprise best practices such as layered architecture, dependency injection, structured logging, and global exception handling.

## Table of Contents

- [Requirements](#requirements)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Configuration](#configuration)
- [Endpoints](#endpoints)
- [Rules for Image URL Selection](#rules-for-image-url-selection)
- [Error Handling](#error-handling)
- [Logging](#logging)
- [Future Enhancements](#future-enhancements)

## Requirements

- .NET 7 SDK
- SQLite Database
- Docker (optional, for containerized deployment)

## Project Structure

```plaintext
UserProfileService
├── Controllers
│   └── AvatarController.cs          # Handles HTTP requests for user avatars
├── Middlewares
│   └── ExceptionHandlingMiddleware.cs # Global error handling middleware
├── Models
│   └── AvatarProfile.cs             # Data model for avatar profile response
├── Repositories
│   └── AvatarRepository.cs          # Database access for image URLs
├── Services
│   └── AvatarService.cs             # Business logic for selecting avatar URL
├── appsettings.json                 # Configuration for database and logging
├── appsettings.Development.json     # Development-specific settings
└── Program.cs                      # Entry point of the application

### Database Initialization

To set up the SQLite database, run the following commands:

1. Open a terminal and navigate to the project directory.
2. Open the SQLite command line and run the initialization script:

   ```bash
   sqlite3 data.db < init.sql
