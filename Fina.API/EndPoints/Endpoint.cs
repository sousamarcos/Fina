using Fina.API.Common.API;
using Fina.API.EndPoints.Categories;
using Fina.API.EndPoints.Transactions;

namespace Fina.API.EndPoints
{
    public static class Endpoint
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");
            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "Ok" });

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization()
                .MapEndpoint<CategoryEndPoints>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                //.RequireAuthorization()
                .MapEndpoint<TransactionEndPoints>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndPoint>(this IEndpointRouteBuilder app) where TEndPoint : IEndPoint
        {
            TEndPoint.Map(app);
            return app;
        }
    }
}
