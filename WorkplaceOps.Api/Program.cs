using Microsoft.EntityFrameworkCore;
using WorkplaceOps.Infrastructure.Persistence;
using WorkplaceOps.Application.Businesses;
using WorkplaceOps.Infrastructure.Businesses;
using WorkplaceOps.Application.Rules;
using WorkplaceOps.Infrastructure.Rules;

var builder = WebApplication.CreateBuilder(args);

// Adding a CORS policy to allow requests from any origin, method, and header
const string ClientCorsPolicy = "ClientCorsPolicy";

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registering the repositories and services 
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<BusinessService>();

builder.Services.AddScoped<IBusinessOperationalProfileRepository, BusinessOperationalProfileRepository>();
builder.Services.AddScoped<BusinessOperationalProfileService>();

builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<RuleService>();


builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(ClientCorsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:64178") // Allow requests from this specific origin
            .AllowAnyHeader() // Allow any HTTP header
            .AllowAnyMethod(); // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ClientCorsPolicy);

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
