using IntegraPartnersContactApplicationAPI;
using IntegraPartnersContactApplicationAPI.Controllers;
using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.mapping;
using IntegraPartnersContactApplicationAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IntegraPartnersContactAPIDataContext>(options =>
{
    options.UseSqlServer("Data Source=DESKTOP-7Q3Q0SF;Initial Catalog=IntegraPartners;Integrated Security=True;Encrypt=False");
});
builder.Services.AddScoped<IMapping, Mapping>();
builder.Services.AddScoped<IUsersController, UsersController>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));
//builder.Services.AddSwaggerGen();

var app = builder.Build();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{UserID?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
 //   app.UseSwagger();
   // app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
