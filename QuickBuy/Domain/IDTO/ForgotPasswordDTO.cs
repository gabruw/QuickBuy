using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}