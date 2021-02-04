using Park2API.Models;
using Park2API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park2API.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> ChangePasswordAsync(ChangePasswordModel model);
    }
}
