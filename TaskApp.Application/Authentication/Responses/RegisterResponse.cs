
using TaskApp.Domain.Entities;

namespace TaskApp.Application.Authentication.Responses
{
    public class RegisterResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public static class RegisterResponseExtension
    {

        public static RegisterResponse ToResponse(this ApplicationUser user)
        {
            return new RegisterResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                UserName = user.UserName!
                // Token = user.Token,
                // RefreshToken = user.RefreshToken
            };
        }
    }

}