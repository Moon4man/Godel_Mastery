using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Core.Services
{
    public class AuthorizationService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<AuthorizationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Authentication Authenticate(string username, string password)
        {
            var userAuth = _unitOfWork.Authentication.Get(username);
            if (userAuth == null)
            {
                _logger.LogInformation($"The user was not found in the DB");
                return null;
            }
            var salt = Convert.FromBase64String(userAuth.PasswordSalt);
            var hashString = Convert.ToBase64String(CalculateHash(password, salt));
            if (userAuth.PasswordHash != hashString)
            {
                _logger.LogInformation($"The entered password does not match the password from the DB");
                return null;
            }
            return userAuth;
        }

        public async Task AddUser(string username, string password)
        {
            var salt = GenerateSalt();
            _logger.LogDebug("Salt is calculated");
            var hash = CalculateHash(password, salt);
            _logger.LogDebug("Hash is calculated");
            var saltString = Convert.ToBase64String(salt);
            var hashString = Convert.ToBase64String(hash);
            await _unitOfWork.Authentication.Add(new Authentication() { UserName = username, PasswordHash = hashString, PasswordSalt = saltString, Role = "user" });
            _logger.LogDebug($"A user with an Username: {username} has been added to the database!");
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] CalculateHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }

        public bool Exists(string username)
        {
            return _unitOfWork.Authentication.Get(username) != null;
        }

        public async Task Add(UserDTO user)
        {
            var users = _mapper.Map<User>(user);
            await _unitOfWork.User.Add(users);
        }
    }
}
