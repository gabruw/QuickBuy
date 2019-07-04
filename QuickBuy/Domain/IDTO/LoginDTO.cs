﻿using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class LoginDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}