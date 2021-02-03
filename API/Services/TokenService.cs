using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<string> CreateToken(AppUser user)
        {
            //Adding our claims
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName.ToString())
            };
            //

            var roles = await _userManager.GetRolesAsync(user);

            //JwtRegisteredClaimNames doesnt contain an option for role, so we use ClaimTypes
            //Of course, we could use a custom var from JwtRegisteredClaimNames, but let's keep it simple and easy to understand.
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //Creating credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //

            //Describe how our token is going to look
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Initialize tokenHandler to create our token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Return the created token
            return tokenHandler.WriteToken(token);
        }
    }
}