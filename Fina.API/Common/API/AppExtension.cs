namespace Fina.API.Common.API
{
    public static class AppExtension
    {
        public static void ConfigureAmbienteDesenvolvimento(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // app.MapSwagger().RequireAuthorization();
        }
    }
}