using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskApp.Application.Authentication.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Domain.Entities;

namespace TaskApp.Infrastructure.Authentication.Commands
{
    public class RegisterCommand : IRequest<Response<RegisterResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.").NotEmpty().WithMessage("Password is required.");
        }
    }

    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<RegisterCommand, Response<RegisterResponse>>
    {
        public async Task<Response<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, UserName = request.Email, EmailConfirmed = true };
            var newUser = await userManager.CreateAsync(user, request.Password);
            if (!newUser.Succeeded)
                throw new Exception(newUser.Errors.FirstOrDefault()?.Description);
            return new Response<RegisterResponse>(user.ToResponse());
        }
    }


}