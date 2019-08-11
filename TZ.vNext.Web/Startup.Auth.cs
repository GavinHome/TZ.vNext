//-----------------------------------------------------------------------------------
// <copyright file="Startup.Auth.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TZ.vNext.Services.Contracts;
using TZ.vNext.Web.TokenProvider;

namespace TZ.vNext.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        private void ConfigureAuth(IServiceCollection services)
        {
            var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));

            var _tokenValidationParameters = new TokenValidationParameters
            {
                //// The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                //// Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                //// Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                //// Validate the token expiry
                ValidateLifetime = true,
                //// If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => { options.TokenValidationParameters = _tokenValidationParameters; });
            ////.AddCookie(options =>
            ////{
            ////    options.Cookie.Name = Configuration.GetSection("TokenAuthentication:CookieName").Value;
            ////    options.TicketDataFormat = new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, _tokenValidationParameters);
            ////});
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password, IAccountService accountService)
        {
            bool.TryParse(Configuration.GetSection("TZIWB:NoNeedPassword").Value, out bool isDev);
            var isSuccess = accountService.Auth(username, password, isDev, Configuration.GetSection("TZIWB:DebugKey").Value);
            if (isSuccess)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }

        private TokenProviderOptions GetTokenProviderOptions()
        {
            var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));

            var _tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity,
                Expiration = int.TryParse(Configuration.GetSection("TZIWB:TokenExpiration")?.Value, out int seconds) ? TimeSpan.FromSeconds(seconds) : TimeSpan.FromSeconds(300)
            };

            return _tokenProviderOptions;
        }
    }
}
