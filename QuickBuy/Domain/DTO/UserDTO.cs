using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class UserDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string IdAccount { get; set; }

        [ForeignKey("IdAccount")]
        public virtual AccountIDTO AccountUser { get; set; }

        public int IdAddress { get; set; }

        [ForeignKey("IdAddress")]
        public virtual AddressDTO AddressUser { get; set; }

        [Required]
        [MaxLength(240)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string LastName { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                ValidationMessage.Add("Erro: O Nome do Usuário está vazio.");
            }
        }
    }
}