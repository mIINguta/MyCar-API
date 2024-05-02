using Microsoft.AspNetCore.Authentication.JwtBearer;
using  Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCarApi.Context;
using MyCarApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);


//conexão banco de dados e ligando Identity
builder.Services.AddDbContext<MyCarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MyCarContext>().AddDefaultTokenProviders();
// configuração da autenticação com JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  //pegando string no app.settings
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetSection("KEY").Value)),
                  ClockSkew = TimeSpan.Zero
              });
// configuração da autoricação com JWT
builder.Services.AddAuthorization(options => 
      {
          var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
          defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
          options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
      });
    builder.Services.AddTransient<UserToken>(); // salvamos o token
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();

    var app = builder.Build();

// cors que possibilita consumir a api com o endereço da aplicação react
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173"); 
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.MapControllers();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.Run();