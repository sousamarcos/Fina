using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Fina.Core.Services;
using System.Net.Http;
using System.Net.Http.Json;

namespace Fina.Web.Services
{
    public class CategoryService(IHttpClientFactory httpClientFactory) : ICategoryService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "Falha ao criar categoria");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
