namespace CustomerPortal.UserAuthService.Domain.Services;

public interface IPasswordService
{
    void EnsureRequirementsAreMet(string password);
    string HashPassword(string email, string password);
}
