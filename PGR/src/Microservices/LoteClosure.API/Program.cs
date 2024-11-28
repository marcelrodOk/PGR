using LoteClosureServices.Business.Implementations;
using LoteClosureServices.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLoteClosure.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration["LocalEnviroment"] == "true")
{
	builder.Services.AddDbContext<LoteClosureDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"),
		 x => x.MigrationsHistoryTable("__EFMigrationsHistory", "TblIntermedia")));
}
else
{
	builder.Services.AddDbContext<LoteClosureDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
			 x => x.MigrationsHistoryTable("__EFMigrationsHistory", "TblIntermedia")));
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ILoteClosureService, LoteClosureService>();
builder.Services.AddScoped<ILoteClosureService, LoteClosureService>();

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
