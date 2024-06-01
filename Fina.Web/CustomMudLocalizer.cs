using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Fina.Web
{
    internal class CustomMudLocalizer : MudLocalizer
    {
        private Dictionary<string, string> _localization;

        public override LocalizedString this[string key]
        {
            get
            {
                var currentCulture = Thread.CurrentThread.CurrentUICulture.Parent.TwoLetterISOLanguageName;
                if (currentCulture.Equals("pt", StringComparison.InvariantCultureIgnoreCase)
                    && _localization.TryGetValue(key, out var res))
                {
                    return new(key, res);
                }
                else
                {
                    return new(key, key, true);
                }
            }
        }

        public CustomMudLocalizer()
        {
            _localization = new()
        {

            { "MudDataGrid.AddFilter", "Adicionar filtro" },
            { "MudDataGrid.Apply", "Aplicar" },
            { "MudDataGrid.Cancel", "Cancelar" },
            { "MudDataGrid.Clear", "Limpar" },
            { "MudDataGrid.CollapseAllGroups", "Recolher todos os grupos" },
            { "MudDataGrid.Column", "Coluna" },
            { "MudDataGrid.Columns", "Colunas" },
            { "MudDataGrid.contains", "Contem" },
            { "MudDataGrid.ends with", "Termina com" },
            { "MudDataGrid.equals", "Igual" },
            { "MudDataGrid.ExpandAllGroups", "Expandir todos os grupos" },
            { "MudDataGrid.Filter", "Filtro" },
            { "MudDataGrid.False", "Falso" },
            { "MudDataGrid.FilterValue", "Valor filtro" },
            { "MudDataGrid.Group", "Grupo" },
            { "MudDataGrid.Hide", "Esconder" },
            { "MudDataGrid.HideAll", "Esconder todos" },
            { "MudDataGrid.is", "é" },
            { "MudDataGrid.is after", "é após" },
            { "MudDataGrid.is before", "é antes" },
            { "MudDataGrid.is empty", "é vazio" },
            { "MudDataGrid.is not", "Não é" },
            { "MudDataGrid.is not empty", "Não é vazio" },
            { "MudDataGrid.is on or after", "está ligado ou após" },
            { "MudDataGrid.is on or before", "está ligado ou antes" },
            { "MudDataGrid.not contains", "não contém" },
            { "MudDataGrid.not equals", "não é igual" },
            { "MudDataGrid.Operator", "Operador" },
            { "MudDataGrid.RefreshData", "Atualizar dados" },
            { "MudDataGrid.Save", "Salvar" },
            { "MudDataGrid.ShowAll", "Mostrar tudo" },
            { "MudDataGrid.starts with", "Começa com" },
            { "MudDataGrid.True", "Verdadeiro" },
            { "MudDataGrid.Ungroup", "Desagrupar" },
            { "MudDataGrid.Unsort", "Ordenar" },
            { "MudDataGrid.Value", "Valor" }
        };
        }
    }
}