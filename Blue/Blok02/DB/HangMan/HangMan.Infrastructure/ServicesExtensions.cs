using HangMan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HangMan.Infrastructure {
    public static class ServicesExtensions {
        public static IServiceCollection AddHangManDbContext(this IServiceCollection services, string connectionString) {
            services.AddDbContext<HangManDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
