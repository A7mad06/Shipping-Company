using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationServices
    {
        public Task<Customer?> CheckEmailExistAsync(string email);
        public Task<UserResultDTO?> LoginAsync(LoginDTO customer);
        public Task<UserResultDTO?> RegisterAsync(RegisterDTO customer);
        public Task<UserResultDTO?> RegisterAdminAsync(RegisterDTO Admin);
        public Task<string?> CreateToken(Customer customer, bool RememberMe);
    }
}
