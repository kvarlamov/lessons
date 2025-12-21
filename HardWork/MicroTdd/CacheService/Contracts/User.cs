namespace CacheService.Contracts;

public class User
{
    public long Id { get; }
    public string FirstName { get; }

    private User(long id, string firstName)
    {
        Id = id;
        FirstName = firstName;
    }

    
    /// <param name="firstName">Not Null or empty</param>
    /// PRE: first name is not null
    /// <returns>New user</returns>
    /// POST: created new user with id=0
    public static User CreateNew(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty", nameof(firstName));
        }
        
        return new User(id:0, firstName: firstName);
    }
}