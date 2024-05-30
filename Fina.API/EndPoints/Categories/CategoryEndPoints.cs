using Fina.API.Common.API;
using Fina.Core.Requests.Categories;
using Fina.Core.Services;
using Fina.Core.Models;
using Fina.Core.Responses;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Fina.Core;

namespace Fina.API.EndPoints.Categories
{
    public class CategoryEndPoints : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", CategoryServiceCreateAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();

            app.MapPut("/{id}", CategoryServiceUpdateAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

            app.MapDelete("/{id}", CategoryServiceDeleteAsync)
            .WithName("Categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

            app.MapGet("/{id}", CategoryServiceGetByIdAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

            app.MapGet("/", CategoryServiceGetAllAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();
        }

        private static async Task<IResult> CategoryServiceCreateAsync(ICategoryService handler,CreateCategoryRequest request)
        {
            request.UserId = APIConfiguration.UserID;
            var response = await handler.CreateAsync(request);

            return response.IsSuccess ? TypedResults.Created($"/{response.Data?.Id}", response) : TypedResults.BadRequest(response);
        }

        private static async Task<IResult> CategoryServiceDeleteAsync(ICategoryService service, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = APIConfiguration.UserID,
                Id = id
            };

            var result = await service.DeleteAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> CategoryServiceGetAllAsync(ICategoryService service, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = APIConfiguration.UserID,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await service.GetAllAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> CategoryServiceGetByIdAsync(ICategoryService service, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = APIConfiguration.UserID,
                Id = id
            };

            var result = await service.GetByIdAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        private static async Task<IResult> CategoryServiceUpdateAsync(ICategoryService service, UpdateCategoryRequest request, long id)
        {

            request.UserId = APIConfiguration.UserID;
            request.Id = id;

            var result = await service.UpdateAsync(request);


            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

    }
}
