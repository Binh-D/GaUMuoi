using GaUMuoi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Đăng ký GaUMuoiDbContext với SQL Server
builder.Services.AddDbContext<GaUMuoiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký AppDbContext
builder.Services.AddDbContext<GaUMuoiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký Authentication với Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";   // Trang đăng nhập
        options.LogoutPath = "/Login/Logout";  // Trang đăng xuất
    });

// Đăng ký MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Áp dụng migration tự động (nếu chưa có CSDL)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GaUMuoiDbContext>();

    context.Database.Migrate(); // Tự động tạo CSDL nếu chưa có

    // Chạy Seed để khởi tạo dữ liệu mặc định
    DbInitializer.Seed(context);
}

// Cấu hình Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Sử dụng session
app.UseAuthentication(); // Kích hoạt đăng nhập
app.UseAuthorization(); // Kích hoạt phân quyền

// Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
