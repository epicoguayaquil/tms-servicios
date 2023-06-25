using Microsoft.Extensions.Options;

namespace ec.gob.mimg.tms.api.BackgroudServices
{
    public class TaskManagerService : BackgroundService
    {
        private readonly ILogger<TaskManagerService> _logger;
        private static int? establecimientoNuevoHora;
        private static int? establecimientoNuevoMin;

        public TaskManagerService(ILogger<TaskManagerService> logger)
        {
            _logger = logger;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            establecimientoNuevoHora = Convert.ToInt16(root.GetSection("BackgroudServicesSettings:EstablecimientosNuevos_Hora").Value);
            establecimientoNuevoMin = Convert.ToInt16(root.GetSection("BackgroudServicesSettings:EstablecimientosNuevos_Min").Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug(">>> TaskManagerService is starting...");

            DateTime dateStart = DateTime.Now;
            int start_hour = Convert.ToInt32(dateStart.Hour.ToString()) ;
            int start_min = Convert.ToInt32(dateStart.Minute.ToString()) + 1;
            
            TaskService.IntervalInSeconds(start_hour, start_min, 10,
            () => {
                DateTime aDate = DateTime.Now;
                _logger.LogInformation("[Task #1 ] {0}", aDate.ToString("yyyy/MM/dd HH:mm:ss"));

            });

            // Se ejecuta desde las 08:00 cada 6 horas 
            TaskService.IntervalInHours(08, 00, 6,
            () => {
                DateTime aDate = DateTime.Now;
                _logger.LogInformation("[Task #2 ] {0}", aDate.ToString("yyyy/MM/dd HH:mm:ss"));
            });

            // Se ejecuta desde las 08:00 cada 1 día
            TaskService.IntervalInDays((int)establecimientoNuevoHora, (int)establecimientoNuevoMin, 1,
            () => {
                DateTime aDate = DateTime.Now;
                _logger.LogInformation("[Task #3 ] {0}", aDate.ToString("yyyy/MM/dd HH:mm:ss"));
            });
        }
    }

}
