# An Example of a WebApi project using MinimalApis

### This project is an example of a common WebApi project using AspNetCore MinimalApis.

### The project follows current modern practices recommended by Microsoft & the dotnet community.

### To run the project: -

- Make sure you have Dotnet 7.0 installed on your machine.  
  _(To check it run `dotnet --version` or `dotnet --list-sdks`)_
- Clone the repo.
- Run `dotnet run` in the `Api` directory of the project.
- To view the list of endpoint: -
  - Visit `http://localhost:5201`
  - If using `https` then visit `https://localhost:7275`
- Project uses SQLite as the database.

### Features: -

- Uses **MinimalApis** to create the endpoints.
- Uses **FluentValidation** to validate the request.
- Uses **Mediator** not **MediaTr** to handle the request.  
  _(Note: - **Mediator** is much faster than **MediaTr**)_
- Uses api versioning.
- Contains an example of using background services where the background service uses the scoped database service.
- Uses **Serilog** for logging.
