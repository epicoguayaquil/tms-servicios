using ec.gob.mimg.tms.srv.sri.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatecDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
