using BooksService.Application.Abstractions;
using BooksService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IGoogleBooksService, GoogleBooksService>();
            services.AddScoped<IGoogleBooksServiceDecorator, CachingGoogleBooksServiceDecorator>();
            return services;
        }
    }
}
