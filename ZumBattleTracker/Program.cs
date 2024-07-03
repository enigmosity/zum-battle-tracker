using Microsoft.EntityFrameworkCore;
using ZumBattleTracker;
using ZumBattleTracker.Helpers;
using ZumBattleTracker.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<PokemonService>();
builder.Services.AddTransient<Random>();
builder.Services.AddTransient<IBattleHelper, BattleHelper>();
builder.Services.AddTransient<TournamentHelper>();

builder.Services.AddDbContext<PokemonContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



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
