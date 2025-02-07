using Core.Interfaces;
using Domain.Entities;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Responses;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.App.User.Command
{
    public class LoginUserCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }  
        public string GoogleIdToken { get; set; }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IAppDbContext _appDbContext;
        public LoginUserCommandHandler(IJwtService jwtService, IAppDbContext appDbContext, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;       
            _appDbContext = appDbContext;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.GoogleIdToken))
            {
                var googleUser = await ValidateGoogleToken(command.GoogleIdToken);
                var user =  _appDbContext.Set<Users>().Where(x => x.Email == command.Email);
                var newUser = user.Adapt<Users>();
                return await GenerateTokens(newUser);
            }
            var loginuser = _appDbContext.Set<Users>().Where(x => x.Email == command.Email);
            var newLoginUser = loginuser.Adapt<Users>();

            if (loginuser == null || !_passwordHasher.VerifyPassword(newLoginUser.PasswordHash, command.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return await GenerateTokens(newLoginUser);
        }

        private async Task<TokenResponse> GenerateTokens(Users user)
        {
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string googleIdToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken);
                return payload;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Invalid Google ID Token.", ex);
            }
        }
    }
}
