namespace Domain.Queries;

/// <summary>
/// Query to get a user by ID
/// </summary>
public class GetUserByIdQuery
{
    public int UserId { get; set; }

    public GetUserByIdQuery(int userId)
    {
        UserId = userId;
    }
}
