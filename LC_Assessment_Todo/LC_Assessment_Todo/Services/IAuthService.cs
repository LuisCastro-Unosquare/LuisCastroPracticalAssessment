namespace LC_Assessment_Todo.Services
{
    public interface IAuthService
    {
        string GenerateToken(string userName, string password);
    }
}
