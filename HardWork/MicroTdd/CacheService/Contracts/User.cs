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

    public static User Create(long id, string firstName)
    {
        return new User(id,  firstName: firstName);
    }

    public string GetKey()
    {
        return Id.ToString();
    }
}

public sealed class Result<T>
{
    public T Value { get; }
    public bool IsSuccess { get; }
    public string? Error { get; }

    private Result(T value, bool isSuccess, string? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, isSuccess: true, error: null);
    }

    public static Result<T> Failure(string? error)
    {
        return new Result<T>(default!, isSuccess: false, error: error);
    }
}