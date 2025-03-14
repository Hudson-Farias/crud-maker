using DotNetEnv;
using Microsoft.EntityFrameworkCore;

using Database;
using UsersAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

Env.Load();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Env.GetString("DATABASE_URL")));
builder.Services.AddScoped<UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.RoutePrefix = "docs";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}
else
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();
app.MapControllers();
app.Run();
