using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices(UserManager<Customer> userManager, IMapper mapper,IConfiguration configurations,IOptions<JwtOptions> options) : IAuthenticationServices
    {
        public async Task<Customer?> CheckEmailExistAsync(string email)
        {
            var customer = await userManager.FindByEmailAsync(email);
            return customer;
        }
        public async Task<string?> CreateToken(Customer customer,bool RememberMe)
        {
            var jwtoption = options.Value;
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,customer.UserName!),new Claim(ClaimTypes.Email,customer.Email!)
            };
            var Roles = await userManager.GetRolesAsync(customer);
            foreach(var role in Roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoption.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiring;
            if (RememberMe)
            {
                expiring = DateTime.UtcNow.AddDays(jwtoption.DurationInDays);
            }
            else
            {
                expiring = DateTime.UtcNow.AddMinutes(15);
            }
            var Token = new JwtSecurityToken(
                audience: jwtoption.Audience,
                issuer: jwtoption.Issuer,
                expires: expiring,
                claims: AuthClaims,
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public async Task<UserResultDTO?> LoginAsync(LoginDTO login)
        {
            var customer = await CheckEmailExistAsync(login.Email);
            if (customer != null)
            {
                var res = await userManager.CheckPasswordAsync(customer, login.Password);
                if (res)
                {
                    return new UserResultDTO(customer.UserName!, customer.Email!, await CreateToken(customer,login.RememberMe));
                }
                else
                    throw new Exception("Wrong Password");
            }
            else throw new Exception("Email Doesn't Exist");
        }
        public async Task<UserResultDTO?> RegisterAsync(RegisterDTO register)
        {
            var customer = await CheckEmailExistAsync(register.Email);
            if (customer != null) throw new Exception("Email is already registered");
            if (register.Password != register.ConfirmPassword) throw new Exception("Passwords don't match");
            var FinalCustomer = mapper.Map<Customer>(register);
            var res = await userManager.CreateAsync(FinalCustomer,register.Password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(FinalCustomer, "User");
                return new UserResultDTO(FinalCustomer.UserName,FinalCustomer.Email,await CreateToken(FinalCustomer,false));
            }
            else
            {
                var errors = res.Errors.Select(s => s.Description).ToList();
                throw new RegisterValidationException(errors);
            }
        }
        public async Task<UserResultDTO?> RegisterAdminAsync(RegisterDTO register)
        {
            var customer = await CheckEmailExistAsync(register.Email);
            if (customer != null) throw new Exception("Email is already registered");
            if (register.Password != register.ConfirmPassword) throw new Exception("Passwords don't match");
            var FinalCustomer = mapper.Map<Customer>(register);
            var res = await userManager.CreateAsync(FinalCustomer, register.Password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(FinalCustomer, "Admin");
                return new UserResultDTO(FinalCustomer.UserName, FinalCustomer.Email, await CreateToken(FinalCustomer, false));
            }
            else
            {
                var errors = res.Errors.Select(s => s.Description).ToList();
                throw new RegisterValidationException(errors);
            }
        }
    }
}
