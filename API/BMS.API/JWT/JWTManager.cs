using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BMS.Common;
using BMS.ViewModels.User;
using BMS.ViewModels.Common;

namespace BMS.API.JWT
{
    public class JWTManager
    {
        SecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.JWTKey));
        public string GenerateTokenForUser(UserLoginResponseModel userDetails, bool isRefreshToken)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.JWTKey));
            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(signingKey,
               SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var claims = GenerateClaims(userDetails);
            var claimsIdentity = new ClaimsIdentity(claims, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                NotBefore = now,
                Expires = now.Add(TimeSpan.FromMinutes(AppConstants.JWTTokenTimeoutMinutes * userDetails.LoginFrom == 1 ? (isRefreshToken ? 2 : 1) : 3600))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
            return signedAndEncodedToken;
        }

        public JwtSecurityToken GenerateUserClaimFromJWT(string authToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = signingKey,
                ValidateLifetime = true,
                LifetimeValidator = LifetimeValidator,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return null;
            }
            return validatedToken as JwtSecurityToken;
        }

        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

        public JWTAuthenticationIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            //string name = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "unique_name").Value;
            ExtractClaims(userPayloadToken);
            return new JWTAuthenticationIdentity(Convert.ToString(SessionManager.UserId)) { UserName = Convert.ToString(SessionManager.UserId) };

        }

        private List<Claim> GenerateClaims(UserLoginResponseModel userDetails)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", Convert.ToString(userDetails.UserId)));
            claims.Add(new Claim("UserTypeId", Convert.ToString(userDetails.UserType)));
            claims.Add(new Claim("BookSellerId", Convert.ToString(userDetails.BookSellerId)));
            claims.Add(new Claim("ParentsId", Convert.ToString(userDetails.ParentsId)));
            return claims;
        }

        private void ExtractClaims(JwtSecurityToken userPayloadToken)
        {
            IEnumerable<Claim> claims = ((userPayloadToken)).Claims;
            SessionManager.UserId = Convert.ToInt32(claims.FirstOrDefault(m => m.Type == "UserId").Value);
            SessionManager.UserTypeId = Convert.ToInt32(claims.FirstOrDefault(m => m.Type == "UserTypeId").Value);
            SessionManager.BookSellerId = Convert.ToInt32(claims.FirstOrDefault(m => m.Type == "BookSellerId").Value);
            SessionManager.ParentsId = Convert.ToInt32(claims.FirstOrDefault(m => m.Type == "ParentsId").Value);

        }
    }
}