using Core.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.App.User.Command
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsGoogleSignIn { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserCommandHandler(IAppDbContext appDbContext, IPasswordHasher passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            {
                throw new ArgumentException("Invalid Data");
            }

            var existingUser = _appDbContext.Set<Users>().FirstOrDefault(x => x.Email == command.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("User Already Exists");
            }
            string hashedPassword = null;
            if (!command.IsGoogleSignIn)
            {
                hashedPassword = _passwordHasher.HashPassword(command.Password);
            }
            var user = new Users
            {
                Email = command.Email,
                Name = command.Name,
                PasswordHash = hashedPassword,
                ProfilePicture = command.ProfilePicture
            };
            await _appDbContext.Set<Users>().AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user.UserId;
        }        
    }
}
