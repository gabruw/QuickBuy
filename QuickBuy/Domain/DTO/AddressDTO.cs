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
        public int CEP { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MaxLength(130)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Neighborhood { get; set; }

        [Required]
        [MaxLength(150)]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        public int? Complement { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (CEP < 1)
            {
                ValidationMessage.Add("Erro: O CEP do Endereço está vazio.");
            }

            if (CEP < 8)
            {
                ValidationMessage.Add("Erro: O CEP do Endereço falta números.");
            }

            if (string.IsNullOrEmpty(Country) || string.IsNullOrWhiteSpace(Country))
            {
                ValidationMessage.Add("Erro: O País do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(State) || string.IsNullOrWhiteSpace(State))
            {
                ValidationMessage.Add("Erro: O Estado do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(City) || string.IsNullOrWhiteSpace(City))
            {
                ValidationMessage.Add("Erro: A Cidade do Endereço está vazia.");
            }

            if (string.IsNullOrEmpty(Neighborhood) || string.IsNullOrWhiteSpace(Neighborhood))
            {
                ValidationMessage.Add("Erro: O Bairro do Endereço está vazio.");
            }

            if (string.IsNullOrEmpty(Street) || string.IsNullOrWhiteSpace(Street))
            {
                ValidationMessage.Add("Erro: A Rua do Endereço está vazia.");
            }

            if (Number < 1)
            {
                ValidationMessage.Add("Erro: O Número do Endereço está vazio.");
            }

            if (Number > 6)
            {
                ValidationMessage.Add("Erro: O Número do Endereço possuí caracteres demais (> 6).");
            }
        }
    }
}