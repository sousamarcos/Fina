using Fina.API.Data;
using Fina.API.Services;
using Fina.Core;
using Fina.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Fina.API.Common.API
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConection") ?? string.Empty;
            Configuration.BackEndURL = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontEndURL = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(n => n.FullName); });
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<AppDbContext>(x => { x.UseSqlServer(Configuration.ConnectionString); });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    APIConfiguration.CorsPolicyName,
                    policy => policy
                    .WithOrigins([
                        Configuration.BackEndURL,
                        Configuration.FrontEndURL
                        ])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    )
                );
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
        }
    }
}
