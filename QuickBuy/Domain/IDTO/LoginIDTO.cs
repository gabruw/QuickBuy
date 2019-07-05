using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class LoginIDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}