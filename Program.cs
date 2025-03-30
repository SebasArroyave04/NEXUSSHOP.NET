using prototipo_web.Models;
using prototipo_web.Repositorio;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IRepositorioHome,ReposiitorioHome> ();
builder.Services.AddTransient<IRepositorioCompras, RepositorioCompras>();
builder.Services.AddTransient<IRepositorioProveedor, RepositroioProveedor>();
builder.Services.AddTransient <IrepositorioContacto, RepositorioContactanos>();
builder.Services.AddTransient<IRepositorioPDF, RepositorioPDF>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IcarritoServicio, carritoServicio>();
builder.Services.AddTransient<productoSeleccionado>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{ 
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();