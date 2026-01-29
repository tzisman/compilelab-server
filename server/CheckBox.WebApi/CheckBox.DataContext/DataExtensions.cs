using CheckBox.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.DataContext
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CheckBoxContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IContext, CheckBoxContext>();

            return services;
        }
    }
}
