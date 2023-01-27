namespace Api.v1.Services;

public class UserCountingService
{
    private readonly Database.MyDbContext _db;
    public UserCountingService(Database.MyDbContext db)
    {
        _db = db;
    }

    public async Task DoWork(CancellationToken ctoken)
    {
        while (!ctoken.IsCancellationRequested)
        {
            Console.WriteLine($"\nCurrent user count is: {_db.Users.Count()}");
            await Task.Delay(2000, ctoken);
        }
    }
}
