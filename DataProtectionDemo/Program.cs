using DataProtectionDemo;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Server=localhost;Database=demo-db;Port=5432;User Id=postgres;Password=P@ssword123"));

//builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"c:\\keys"));

//builder.Services.AddDataProtection().PersistKeysToDbContext<AppDbContext>();

builder.Services.AddDataProtection()
    .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
    .PersistKeysToAzureBlobStorage("string-connection",
                                                                   "dataprotection",
                                                                   "data");

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
