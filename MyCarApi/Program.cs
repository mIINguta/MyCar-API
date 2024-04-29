
using Microsoft.EntityFrameworkCore;
using MyCarApi.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyCarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home} / {action= Index} / {id?}");
});
app.Run();
