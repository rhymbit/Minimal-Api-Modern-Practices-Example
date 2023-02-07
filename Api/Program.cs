using Api.v1.Endpoints;
using Api.v1.FiltersAndValidators.Validators;
using Api.v1.Middlewares;
using Api.v1.Models.UserModels;
using Api.v1.Services;

var builder = WebApplication.CreateBuilder(args);

using var db = new MyDbContext();
db.Database.EnsureCreated();

// ********** Add services to the container. ***************

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlite($"Data Source={Path.Join(Environment.CurrentDirectory, "database.db")}");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"],
            ValidAudience = builder.Configuration["Authentication:Schemes:Bearer:ValidAudience"],
            // Use Dotnet's Secret Manager to store this key instead of the configuration file
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Authentication:Key"] ?? "")),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };
    });
builder.Services.AddAuthorization();

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

// Mediator
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

// User Services
builder.Services.AddScoped<UserService>();

// Background Tasks
builder.Services.AddScoped<UserCountingService>();
//builder.Services.AddHostedService<UserCountingServiceHostedService>();

builder.Services.AddCors();

// JWT Service
builder.Services.AddScoped<JwtService>();

// *********** WebApplication app ***************

var app = builder.Build();

var apiGroup = app.NewVersionedApi().MapGroup("/api");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
});
app.UseAuthentication();
app.UseAuthorization();

app.UseRequestOperationCancelled();

app.MapAllEndpointsInformation();

apiGroup
    .MapGet("/v{version:apiVersion}/token", (JwtService jwtService, string username) =>
    {
        var token = jwtService.GenerateJwtToken(username);
        return Results.Ok(token);
    })
    .HasApiVersion(new ApiVersion(1, 0))
    .WithName("GetJwtTokenWithoutAuth");

apiGroup
    .MapGroup("/v{version:apiVersion}/users")
    .MapUserEndpointsV1()
    .RequireAuthorization()
    .HasApiVersion(new ApiVersion(1, 0));

app.Run();
