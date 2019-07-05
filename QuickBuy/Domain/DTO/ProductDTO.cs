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
        [MaxLength(240)]
        public string Name { get; set; }

        [MaxLength(800)]
        public string Describe { get; set; }

        [Required]
        public decimal Price { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                ValidationMessage.Add("Erro: O Nome do Produto está vazio.");
            }

            if (Price < 1)
            {
                ValidationMessage.Add("Erro: O Preço do Produto está vazio.");
            }
        }
    }
}