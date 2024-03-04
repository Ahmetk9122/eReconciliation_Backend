
using Autofac;
using Autofac.Extensions.DependencyInjection;
using eReconciliation.Business.DependencyResolver.Autofac;
using eReconciliation.Business.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// #region [ AutoMapper ]
// builder.Services.AddAutoMapper(typeof(DomainProfile));
// #endregion

#region  [AUTOFAC]
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion


services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
