# ASP.NET Student Registration System

This repository contains a simple Student Registration System built using ASP.NET for the frontend and SQL Server for the backend. The system allows users to register students, update their information, and view the registered students through a web interface.

## Project Structure

- **StudentRegistrations**: ASP.NET Empty Web Application project.
- **WebForm1.aspx**: Web form for capturing student details.
- **web.config**: Configuration file containing the database connection string.
- **SQL Scripts**: Folder containing SQL scripts for creating the database and table.

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/ASP.NET-Student-Registration.git

## Setting up Database:

1. **Open SQL Server Management Studio:**
   - Launch SQL Server Management Studio.

2. **Execute SQL Script:**
   - Execute the SQL script provided in the "SQL Scripts" folder to create the database and table.

## Configuring Connection:

1. **Open `web.config` file:**
   - Navigate to the project directory.
   - Open the `web.config` file in a text editor.

2. **Replace SQL Server Details:**
   - Locate the connection string in the `web.config` file.
   - Replace `YOUR_SERVER_NAME`, `YOUR_USERNAME`, and `YOUR_PASSWORD` with your SQL Server details.

## Opening the Project:

1. **Open in Visual Studio:**
   - Open Microsoft Visual Studio.

2. **Load Project:**
   - Open the project located in the project directory.

## Running the Application:

1. **Run in Visual Studio:**
   - Run the application in Visual Studio to launch the web form.

## Usage:

### Register a Student:

1. **Fill in Student Details:**
   - Open the web form.
   - Fill in the student details in the form on the web page.

2. **Click "Register" Button:**
   - Click the "Register" button to submit the student details.

### Update Student Information:

1. **Edit Student Details:**
   - Open the web form.
   - Edit the student details in the form.

2. **Click "Update" Button:**
   - Click the "Update" button to save the changes.
  
### Delete Student Record:

1. **Select Student to Delete:**
   - In the GridView displaying registered students, select the student record you want to delete.

2. **Click "Delete" Button:**
   - Click the "Delete" button to remove the selected student record.


### View Registered Students:

- The registered students are displayed in a GridView below the form.
- You can edit or view details of each student through the provided options.

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or enhancements, please submit an issue or create a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
