using iParkMedusa.Constants;
using iParkMedusa.Models;
using iParkMedusa.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iParkMedusa.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PaymentMethodId = model.PaymentMethodId
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            var userWithSameUsername = await _userManager.FindByNameAsync(model.Username);

            if (userWithSameEmail == null)
            {
                if (userWithSameUsername == null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Constants.Authorization.default_role.ToString());
                        SendEmailRegistration(user);
                    }
                    return $"User Registered with username {user.UserName}";
                }
                else return $"Username {user.UserName} is already in use.";
            }
            else return $"Email {user.Email} is already registered.";
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }
        public void SendEmailRegistration(ApplicationUser user)
        {

            using (MailMessage mail = new MailMessage())
            using (SmtpClient sender = new SmtpClient("smtp.gmail.com", 587))
            {
                sender.EnableSsl = true;
                sender.Credentials = new NetworkCredential("sapiensoft.upskill@gmail.com", "Sapien123!");
                sender.DeliveryMethod = SmtpDeliveryMethod.Network;

                String body = @"
                                        <html>
                                            <body>
                                                <p>Welcome to Mammoth!</p>
                                                <p>We are glad you joined our service and we'll be happier if you enjoy it as much as we did on developing it.</p>
                                                <p>Redeem your voucher to enjoy the first hour for free in our parks!</p>

                                                <p> ~ The SapienSoft Team </p>
                                            </body>
                                        </html>
                                        ";

                AlternateView view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(view);
                mail.From = new MailAddress("sapiensoft.upskill@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Registration is completed :)";

                sender.Send(mail);
            }
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }

            var roleExists = Enum.GetNames(typeof(Constants.Authorization.Roles)).Any(x => x.ToLower() == model.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(Constants.Authorization.Roles)).Cast<Constants.Authorization.Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Added {model.Role} to user {model.Email}.";
            }
            return $"Role {model.Role} not found.";
        }

        // Additional methods for Users
        public async Task<AuthenticationModel> ChangePasswordAsync(ChangePasswordModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }

            if (await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                authenticationModel.Message = $"Password changed with success for account {model.Email}.";
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect credentials for user {user.Email}.";
            return authenticationModel;
        }
        public async Task<AuthenticationModel> EditUser(RegisterModel newInfo, ApplicationUser oldInfo)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByIdAsync(oldInfo.Id);

            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {oldInfo.Email}.";
                return authenticationModel;
            }
            if ((newInfo.FirstName != oldInfo.FirstName) || (newInfo.LastName != oldInfo.LastName) || (newInfo.PaymentMethodId != oldInfo.PaymentMethodId) || (newInfo.Username != oldInfo.UserName) || (newInfo.Email != oldInfo.Email))
            {
                user.FirstName = newInfo.FirstName;
                user.LastName = newInfo.LastName;
                user.PaymentMethodId = newInfo.PaymentMethodId;
                if (await _userManager.FindByEmailAsync(newInfo.Email)== null)
                {
                    user.Email = newInfo.Email;
                }
                else
                {
                    authenticationModel.Message = "New E-mail is already in use.";
                }
                if(await _userManager.FindByNameAsync(newInfo.Username)==null)
                {
                    user.UserName = newInfo.Username;
                }
                else
                {
                    authenticationModel.Message = "New Username already in use.";
                }
                

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Something went wrong.";
                    return authenticationModel;
                }
            }
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.Message = $"Information edited with success for account {user.Email}.";
            return authenticationModel;
        }
    }
}
