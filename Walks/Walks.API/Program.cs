using Microsoft.EntityFrameworkCore;
using Walks.API.Repositories.RegionRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Adding DbContext.
builder.Services.AddDbContext<WalksDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DbConnection"))); 
//Adding the scope.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
