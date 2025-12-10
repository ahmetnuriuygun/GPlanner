# GPlanner

## Project Overview

GPlanner is a comprehensive task-planning application designed to assist users in managing their daily and academic tasks efficiently. The application leverages AI-powered features to generate personalized task plans and integrates a cross-platform user interface for seamless interaction. Built with modern .NET technologies, it provides a robust backend API and an intuitive MAUI-based frontend.

Key features include:

- User task management with categorization and prioritization.
- AI-generated daily plans using Google's Gemini API.
- Scheduled task tracking and completion monitoring.
- Cross-platform support for Android, iOS, Windows, and macOS.

## Architecture

The project follows a layered architecture with clear separation of concerns:

- **GPlanner.Api**: ASP.NET Core Web API providing RESTful endpoints for task and user management. Includes Swagger documentation for API exploration.
- **GPlanner.Data**: Entity Framework Core data layer with DbContext and migrations for MySQL database interactions.
- **GPlanner.Core**: Domain models, enums, and repository interfaces defining the business logic and data contracts.
- **GPlanner.Maui**: Cross-platform UI built with .NET MAUI, implementing the Model-View-ViewModel (MVVM) pattern.

### MVVM Architecture in GPlanner.Maui

The MAUI application adheres to the MVVM design pattern for maintainable and testable code:

- **Models**: Domain entities from `GPlanner.Core.Model`, such as `User`, `UserTask`, `DailyPlanItem`, and `ScheduledTask`.
- **ViewModels**: Located in `ViewModels/`, these classes expose observable properties and commands. Examples include `TasksViewModel` for task management and `UserViewModel` for user-related operations. They handle data binding and user interactions.
- **Views**: XAML-based pages in `Pages/`, bound to ViewModels for UI rendering. Includes pages for tasks, user profiles, and planning.
- **Services**: Found in `Services/`, these handle HTTP communication with the API (e.g., `UserTaskService`), AI integration (e.g., `GeminiService`), and local data mapping (e.g., `Mapping/` for DTO conversions).

This structure ensures a clean separation between UI logic and business logic, facilitating unit testing and code reusability.

## Prerequisites

- Docker and Docker Compose for containerized deployment.
- .NET 9.0 SDK for building and running the API.
- .NET MAUI workloads installed for the UI (Visual Studio or CLI).
- MySQL database (provided via Docker).
- Optional: Gemini API key for AI features.

## Setup and Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/ThomasMore-u0118137/project-ahmetnuriuygun.git
   cd GPlanner
   ```

2. Ensure Docker is running.

3. Build and start the containers:

   ```bash
   docker-compose up --build -d
   ```

4. Load test data:
   ```bash
   docker-compose exec -T mysql_db_container_name mysql -uroot -pmy_secure_password gplanner_db < scripts/test_data.sql
   ```

## Running the Application

### API (Backend)

- The API runs on `http://localhost:8080`.
- Swagger UI: `http://localhost:8080/swagger` for interactive API documentation and testing.
- Logs: `docker-compose logs -f gplanner_api`.

### MAUI UI (Frontend)

- **Visual Studio**: Open `GPlannerSolution.sln`, set `GPlanner.Maui` as startup project, select target platform, and run.
- **CLI**: Navigate to `GPlanner.Maui` and run `dotnet build` followed by `dotnet run -f net9.0-android` (adjust framework for target).
- Ensure the API is accessible; on emulators/devices, use the host machine's IP if needed.

## Testing

### API Testing (Swagger)

1. Access `http://localhost:8080/swagger`.
2. Test endpoints:
   - `GET /api/usertask`: Retrieve user tasks.
   - `POST /api/usertask`: Create a new task (provide JSON payload).
   - `PUT /api/usertask/{id}`: Update an existing task.
   - `DELETE /api/usertask/{id}`: Delete a task.
   - `POST /api/planning/generate`: Generate AI-based plans (requires Gemini API key).

### UI Testing

1. Launch the MAUI app.
2. **Add Task**:

   - Navigate to Tasks page.
   - Tap "+" to create a new task.
   - Fill in title, description, type, date, and priority.
   - Save and verify it appears in the list.

3. **Update Task**:

   - Select an existing task.
   - Edit fields and save.
   - Confirm changes reflect in the UI.

4. **Delete Task**:

   - Swipe the task to left and you will see a delete button.
   - Click the delete button to remove item from the list.

5. **AI Generate**:
   - On the Planning or Tasks page, tap the AI/Generate button.
   - Ensure Gemini API key is configured.
   - Check that new scheduled tasks are created and displayed.

### Additional Test Cases

- Verify user data loads correctly.
- Test task filtering and archiving.
- Ensure scheduled tasks link properly to daily plans.
- Validate error handling for invalid inputs.

## Configuration

- **Gemini API Key**: Set `GEMINI__APIKEY` environment variable before `docker-compose up` for AI features.
- **Database**: Configured in `docker-compose.yaml` with MySQL 8.0.
- **Connection String**: Uses `Server=mysql_db_container_name;Port=3306;Database=gplanner_db;Uid=root;Pwd=my_secure_password`.

## Cleanup

Stop and remove containers:

```bash
docker-compose down -v
```

## Troubleshooting

- **API not starting**: Check logs with `docker-compose logs gplanner_api`. Ensure MySQL is ready.
- **Gemini errors**: Verify API key is set correctly.
- **UI connectivity**: On mobile/emulator, confirm API host IP is reachable.
- **Database issues**: Re-run `scripts/test_data.sql` if tables are missing.

## Contributing

Contributions are welcome. Please follow standard .NET coding practices and submit pull requests for review.

## License

This project is licensed under the MIT License.
