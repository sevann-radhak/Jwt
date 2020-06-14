using Microsoft.AspNetCore.Identity;

namespace Jwt.Models.Error
{
    public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return base.DuplicateUserName(userName);
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "The Password file needs at least an Upper character -> ('A' to 'Z')"
            };
        }
    }
}
