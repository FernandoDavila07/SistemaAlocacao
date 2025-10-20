using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using MySqlConnector; // Precisamos disso para o erro específico

namespace Infrastructure.Data.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("MigrationExtensions");

            var context = services.GetRequiredService<ApplicationDbContext>();

            logger.LogInformation("Tentando conectar ao banco de dados para aplicar migrações...");

            // Lógica de Repetição (Retry)
            var retryCount = 5;
            var delay = 5000; // 5 segundos

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    // Tenta aplicar as migrações
                    context.Database.Migrate();

                    // Se chegou aqui, funcionou!
                    logger.LogInformation("Migrações aplicadas com sucesso!");
                    return; // Sai da função com sucesso
                }
                catch (MySqlException ex) // Pega especificamente o erro do MySQL
                {
                    logger.LogWarning(ex, "Não foi possível conectar/migrar o banco (Tentativa {Attempt} de {RetryCount})... tentando novamente em {Delay}s", i + 1, retryCount, delay / 1000);

                    if (i == retryCount - 1) // Se for a última tentativa, joga o erro
                    {
                        logger.LogError("Falha ao aplicar migrações após várias tentativas. A aplicação vai travar.");
                        throw; // Joga o erro para fora, o que vai travar a inicialização do app
                    }

                    Thread.Sleep(delay); // Espera antes de tentar de novo
                }
            }
        }
    }
}