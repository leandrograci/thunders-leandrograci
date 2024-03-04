using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Thunders.TaskGo.Web.Startup.Extensions
{
    public static class JwtAuthorizationExtension
    {        
        public static void AddJwtAuthorization(this IServiceCollection services)
        {
           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               var key = Encoding.UTF8.GetBytes("qFBfvoaGAaXMPtqUON63xBiVF9EiSLIEZk14CJszAe0");
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,                   
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = TimeSpan.Zero,                  
                   IssuerSigningKey = new SymmetricSecurityKey(key)
               };
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BearerPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
