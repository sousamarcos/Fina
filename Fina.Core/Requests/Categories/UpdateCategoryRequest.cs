﻿using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "ID inválido")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Títuto inválido")]
        [MaxLength(80, ErrorMessage = "O titudo deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida")]
        public string Descricao { get; set; } = string.Empty;
    }
}
