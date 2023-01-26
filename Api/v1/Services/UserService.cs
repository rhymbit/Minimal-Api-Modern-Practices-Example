namespace Api.v1.Services;

public class UserService
{
    private readonly MyDbContext _db;
    public UserService(MyDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _db.Users.ToListAsync();

    public async Task<User?> GetUserAsync(string id) => await _db.Users.FindAsync(id);

    public async Task<User> AddUserAsync(User user)
    {
        user.Id = Guid.NewGuid().ToString();
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> PutUserAsync(User user)
    {
        var result = await _db.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(u => u
            .SetProperty(x => x.Name, user.Name)
            .SetProperty(x => x.Age, user.Age)
            .SetProperty(x => x.Email, user.Email)
            .SetProperty(x => x.Password, user.Password)
        );
        return result > 0 ? user : null;
    }

    public async Task<User?> DeleteUserAsync(string id)
    {
        var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (existingUser is null)
        {
            return null;
        }
        _db.Users.Remove(existingUser);
        await _db.SaveChangesAsync();
        return existingUser;
    }

    public async Task<List<User>?> DeleteAllUsersAsync()
    {
        var existingUsers = await _db.Users.ToListAsync();
        if (existingUsers is null)
        {
            return null;
        }
        _db.Users.RemoveRange(existingUsers);
        await _db.SaveChangesAsync();
        return existingUsers;
    }
}
