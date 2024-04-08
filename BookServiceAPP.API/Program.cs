using BookServiceAPP.API.Custom;
using BooksService.Infrastructure;
using BooksService.Application;
using BooksService.Infrastructure.Options;
using BooksService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Services.AddControllers().AddOData(options => options.Select().Expand().Filter().OrderBy().Count().SetMaxTop(100));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/auth"),
                TokenUrl = new Uri("https://oauth2.googleapis.com/token"),
                Scopes = {
                    { "openid", "OpenID" },
                    { "profile", "Profile" }
                }
            }
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "openid", "profile" }
        }
    });
    c.OperationFilter<ODataOperationFilter>();

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.Configure<GoogleBooksApiOptions>(builder.Configuration.GetSection("GoogleBooksApi"));
builder.Services.AddHttpClient<GoogleBooksService>();
builder.Services.AddSingleton<ErrorHandlingMiddleware>();
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false, // We don't need to validate signing key for Google JWTs
            ValidIssuer = "https://accounts.google.com",
            ValidAudience = "89987156911-dhgsfosrfemcoca1go4bvd52tb1kk02e.apps.googleusercontent.com",
            ValidateLifetime = false
        };
    });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");

        // Configure OAuth settings
        c.OAuthClientId("89987156911-dhgsfosrfemcoca1go4bvd52tb1kk02e.apps.googleusercontent.com");
        c.OAuthClientSecret("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");
        c.OAuthAppName("InternalClient");


    });
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
app.Run();