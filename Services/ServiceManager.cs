using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationServices> _AuthServices;
        public ServiceManager(IMapper mapper,IConfiguration config,UserManager<Customer> userManager,IOptions<JwtOptions> options)
        {
            _AuthServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, mapper, config, options));
        }
        public IAuthenticationServices AuthenticationServices => _AuthServices.Value;
    }
}
