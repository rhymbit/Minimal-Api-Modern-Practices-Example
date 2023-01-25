using Api.v1.Endpoints;
using Asp.Versioning;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var db = new Database.DbContext();
db.Database.Migrate();

// Add services to the container.
builder.Services.AddDbContext<Database.DbContext>(options =>
{
    var connStringBuilder = new SqliteConnectionStringBuilder(Environment.CurrentDirectory);
    options.UseSqlite(connStringBuilder.ConnectionString);
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(), // localhost:5201/api/v1/users
        new QueryStringApiVersionReader("api-version"), // localhost:5201/api/users?api-version=1.0
        new HeaderApiVersionReader("X-Version"), // X-Version = 1.0 (in request Header)
        new MediaTypeApiVersionReader("ver") // Content-Type = application/json;ver=1.0 (in request Header)
    );
});
builder.Services.AddCors();


var app = builder.Build();
var apiGroup = app.NewVersionedApi().MapGroup("/api");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
});

apiGroup
    .MapGroup("/v{version:apiVersion}/Users")
    .MapUserEndpoints()
    .HasApiVersion(new ApiVersion(1, 0));


app.Run();
