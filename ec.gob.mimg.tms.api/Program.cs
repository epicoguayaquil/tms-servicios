using ec.gob.mimg.tms.model.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(x => x.AddPolicy("EnableCors", builder => {
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TmsDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors("EnableCors");

app.Run();

