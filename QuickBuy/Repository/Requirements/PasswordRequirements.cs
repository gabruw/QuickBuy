using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Repository.Requirements
{
    public class PasswordRequirements<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var email = await manager.GetEmailAsync(user);

            if (email == password)
            {
                var identityError = new IdentityError();
                identityError.Code = "000xREP01";
                identityError.Description = "A senha não pode ser idêntica ao Email.";

                return IdentityResult.Failed(identityError);
            }

            if (password.ToLower().Contains("password"))
            {
                var identityError = new IdentityError();
                identityError.Code = "000xREP02";
                identityError.Description = "A senha não pode conter a palavra 'Password'.";

                return IdentityResult.Failed(identityError);
            }

            if (password.ToLower().Contains("senha"))
            {
                var identityError = new IdentityError();
                identityError.Code = "000xREP03";
                identityError.Description = "A senha não pode conter a palavra 'Senha'.";

                return IdentityResult.Failed(identityError);
            }

            return IdentityResult.Success;
        }
    }
}