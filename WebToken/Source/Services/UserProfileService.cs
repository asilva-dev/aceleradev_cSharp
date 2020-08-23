using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

namespace Codenation.Challenge.Services
{
    public class UserProfileService : IProfileService
    {
        private readonly CodenationContext _dbContext;
        public UserProfileService(CodenationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //JWT
            //ValidatitedTokenReques - parse
            var request = context.ValidatedRequest as ValidatedTokenRequest;

            //verificar se o token é nulo
            if (request != null)
            {
                //Procurar na base as informações
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == request.UserName);
                if (user != null)
                    context.AddRequestedClaims(GetUserClaims(user));
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        public static Claim[] GetUserClaims(User user)
        {
            return new []
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "user")
            };
        }

    }
}