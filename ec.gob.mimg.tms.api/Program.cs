using ec.gob.mimg.tms.api.BackgroudServices;
using ec.gob.mimg.tms.api.Services;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.srv.mail.Services;
using ec.gob.mimg.tms.srv.mail.Services.Implements;
using ec.gob.mimg.tms.srv.mimg.Services;
using ec.gob.mimg.tms.srv.mimg.Services.Implements;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

# region LOGGER WITH SERILOG
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#  endregion

# region LOGGER WITH LOGGING MS
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();
#  endregion

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

builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<INotificacionService, NotifacionService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IApiSriService, ApiSriService>();
builder.Services.AddScoped<IApiCatastroService, ApiCatastroService>();
builder.Services.AddScoped<IApiMimgService, ApiMimgService>();
builder.Services.AddScoped<IApiActivoMilService, ApiActivoMilService>();
builder.Services.AddScoped<IApiPatenteService, ApiPatenteService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddHostedService<TaskManagerService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseCors("EnableCors");

app.Logger.LogInformation(">>> Starting the app");

app.Run();


