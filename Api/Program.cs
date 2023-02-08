using Api.v1.BackgroundTasks.UserBgTasks;
using Api.v1.Endpoints;
using Api.v1.FiltersAndValidators.Validators;
using Api.v1.Middlewares;
using Api.v1.Models.ApiKeys;
using Api.v1.Models.UserModels;
using Api.v1.Services;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

using var db = new MyDbContext();
db.Database.EnsureCreated();

// ********** Add services to the container. ***************

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlite($"Data Source={Path.Join(Environment.CurrentDirectory, "database.db")}");
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(), // api/v1/users
        new QueryStringApiVersionReader("api-version"), // api/users?api-version=1.0
        new HeaderApiVersionReader("X-Version"), // X-Version=1.0 (in request Header)
        new MediaTypeApiVersionReader("ver") // Content-Type=application/json;ver=1.0 (in request Header)
    );
});

// Fluent Validators
builder.Services.AddScoped<IValidator<AddUserRequestModel>, AddUserValidator>();
builder.Services.AddScoped<IValidator<PutUserRequestModel>, PutUserValidator>();
builder.Services.AddScoped<IValidator<UserApiKeyModel>, UserApiKeyValidator>();

// Mediator
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

// User Services
builder.Services.AddScoped<UserService>();

// Background Tasks
builder.Services.AddScoped<UserCountingService>();
builder.Services.AddHostedService<UserCountingServiceHostedService>();

builder.Services.AddCors();

// *********** WebApplication app ***************
var app = builder.Build();
var apiGroup = app.NewVersionedApi().MapGroup("/api");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
});

app.UseRequestOperationCancelled();

app.MapAllEndpointsInformation();

apiGroup
    .MapGroup("/v{version:apiVersion}/users")
    .MapUserEndpointsV1()
    .HasApiVersion(new ApiVersion(1, 0));


app.Run();
