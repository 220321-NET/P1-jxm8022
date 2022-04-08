using DataLayer;
using BusinessLayer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(
    (ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("../logs/telescopeLogs.txt", rollingInterval: RollingInterval.Day)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("TelescopeStoreDB")));
builder.Services.AddScoped<IBusiness, Business>();

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
