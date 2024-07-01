# Driving School Website

Welcome to the Driving School Website project. This is a portfolio project developed using .NET Core MVC and SQL Server. It showcases a web application designed to facilitate driving lesson bookings and enhance communication between clients and the driving school via email and phone messages.

## Features

- **Home Page**: Introduction to the driving school and its services.
- **About Us**: Information about the driving school, instructors, and mission.
- **Contact Us**: Contact form to get in touch with the school.
- **Booking System**: Clients can book driving lessons online.
- **Email Communication**: Automatic email notifications for bookings and reminders.
- **Phone Messaging**: SMS notifications for bookings and reminders.

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Database**: SQL Server
- **Email Service**: SMTP
- **SMS Service**: Twilio (or any other SMS gateway)

## Project Structure

- **Controllers**: Handles the incoming HTTP requests, processes them, and returns the appropriate response.
- **Views**: Defines the UI of the application using Razor syntax.
- **Models**: Represents the data structure and business logic.
- **Migrations**: Handles the creation and modification of the database schema.

## Installation and Setup

### Prerequisites

- .NET Core SDK
- SQL Server
- Visual Studio or any other compatible IDE

### Steps

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/driving-school-website.git
   cd driving-school-website
   ```

2. **Restore NuGet packages**:

   ```bash
   dotnet restore
   ```

3. **Setup the database**:

   - Create a SQL Server database.
   - Update the `appsettings.json` file with your database connection string.

4. **Run database migrations**:

   ```bash
   dotnet ef database update
   ```

5. **Configure Email and SMS services**:
   - Update the `appsettings.json` file with your SMTP server and Twilio (or other SMS gateway) settings.

### Running the Application

1. **Build the application**:

   ```bash
   dotnet build
   ```

2. **Run the application**:

   ```bash
   dotnet run
   ```

3. Open your browser and navigate to `https://localhost:5001` to access the website.

## Usage

- **Home Page**: Learn about the driving school and its offerings.
- **About Us**: Get to know more about the team and the mission of the driving school.
- **Contact Us**: Use the contact form to send inquiries.
- **Booking System**: Easily schedule driving lessons online.
- **Notifications**: Receive email and SMS notifications for booking confirmations and reminders.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any queries or support, please contact us at [support@drivingschool.com](mailto:support@drivingschool.com).

---

This project demonstrates a complete web solution for a driving school, highlighting the use of modern web technologies and communication tools.
