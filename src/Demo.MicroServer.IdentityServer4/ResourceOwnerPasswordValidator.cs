using Demo.MicroServer.IdentityServer4.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.MicroServer.IdentityServer4
{
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ///这里为了省事才这样把用户验证的过程给写了，优雅的方式最好还是通过接口去实现
            using (var dbcontext = new DemoMicroServerContext())
            {
                var users = dbcontext.Set<users>();

                var user = users.Where(t => t.user_name == context.UserName && t.pass_word == context.Password).FirstOrDefault();

                if (user != null)
                {
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                        claims: GetUserClaims(user));
                }
                else
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid");
            }
            return Task.FromResult("");
        }
        
        private Claim[] GetUserClaims(users user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.user_id),
            new Claim("user_name", user.user_name)
            };
        }
    }
}
