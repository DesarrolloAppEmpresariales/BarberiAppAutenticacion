using BarberiAppAutenticacion.Models;
using BarberiAppAutenticacion.Interface;
using BarberiAppAutenticacion.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Inicia clase principal");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    //Donot forgot to add ConnectionStrings as "dbConnection" to the appsetting.json file
    builder.Services.AddDbContext<DatabaseContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
    builder.Services.AddTransient<IUsuario, UsuarioRepository>();
    builder.Services.AddControllers();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "El programa se ha detenido porque ha ocurrido una excepciòn");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

