using Common;
using Processes.Web.Microservices.Implementations;
using Processes.Web.Microservices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ILoteClosureService, LoteClosureService>();
builder.Services.AddTransient<ILoteClosureService, LoteClosureService>();
StaticDetails.LoteClosureAPIBase = builder.Configuration["ApiLoteClosureUrl"];

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
