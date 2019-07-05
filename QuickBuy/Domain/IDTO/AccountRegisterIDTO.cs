using System.ComponentModel.DataAnnotations;

namespace Domain.IDTO
{
    public class AccountRegisterIDTO
    {
        [Required]
        [MaxLength(240)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}