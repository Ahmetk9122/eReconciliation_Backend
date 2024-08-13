
using Autofac;
using Autofac.Extensions.DependencyInjection;
using eReconciliation.Business.DependencyResolver.Autofac;
using eReconciliation.Business.DependencyResolver;
using eReconciliation.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using eReconciliation.Core.Extensions;
using AutoMapper;
using eReconciliation.Business;
using eReconciliation.Core.Utilities.IoC;
using eReconciliation.Core.DependencyResolvers;
using System.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region [ AutoMapper ]
services.AddAutoMapper(typeof(DomainProfile));
#endregion

#region  [AUTOFAC]
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion

services.AddControllers();
#region  [JWT]
IConfiguration configuration = builder.Configuration;

builder.Services.AddCors(opt =>
{
    ///Dışardan apiyle veya istekle hangi sitelerden gelebileceğini sorguluyorç
    opt.AddPolicy("AllowOrigin",
    builder => builder.WithOrigins("https://localhost:7129"));

});

var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
    };
});

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
});
#endregion

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});


//Bunu CoreModule içine taşı
services.AddSingleton<Stopwatch>();

var app = builder.Build();

MappingExtensions.Configure(app.Services.GetService<IMapper>());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("https://localhost:7129").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
