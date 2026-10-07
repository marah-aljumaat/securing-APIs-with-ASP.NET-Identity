using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add controllers support *********
builder.Services.AddControllers();

// Configure in-memory database for Identity ************
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDB"));

// Configure Identity services*******************
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

// Add authorization policies (without requiring authentication)*******************
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireITDepartment", policy => policy.RequireClaim("Department", "IT"));
});                

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//**************
app.UseAuthorization();

// Enable controllers**********
app.MapControllers();

app.Run();

