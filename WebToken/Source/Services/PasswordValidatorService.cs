using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;
 
namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService: IResourceOwnerPasswordValidator
    {
        private readonly CodenationContext _dbContext;
        public PasswordValidatorService(CodenationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //Acessar contexto de cliente
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == context.UserName);
            //validar a senha
            if(user != null && user.Password == context.Password)
            {
                //retornar o Grant Validation Result
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod:"custom",
                    claims: UserProfileService.GetUserClaims(user));
                return Task.CompletedTask;
            }
            else
            {
                //add descrição de erro de token
                context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant, "Invalid username or password");
                return Task.FromResult(context.Result);
            }
        }
    }
}