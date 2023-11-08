namespace Estacionamento.Web.Configurations;

public static class AutenticationConfiguration
{
    public static void AddAutenticationConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
    }
}
