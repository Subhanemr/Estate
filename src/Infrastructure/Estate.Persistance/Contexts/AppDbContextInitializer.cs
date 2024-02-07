using Estate.Domain.Entities;
using Estate.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Estate.Persistance.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task InitializeDbContextAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task CreateUserRolesAsync()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }

        public async Task InitializeAdminAsync()
        {
            AppUser admin = new AppUser
            {
                Name = "Subhan",
                Surname = "Neqa",
                Email = _configuration["AdminSettings:Email"],
                UserName = _configuration["AdminSettings:UserName"],
                IsFounder = true
            };

            await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(admin, UserRoles.Admin.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
            await _userManager.ConfirmEmailAsync(admin, token);
        }
        public async Task InitializeModeratorAsync()
        {
            AppUser moder = new AppUser
            {
                Name = "Moderator",
                Surname = "Neqa",
                Email = _configuration["ModeratorSettings:Email"],
                UserName = _configuration["ModeratorSettings:UserName"],
                IsFounder = true
            };

            await _userManager.CreateAsync(moder, _configuration["ModeratorSettings:Password"]);
            await _userManager.AddToRoleAsync(moder, UserRoles.Moderator.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(moder);
            await _userManager.ConfirmEmailAsync(moder, token);
        }
    }
}
