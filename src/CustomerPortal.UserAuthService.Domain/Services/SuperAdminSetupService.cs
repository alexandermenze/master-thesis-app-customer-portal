using CustomerPortal.UserAuthService.Domain.Aggregates;
using CustomerPortal.UserAuthService.Domain.DataClasses;
using CustomerPortal.UserAuthService.Domain.Repositories;

namespace CustomerPortal.UserAuthService.Domain.Services;

public class SuperAdminSetupService(
    IUserRepository userRepository,
    IRegisterUserService registerUserService
) : ISuperAdminSetupService
{
    public async Task SetupSuperAdmin(string email, string password)
    {
        if (await userRepository.GetByEmail(email) is not null)
            return;

        var currentSuperAdmins = await userRepository.GetByRole(UserRole.SuperAdmin);

        foreach (var currentSuperAdmin in currentSuperAdmins)
            await userRepository.Delete(currentSuperAdmin.Id);

        var user = await registerUserService.Register(
            new RegisterUserData(email, password, "Super", "Admin", UserRole.SuperAdmin)
        );

        user.Approve();

        await userRepository.Save(user);
    }
}
