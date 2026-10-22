using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Carrega as connection strings do ficheiro de segredos (fora do Git)
builder.Configuration.AddJsonFile("App_Secrets/secrets.json", optional: false, reloadOnChange: true);

// Configuração do Entity Framework Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro da camada de negócio
builder.Services.AddScoped<ClienteNegocio>();
builder.Services.AddScoped<TecnicoNegocio>();
builder.Services.AddScoped<ServicoNegocio>();
builder.Services.AddScoped<OrdemServicoNegocio>();
builder.Services.AddScoped<UsuarioNegocio>();

// Configuração da Autenticação por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Sair";
        options.AccessDeniedPath = "/Login/AcessoNegado";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Middleware de log de exceções - deve ser o primeiro do pipeline
app.UseMiddleware<TechFixOS.LogMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
