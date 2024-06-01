using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories
{
    public partial class HomeCategoriesPage : ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public List<Category> listaCategorias { get; set; } = new List<Category>();
        public string searchString;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        [Inject]
        public ICategoryService Service { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; }= null!;


        public Func<Category, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (x.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            //if ($"{x.Description} {x.Title}".Contains(searchString))
            //    return true;

            return false;
        };

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await Service.GetAllAsync(request);
                if (result.IsSuccess)
                    listaCategorias = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OpenAddCategoryDialog()
        {
            try
            {
                var parameters = new DialogParameters();
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
                var dialog = DialogService.Show<AddEditCategoryDialog>("Adicionar Categoria", parameters, options);
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    var category = (Category)result.Data;

                    var request = new CreateCategoryRequest()
                    {
                        Description = category.Description,
                        Title = category.Title
                    };

                    var servicoCategoriaCriada = await Service.CreateAsync(request);
                    if (servicoCategoriaCriada.IsSuccess)
                        Snackbar.Add(servicoCategoriaCriada.Message, Severity.Success);
                    else
                        Snackbar.Add(servicoCategoriaCriada.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OpenEditCategoryDialog(long id, string title)
        {
            try
            {
                var category = new Category
                {
                    Title = title,
                    Id = id
                };
                var parameters = new DialogParameters { ["Category"] = category };
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
                var dialog = DialogService.Show<AddEditCategoryDialog>("Editar Categoria", parameters, options);
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    var updatedCategory = (Category)result.Data;

                    var request = new UpdateCategoryRequest()
                    {
                        Description = category.Description,
                        Title = category.Title
                    };

                    var servicoCategoriaAtualizada = await Service.UpdateAsync(request);

                    if (servicoCategoriaAtualizada.IsSuccess)
                        Snackbar.Add(servicoCategoriaAtualizada.Message, Severity.Success);
                    else
                        Snackbar.Add(servicoCategoriaAtualizada.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OnDeleteCategory(long id, string title)
        {
            try
            {
                var result = await Dialog.ShowMessageBox(
                    "ATENÇÃO",
                    $"Ao prosseguir a categoria {title} será removida. Deseja continuar?",
                    yesText: "Excluir",
                    cancelText: "Cancelar");

                if (result is true)
                    await DeleteAsync(id, title);

                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task DeleteAsync(long id, string title)
        {
            try
            {
                var request = new DeleteCategoryRequest
                {
                    Id = id
                };
                await Service.DeleteAsync(request);
                listaCategorias.ToList().RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}
