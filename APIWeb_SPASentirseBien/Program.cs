using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

//Identity Core
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            }
        };
    });
builder.Services.ConfigureApplicationCookie(options => 
    {
        options.SlidingExpiration = true;
        options.LoginPath = "/api/Account/login";  // Especifica la ruta de inicio de sesión
        options.LogoutPath = "/api/Account/logout"; // Especifica la ruta de cierre de sesión
    });
builder.Services.AddAuthorization();
builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddApiEndpoints();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//DB Context
builder.Services.AddDbContext<ApplicationDBContext>(options => 
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)),
    mySqlOptions =>
            {
                // Habilita la resiliencia de errores transitorios con 5 intentos
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,  // Número máximo de reintentos
                    maxRetryDelay: TimeSpan.FromSeconds(10),  // Tiempo máximo entre reintentos
                    errorNumbersToAdd: null  // Opcional: puedes añadir números de error específicos
                );
            });
});

//Añadir los controladores
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "Cliente", "Empleado" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            // Crear los roles si no existen
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    string email = "adminUser@thisIsAdmin.com";
    string password = "835NoOneAdmin047-";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new Usuario
        {
            UserName = "Administrator",
            Email = email
        };

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
