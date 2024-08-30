using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Identity Core
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<Usuario>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddApiEndpoints();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//DB Context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

//Añadir los controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<Usuario>();

app.MapControllers();

app.Run();
