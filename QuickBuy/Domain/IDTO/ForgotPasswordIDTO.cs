using System.ComponentModel.DataAnnotations;

namespace Domain.IDTO
{
    public class ForgotPasswordIDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}