using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class UserDTO : IdentityUser
    {
        public int IdEndereco { get; set; }

        [ForeignKey("IdEndereco")]
        public AddressDTO EnderecoUser { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Permission { get; set; }

        public ICollection<OrderDTO> Orders { get; set; }
    }
}