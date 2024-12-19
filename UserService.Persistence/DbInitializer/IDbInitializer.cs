namespace UserService.Persistence.DbInitializer;

public interface IDbInitializer
{
    public Task InitializeAsync();
}