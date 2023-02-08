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
- Uses **MinimalApi Filters** to handle processing incoming requests before they make it to the endpoints
  - Uses **FluentValidation** to validate the request.
- Uses **Mediator** not **MediaTr** to handle the request.  
  _(Note: - **Mediator** is much faster than **MediaTr**)_
- Uses **Api Versioning**.
- Contains: -
  - Example of Implementing a **Middleware** in minimal apis.
  - Example of using **Background Services** where the background service uses the scoped database service.
- Uses **Serilog** for logging.

### Jwt Authentication

- Checkout to `Jwt` branch to learn about how to implement **Jwt Authentication** in **MinimalApis**.

### Api Keys Authentication

- Checkout to `Api-Key` branch to learn about how to implement **Api Keys Authentication** in **MinimalApis**.  
  _(It basically just uses filters 🤷🏻‍♂️)_
