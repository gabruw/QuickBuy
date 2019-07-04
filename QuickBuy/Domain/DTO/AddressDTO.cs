using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class AddressDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        public int CEP { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(130)]
        public string City { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Neighborhood { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string Street { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(6)]
        public int Number { get; set; }

        public int Complement { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (CEP < 0)
            {
                AddError("Erro: O CEP do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(Country) || string.IsNullOrWhiteSpace(Country))
            {
                AddError("Erro: O País do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(State) || string.IsNullOrWhiteSpace(State))
            {
                AddError("Erro: O Estado do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(City) || string.IsNullOrWhiteSpace(City))
            {
                AddError("Erro: A Cidade do Endereço está vazia.");
            }

            if (string.IsNullOrEmpty(Neighborhood) || string.IsNullOrWhiteSpace(Neighborhood))
            {
                AddError("Erro: O Bairro do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(Street) || string.IsNullOrWhiteSpace(Street))
            {
                AddError("Erro: A Rua do Endereço está vazia.");
            }

            if (Number < 0)
            {
                AddError("Erro: O Número do Endereço está vazio.");
            }
        }
    }
}