# DataExport Console Application

This console application is designed to export data from a Microsoft SQL Server database to a CSV file. The application is configured to retrieve member data from various districts and save the results in CSV files, with each file corresponding to a specific district.

## Features

- **Data Export**: Exports member data from an SQL Server database based on districts.
- **District Filtering**: Automatically identifies districts and filters data for export.
- **CSV File Generation**: Saves the extracted data in CSV format for each district.
- **Logging**: Uses Serilog for logging information, warnings, and errors to the console.

## Project Structure

- **Program.cs**: The main entry point of the application. It initializes logging, sets up dependencies, and coordinates the data export process.
- **DataDownload.cs**: Handles the retrieval of member data from the database and manages the export to CSV files.
- **Districts.cs**: Retrieves the list of districts from the database to be used for filtering data.
- **Helper/Utility.cs** (implied): Contains utility methods for file path generation and other common tasks.
- **Model/District.cs** (implied): Represents the district model used in data retrieval.

## Prerequisites

- .NET 6.0 or later
- SQL Server with access to the relevant database
- NuGet packages:
  - `Dapper`
  - `CsvHelper`
  - `Serilog`
  - `Serilog.Sinks.Console`

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/YOUR_USERNAME/DataExport.git
   cd DataExport
   ```

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

3. **Build the application**:
   ```bash
   dotnet build
   ```

## Configuration

Ensure that the database connection strings and other relevant configurations are correctly set in your `ConfigurationReader` class or other configuration management.

## Usage

1. **Run the application**:
   ```bash
   dotnet run
   ```

2. The application will:
   - Retrieve the list of districts from the database.
   - For each district, fetch the associated member data.
   - Export the data into a CSV file named with the current date and district.

3. **Logging**:
   - The application logs its operations to the console using Serilog, including the start and end of data exports, as well as any errors encountered.

## Example Output

CSV files will be saved in the format:
```
20240101-DISTRICTNAME-datadump.csv
```

## Error Handling

The application logs errors using Serilog. If any issues occur during data retrieval or file writing, they will be logged, and the application will continue processing other districts.

## Contributing

Feel free to submit issues or pull requests if you find any bugs or have suggestions for new features.

## License

This project is licensed under the MIT License.
