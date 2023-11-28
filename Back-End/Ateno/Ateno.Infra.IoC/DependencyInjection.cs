using Ateno.Application.Helpers;
using Ateno.Application.Interfaces;
using Ateno.Application.Mappings;
using Ateno.Application.Services;
using Ateno.Domain.Account;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using Ateno.Infra.Data.Identity;
using Ateno.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ateno.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                 ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            ////Configurações do Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                    opt.SignIn.RequireConfirmedEmail = true
                )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider(ConstantsToken.LoginProviderToken,
                                  typeof(DataProtectorTokenProvider<ApplicationUser>));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
            });
            services.ConfigureApplicationCookie(options =>
                     options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudyDeckRepository, StudyDeckRepository>();
            services.AddScoped<IStudyCardRepository, StudyCardRepository>();
            services.AddScoped<IStudyProcessRepository, StudyProcessRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomUserRepository, RoomUserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudyDeckService, StudyDeckService>();
            services.AddScoped<IStudyProcessService, StudyProcessService>();
            services.AddScoped<IControllerService, ControllerService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddAutoMapper(typeof(DomainAndDTOMapping));

            services.AddScoped<IAccountService, IdentityService>();
            services.AddScoped<IInitialSeed, InitialSeed>();

            return services;
        }
    }
}
