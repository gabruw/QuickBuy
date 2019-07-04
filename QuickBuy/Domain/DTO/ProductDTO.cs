using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class ProductDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(240)]
        public string Name { get; set; }

        [MinLength(0)]
        [MaxLength(800)]
        public string Describe { get; set; }

        [Required]
        public decimal Price { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                AddError("Erro: O Nome do Produto está vazio.");
            }

            if (string.IsNullOrEmpty(Describe) || string.IsNullOrWhiteSpace(Describe))
            {
                AddError("Erro: A Descrição do Produto está vazia.");
            }

            if (Price < 0)
            {
                AddError("Erro: O Preço do Produto está vazio.");
            }
        }
    }
}