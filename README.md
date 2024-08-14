# TestProject

This repo consists of the solution in .net 8 with tests written using Nunit and playwright.

This solution can also be run in Pipeline using CircleCI with the config.yml file


## Prerequisites for running the tests locally

- .NET 8.0 or later
- Visual Studio 2022 or later (optional but recommended for development)
- Chrome browser (for any other browser you need to update the appsettings.json file)

### Steps for running the Test locally

## Clone the Repository

First, clone the repository to your local machine:

```bash
git clone https://github.com/bijoyanty/TestProject
```

## Steps to run the actual Test

	-Using Visual Studio
		Open the solution in Visual Studio.
		Build the solution to ensure all dependencies are resolved.
		Go to the Test Explorer (Test -> Test Explorer).
		Run or debug the tests directly from Test Explorer.
	
	-Using Command Line
		You can run tests using the .NET CLI:
		dotnet test
