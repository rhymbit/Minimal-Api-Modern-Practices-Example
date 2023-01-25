using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Join("..", Path.DirectorySeparatorChar.ToString(), "Database", "database.db");

var db = new Database.DbContext();
db.Database.Migrate();


// Add services to the container.
builder.Services.AddDbContext<Database.DbContext>(options =>
{
    string connectionString = new SqliteConnectionStringBuilder(dbPath).ConnectionString;
    options.UseSqlite(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
