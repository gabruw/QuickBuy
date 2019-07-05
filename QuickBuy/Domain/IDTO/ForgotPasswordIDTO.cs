using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ForgotPasswordIDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}