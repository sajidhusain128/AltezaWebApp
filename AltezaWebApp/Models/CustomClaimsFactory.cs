using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AltezaWebApp.Models
{
    public class CustomClaimsFactory: UserClaimsPrincipalFactory<LoginModel>
    {
        public CustomClaimsFactory(UserManager<LoginModel> userManager, IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(LoginModel loginModel)
        {
            var identity = await base.GenerateClaimsAsync(loginModel);
            identity.AddClaim(new Claim("Username", loginModel.Username));
            identity.AddClaim(new Claim("Password", loginModel.Password));

            return identity;
        }

        
    }

    public class ApplicationDbContext : IdentityDbContext<LoginModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
