using Estacionamento.Domain;
using Estacionamento.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
services.AddEntityFrameworkSqlServer()
    .AddDbContext<EstacionamentoContext>(options => {
        options.ConfigureWarnings(warningsConfigurationBuilder => warningsConfigurationBuilder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        options.UseLazyLoadingProxies()
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

services.AddScoped<IEstacionamentoContext, EstacionamentoContext>();
services.AddDomainServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
