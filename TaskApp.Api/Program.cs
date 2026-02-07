using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application;
using TaskApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(typeof(AssemblyReference).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskApp API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
// app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.Run();