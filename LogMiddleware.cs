namespace TechFixOS
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private static readonly object _lock = new();

        public LogMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await RegistrarLogAsync(context, ex);
                throw;
            }
        }

        private Task RegistrarLogAsync(HttpContext context, Exception ex)
        {
            var pastaLogs = Path.Combine(_env.ContentRootPath, "App_Data", "logs");
            Directory.CreateDirectory(pastaLogs);

            var caminhoArquivo = Path.Combine(pastaLogs, "log.txt");

            var usuario = context.User?.Identity?.IsAuthenticated == true
                ? context.User.Identity.Name ?? "Desconhecido"
                : "Anônimo";

            var url = $"{context.Request.Method} {context.Request.Path}{context.Request.QueryString}";

            var registro = string.Join(Environment.NewLine,
                "==================================================",
                $"Data/Hora   : {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                $"URL         : {url}",
                $"Utilizador  : {usuario}",
                $"Mensagem    : {ex.Message}",
                $"Stack Trace : {ex.StackTrace}",
                string.Empty);

            lock (_lock)
            {
                File.AppendAllText(caminhoArquivo, registro);
            }

            return Task.CompletedTask;
        }
    }
}
