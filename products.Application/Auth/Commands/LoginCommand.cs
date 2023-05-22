using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using products.Application.Auth.Models;
using products.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace products.Application.Auth.Commands;

public class LoginCommand : IRequest<CommandResponse<TokenEntity>>
{
    public string Email { get; set; }
    public string Password { get; set; }


    public class Handler : IRequestHandler<LoginCommand, CommandResponse<TokenEntity>>
    {
        private UserManager<ApplicationUser> _userManager;
        public Handler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task<CommandResponse<TokenEntity>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.Include(q => q.AspNetUserRoles).Where(q => q.Email == request.Email).FirstOrDefaultAsync();
                if (user != null)
                    if (await _userManager.CheckPasswordAsync(user, request.Password))
                    {

                        var roles = _userManager.GetRolesAsync(user).Result;

                        {
                            var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(roles.ToArray()), ClaimValueTypes.String),
                };



                            var superSecretPassword = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecrateKeyforapp12"));

                            var token = new JwtSecurityToken(
                                issuer: "produ",
                                audience: "produ",
                                expires: DateTime.Now.AddDays(1),
                                claims: claims,
                                signingCredentials: new SigningCredentials(superSecretPassword, SecurityAlgorithms.HmacSha256)
                            );

                            var result = new TokenEntity
                            {
                                Token = new JwtSecurityTokenHandler().WriteToken(token),
                                Expiration = token.ValidTo,
                                Roles=roles.ToList(),
                            };

                            return new CommandResponse<TokenEntity>
                            {
                                Data = result,
                                Message = "Login Success !",
                                Success = true,
                            };
                        }
                        return new CommandResponse<TokenEntity>
                        {
                            Data = null,
                            Message = "Wrong Password!",
                            Success = true,
                        };
                    }
                return new CommandResponse<TokenEntity>
                {
                    Data = null,
                    Message = "User Not Found !",
                    Success = false,
                }; ;

            }
            catch (Exception ex)
            {
                return new CommandResponse<TokenEntity>
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                }; ;
            }
        }
    }
}

