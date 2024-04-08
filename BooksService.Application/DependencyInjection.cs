using AutoMapper;
using BooksService.Application.Abstractions;
using BooksService.Application.CacheProviders;
using BooksService.Application.Queries;
using BooksService.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));
            services.AddTransient<IValidator<GetBooksQuery>, GetBooksQueryValidator>();
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>(); // For in-memory cache
            // OR
            //services.AddSingleton<ICacheProvider, DistributedCacheProvider>(); // For distributed cache
            // Configure AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddValidatorsFromAssembly(assembly);


            return services;
        }
    }
}
