using Fina.API.Common.API;
using Fina.Core;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Fina.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fina.API.EndPoints.Transactions
{
    public class TransactionEndPoints : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", TransactionServiceCreateAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();

            app.MapPut("/{id}", TransactionServicUpdateAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

            app.MapDelete("/{id}", TransactionServiceDeleteAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma transação")
            .WithDescription("Exclui uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

            app.MapGet("/{id}", TransactionServiceGetByIdAsync)
                        .WithName("Transactions: Get By Id")
                        .WithSummary("Recupera uma transação")
                        .WithDescription("Recupera uma transação")
                        .WithOrder(4)
                        .Produces<Response<Transaction?>>();

            app.MapGet("/", TransactionServiceGetAllAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Recupera todas as transações")
            .WithDescription("Recupera todas as transações")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();
        }

        private static async Task<IResult> TransactionServiceCreateAsync(ITransactionService servico, CreateTransactionRequest request)
        {
            request.UserId = APIConfiguration.UserID;
            var response = await servico.CreateAsync(request);
            //if (response.IsSucess)
            //    return Results.Ok();

            //return Results.NotFound();

            return response.IsSuccess ? TypedResults.Created($"/{response.Data?.Id}", response) : TypedResults.BadRequest(response);
        }

        private static async Task<IResult> TransactionServicUpdateAsync(ITransactionService servico, UpdateTransactionRequest request, long id)
        {
            request.UserId = APIConfiguration.UserID;
            request.Id = id;

            var result = await servico.UpdateAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> TransactionServiceGetByIdAsync(ITransactionService service, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = APIConfiguration.UserID,
                Id = id
            };

            var result = await service.GetByIdAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> TransactionServiceDeleteAsync(ITransactionService service, long id)
        {
            var request = new DeleteTransactionRequest
            {
                Id = id,
                UserId = APIConfiguration.UserID
            };

            var result = await service.DeleteAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> TransactionServiceGetAllAsync(ITransactionService service,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = APIConfiguration.UserID,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await service.GetByPeriodAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
