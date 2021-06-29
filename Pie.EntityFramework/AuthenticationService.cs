using Microsoft.AspNet.Identity;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services.IAuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserDataService _userDataService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IUserDataService userDataService, IPasswordHasher passwordHasher)
        {
            _userDataService = userDataService;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, UserType userType)
        {
            RegistrationResult registrationResult = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                registrationResult = RegistrationResult.PasswordsDoNotMatch;
                return registrationResult;
            }

            if (await _userDataService.UserEntityByEmail(email) != null)
            {
                registrationResult = RegistrationResult.EmailAlreadyExists;
                return registrationResult;
            }

            if (await _userDataService.UserEntityByUsername(username) != null)
            {
                registrationResult = RegistrationResult.UsernameAlreadyExists;
                return registrationResult;
            }

            if (registrationResult == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User newUser = new User()
                {
                    Email = email,
                    Username = username,
                    HashedPassword = hashedPassword,
                    Type = userType,
                };

                User createdUser = await _userDataService.Create(newUser);
            }

            return registrationResult;
        }

        public async Task<User> Login(string username, string password)
        {
            User storedUser = await _userDataService.UserEntityByUsername(username);

            if (storedUser != null)
            {
                PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(storedUser.HashedPassword, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    return storedUser;
                }
            }

            return null;
        }
    }
}
