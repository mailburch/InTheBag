namespace InTheBag
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===== Session services (must be ABOVE AddControllersWithViews) =====
            builder.Services.AddMemoryCache();
            builder.Services.AddSession(options =>
            {
                // example: 5-minute idle timeout (default is 20)
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                // allow the session cookie even if the user disables non-essential cookies
                options.Cookie.IsEssential = true;
                // leave HttpOnly = true unless you truly need client-side scripts to read it
                // options.Cookie.HttpOnly = false;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ===== Enable session (must be ABOVE UseAuthorization and before endpoints) =====
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
