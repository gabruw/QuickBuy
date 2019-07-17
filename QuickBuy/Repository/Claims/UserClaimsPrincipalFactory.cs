using Domain.IDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Repository.Claims
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AccountIDTO>
    {
        public UserClaimsPrincipalFactory(UserManager<AccountIDTO> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AccountIDTO user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (user.Permission.Equals("User"))
            {
                identity.AddClaim(new Claim("User", user.Permission));
            }
            else if (user.Permission.Equals("Administrator"))
            {
                identity.AddClaim(new Claim("Administrator", user.Permission));
            }
            
            return identity;
        }
    }
}
